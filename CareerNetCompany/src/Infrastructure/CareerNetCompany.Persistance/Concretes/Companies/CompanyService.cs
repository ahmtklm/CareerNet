using AutoMapper;
using CareerNetCompany.Application.Dtos;
using CareerNetCompany.Application.Exceptions;
using CareerNetCompany.Application.Interfaces.Company;
using CareerNetCompany.Application.Interfaces.Repositories;
using CareerNetCompany.Domain.Entities;

namespace CareerNetCompany.Persistance.Concretes.Companies
{
    /// <summary>
    /// ICompanyService arayüzünü implement eden ve Company entity'si ile ilgili işlemleri yöneten service sınıfı.
    /// </summary>
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Company> _companyRepository;

        public CompanyService(IMapper mapper, IRepository<Company> companyRepository)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDto> CreateCompanyAsync(CompanyCreateDto createDto)
        {
            //İşveren aynı telefon numarasıyla tekrar kayıt olmamalıdır
            var isPhoneNumberExist = await _companyRepository.AnyAsync(p => p.PhoneNumber == createDto.PhoneNumber);
            if (isPhoneNumberExist)
                throw new ConflictException($"{createDto.PhoneNumber} numaraya ait başka bir firma kayıtlı olmamalıdır.");

            //Yeni işveren ilan yayınlama hakkı sayısı Dto'da default 2 olduğu için burada ekstra set etmeye gerek yok
            var companyEntity = _mapper.Map<Company>(createDto);
            var addedCompany = await _companyRepository.AddAsync(companyEntity);

            return _mapper.Map<CompanyDto>(addedCompany);
        }

        public async Task<CompanyDto> UpdateCompanyAsync(CompanyUpdateDto updateDto)
        {
            //// İşverenin mevcut olup olmadığı kontrol edilir
            var isExistCompany = await _companyRepository.AnyAsync(p=>p.Id == updateDto.Id);
            if (!isExistCompany) throw new KeyNotFoundException($"{updateDto.Id} Id'li firma bulunamadı");

            var companyEntity = _mapper.Map<Company>(updateDto);
            var updatedCompany = await _companyRepository.UpdateAsync(companyEntity);
            return _mapper.Map<CompanyDto>(updatedCompany);
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public async Task<CompanyDto> GetCompanyByIdAsync(Guid id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
                throw new KeyNotFoundException($"{id} Id'li firma bulunamadı");

            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<bool> DeleteCompanyAsync(Guid id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
                throw new KeyNotFoundException($"{id} Id'li firma bulunamadı");

            await _companyRepository.DeleteAsync(company);
            return true;
        }

        public async Task<bool> CheckJobRightsAndDecreaseCount(Guid companyId)
        {
            //Firma kontrolü
            var company = await _companyRepository.GetByIdAsync(companyId);
            if(company == null) throw new KeyNotFoundException($"{companyId} Id'li firma bulunamadı");

            //Firmanın ilan yayınlama hakkı kontrolü
            if (company.JobPostingRightCount <= 0)
                throw new ConflictException($"{companyId} Id'li firmanın ilan yayınlama hakkı yoktur.");

            //İlan yayına aldındığında ilan hakkı 1 azaltılır ve Db'ye güncellenir.
            company.JobPostingRightCount--;
            await _companyRepository.UpdateAsync(company);

            return true;
        }
    }
}
