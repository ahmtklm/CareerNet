using CareerNetCompany.Application.Dtos;
using CareerNetCompany.Application.Interfaces.Company;
using Microsoft.AspNetCore.Mvc;

namespace CareerNetCompany.API.Controllers
{
    /// <summary>
    /// İşveren işlemlerini yöneten controller.
    /// </summary>
    public class CompanyController : CustomBaseController
    {
        private readonly ICompanyService _companyService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Yeni bir işveren oluşturur.
        /// </summary>
        /// <param name="createDto">Yeni işveren bilgileri.</param>
        /// <returns>Oluşturulan işverenin detayları.</returns>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCompany(CompanyCreateDto createDto)
        {
            //Aynı telefon numarasına ait firma var mı kontrolü
            var isExistCompany = await _companyService.GetCompanyByPhoneNumber(createDto.PhoneNumber);
            if (isExistCompany) return BadRequest($"{createDto.PhoneNumber} numarasına ait başka bir firma kayıtlıdır.");

            var result = await _companyService.CreateCompanyAsync(createDto);
            return Ok(result);
        }

        /// <summary>
        /// Tüm işverenlerin listesini getirir.
        /// </summary>
        /// <returns>İşverenlerin detay listesi.</returns>
        [HttpGet("GetAllCompanies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            var result = await _companyService.GetAllCompaniesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Belirli bir işverenin detaylarını getirir.
        /// </summary>
        /// <param name="id">İşverenin kimliği.</param>
        /// <returns>İşverenin detayları.</returns>
        [HttpGet("GetCompanyById")]
        public async Task<IActionResult> GetCompanyById(Guid id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            return Ok(company);
        }

        /// <summary>
        /// Belirli bir işvereni günceller.
        /// </summary>
        /// <param name="updateDto">Güncelleme bilgileri.</param>
        /// <returns>Güncellenen işverenin detayları.</returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyUpdateDto updateDto)
        {
            //Id'ye ait firma kontrolü
            var company = await _companyService.GetCompanyByIdAsync(updateDto.Id);
            if (company == null) return NotFound($"{updateDto.Id} Id'li firma bulunamadı");

            var result = await _companyService.UpdateCompanyAsync(updateDto);
            return Ok(result);
        }

        /// <summary>
        /// Belirli bir işvereni siler
        /// </summary>
        /// <param name="id">Silinecek işverenin kimliği.</param>
        /// <returns>Silme işleminin başarılı olup olmadığını gösterir.</returns>
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _companyService.DeleteCompanyAsync(id);
            return NoContent();
        }
    }
}
