namespace CareerNetCompany.Application.Dtos
{
    /// <summary>
    /// Tüm DTO sınıfları için ortak olan özellikleri tanımlayan temel DTO sınıfı.
    /// </summary>
    public class BaseDto
    {
        /// <summary>
        /// Her bir DTO için benzersiz kimlik numarası.
        /// </summary>
        public Guid Id { get; set; }
    }
}
