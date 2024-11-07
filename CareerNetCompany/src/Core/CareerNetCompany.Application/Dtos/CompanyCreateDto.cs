namespace CareerNetCompany.Application.Dtos
{
    /// <summary>
    /// Yeni Firma kaydı oluşturmak için gerekli veri transfer sınıfı.
    /// </summary>
    public class CompanyCreateDto
    {
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public int JobPostingRightCount { get; set; } = 2;
    }
}
