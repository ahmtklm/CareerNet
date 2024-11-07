using CareerNetCompany.Application.Dtos;
using CareerNetCompany.Application.Interfaces.Company;
using Microsoft.AspNetCore.Mvc;

namespace CareerNetCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _companyService.CreateCompanyAsync(createDto);

            return Created();
        }

        /// <summary>
        /// Belirli bir işvereni günceller.
        /// </summary>
        /// <param name="updateDto">Güncelleme bilgileri.</param>
        /// <returns>Güncellenen işverenin detayları.</returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _companyService.UpdateCompanyAsync(updateDto);
            return Ok(result);
        }


        /// <summary>
        /// Belirli bir işvereni siler (soft delete).
        /// </summary>
        /// <param name="id">Silinecek işverenin kimliği.</param>
        /// <returns>Silme işleminin başarılı olup olmadığını gösterir.</returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var result = await _companyService.DeleteCompanyAsync(id);
            if (!result)
                return NotFound("Company not found");

            return NoContent();
        }

        /// <summary>
        /// Belirli bir işverenin detaylarını getirir.
        /// </summary>
        /// <param name="id">İşverenin kimliği.</param>
        /// <returns>İşverenin detayları.</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCompanyById(Guid id)
        {
            var result = await _companyService.GetCompanyByIdAsync(id);
            if (result == null)
                return NotFound("Company not found");

            return Ok(result);
        }

        /// <summary>
        /// Tüm işverenlerin listesini getirir.
        /// </summary>
        /// <returns>İşverenlerin detay listesi.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var result = await _companyService.GetAllCompaniesAsync();
            return Ok(result);
        }
    }
}
