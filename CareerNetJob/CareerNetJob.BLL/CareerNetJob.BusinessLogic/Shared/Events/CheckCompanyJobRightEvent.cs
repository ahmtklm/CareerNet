using CareerNetJob.BusinessLogic.Shared.Events.Common;

namespace CareerNetJob.BusinessLogic.Shared.Events
{
    /// <summary>
    /// Firmanın iş ilanı yayınlama hakkını kontrol etmek için kullanılan event
    /// Firmanın Bilgileri çekilerek ilan yayın hakkı sayısına bakılır
    /// Yayın hakkı varsa 1 azaltılarak Company güncellenir._sendEndpointProvider ile CompanyJobRightConfirmedEvent isimli event oluşturulur.
    /// Yayın hakkı yoksa _publishEndpoint ile CompanyJobRightDeniedEvent isimli even  publish edilir.
    /// </summary>
    public class CheckCompanyJobRightEvent : IEvent
    {
        public Guid CompanyId { get; set; }
        public required string Position { get; set; }
        public required string Description { get; set; }
        public string? Benefits { get; set; }
        public string? EmploymentType { get; set; }
        public int? Salary { get; set; }
    }
}
