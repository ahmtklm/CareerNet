using CareerNetCompany.Application.Dtos;

namespace CareerNetCompany.Application.Interfaces
{
    /// <summary>
    /// Company ile ilgili temel işlemlerini tanımlayan arayüz.
    /// </summary>
    internal interface ICompanyService
    {
        /// <summary>
        /// Yeni bir işveren ekler.
        /// </summary>
        /// <param name="createDto">Eklenmesi istenen işveren bilgileri.</param>
        /// <returns>Oluşturulan işverenin detayları.</returns>
        Task<CompanyDto> CreateCompanyAsync(CompanyCreateDto createDto);

        /// <summary>
        /// Mevcut bir işvereni günceller.
        /// </summary>
        /// <param name="updateDto">Güncellenmesi istenen işveren bilgileri.</param>
        /// <returns>Güncellenen işverenin detayları.</returns>
        Task<CompanyDto> UpdateCompanyAsync(CompanyUpdateDto updateDto);

        /// <summary>
        /// Belirli bir işvereni siler (soft delete).
        /// </summary>
        /// <param name="id">Silinmesi istenen işverenin kimliği.</param>
        /// <returns>Silme işleminin başarılı olup olmadığını gösterir.</returns>
        Task<bool> DeleteCompanyAsync(Guid id);

        /// <summary>
        /// Kimliğe göre bir işverenin detaylarını getirir.
        /// </summary>
        /// <param name="id">İşverenin kimliği.</param>
        /// <returns>İşverenin detayları.</returns>
        Task<CompanyDto> GetCompanyByIdAsync(Guid id);

        /// <summary>
        /// Tüm işverenlerin listesini getirir.
        /// </summary>
        /// <returns>İşverenlerin detay listesi.</returns>
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();
    }
}
