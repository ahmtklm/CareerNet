using AutoMapper;
using CareerNetCompany.Application.Dtos;
using CareerNetCompany.Application.Interfaces.Company;
using CareerNetCompany.Application.Interfaces.Repositories;
using CareerNetCompany.Domain.Entities;
using CareerNetCompany.Persistance.Concretes.Repositories;

namespace CareerNetCompany.Persistance.Concretes.Companies
{
    /// <summary>
    /// ICompanyService arayüzünü implement eden ve Company entity'si ile ilgili işlemleri yöneten service sınıfı.
    /// </summary>
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(IMapper mapper, IRepository<Company> companyRepository)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDto> CreateCompanyAsync(CompanyCreateDto createDto)
        {
            var companyEntity = _mapper.Map<Company>(createDto);
            var addedCompany = await _companyRepository.AddAsync(companyEntity);
            return _mapper.Map<CompanyDto>(addedCompany);
        }

        public async Task<CompanyDto> UpdateCompanyAsync(CompanyUpdateDto updateDto)
        {
            var isExistCompany = await _companyRepository.AnyAsync(p=>p.Id == updateDto.Id);
            if (!isExistCompany) throw new Exception("Firma Bulunamadı");

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
            return company != null ? _mapper.Map<CompanyDto>(company) : null!;
        }

        public async Task<bool> DeleteCompanyAsync(Guid id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
                return false;

            await _companyRepository.DeleteAsync(company);
            return true;
        }
    }
}
