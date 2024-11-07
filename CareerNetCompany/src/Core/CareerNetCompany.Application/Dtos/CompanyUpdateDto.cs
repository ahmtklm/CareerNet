namespace CareerNetCompany.Application.Dtos
{
    /// <summary>
    /// Mevcut bir firmanın kaydını güncellemek için gerekli veri transfer yapısı.
    /// </summary>
    public class CompanyUpdateDto : BaseDto
    {
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public int JobPostingRightCount { get; set; }
    }
}
