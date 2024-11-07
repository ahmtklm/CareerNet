namespace CareerNetCompany.Application.Dtos
{
    /// <summary>
    /// Firma verilerini içeren veri transfer sınıfı.
    /// </summary>
    public class CompanyDto : BaseDto
    {
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public int JobPostingRightCount { get; set; }
    }
}
