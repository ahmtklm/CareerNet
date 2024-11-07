using CareerNetCompany.Domain.Base;

namespace CareerNetCompany.Domain.Entities
{
    /// <summary>
    /// Company sınıfı, işveren bilgilerini tutar.
    /// Bu sınıf, işverenin adı, adresi, telefon numarası ve ilan hakkı gibi özellikleri içerir.
    /// </summary>
    public class Company : BaseEntity
    {
        /// <summary>
        /// İşverenin adı veya firma adı. Bu alan zorunludur.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// İşverenin benzersiz telefon numarası. Bu alan zorunludur
        /// </summary>
        public required string PhoneNumber { get; set; }

        /// <summary>
        /// İşverenin adresi. Bu alan zorunludur.
        /// </summary>
        public required string Address { get; set; }

        /// <summary>
        /// İşverenin yayınlayabileceği ilan hakkı sayısı. Bu alan zorunludur.
        /// </summary>
        public int JobPostingRightCount { get; set; } = 2;
    }
}
