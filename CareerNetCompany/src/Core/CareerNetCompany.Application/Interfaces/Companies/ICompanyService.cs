using CareerNetCompany.Application.Dtos;

namespace CareerNetCompany.Application.Interfaces.Company
{
    /// <summary>
    /// Company ile ilgili temel işlemlerini tanımlayan arayüz.
    /// </summary>
    public interface ICompanyService
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
        /// <param name="companyId">Silinmesi istenen işverenin kimliği.</param>
        /// <returns>Silme işleminin başarılı olup olmadığını gösterir.</returns>
        Task<bool> DeleteCompanyAsync(Guid companyId);

        /// <summary>
        /// Kimliğe göre bir işverenin detaylarını getirir.
        /// </summary>
        /// <param name="companyId">İşverenin kimliği.</param>
        /// <returns>İşverenin detayları.</returns>
        Task<CompanyDto> GetCompanyByIdAsync(Guid companyId);

        /// <summary>
        /// Telefon numarasına göre şirket bilgisini getiren metod.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<bool> GetCompanyByPhoneNumber(string phoneNumber);

        /// <summary>
        /// Tüm işverenlerin listesini getirir.
        /// </summary>
        /// <returns>İşverenlerin detay listesi.</returns>
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();

        /// <summary>
        /// Firmanın ilan hakkı sayısını kontrol edip yayınlama hakkını 1 azaltır.
        /// </summary>
        /// <param name="companyId">İşverenin kimliği.</param>
        /// <returns></returns>
        Task<bool> CheckJobRightsAndDecreaseCount(Guid companyId);

    }
}
