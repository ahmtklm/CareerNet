using CareerNetJob.BusinessLogic.Shared.Events.Common;

namespace CareerNetJob.BusinessLogic.Shared.Events
{
    /// <summary>
    /// Firmanın ilan yayınlama hakkı olduğu onaylandığında tetiklenir.
    /// Job CreateDto hazırlanır
    /// İlan Kalite skoru hesaplanır
    /// İlan Elastice kaydedilir
    /// </summary>
    public class CompanyJobRightConfirmedEvent : IEvent
    {
        public Guid CompanyId { get; set; }
        public bool HasJobRight { get; set; }
        public required string Position { get; set; }
        public required string Description { get; set; }
        public string? Benefits { get; set; }
        public string? EmploymentType { get; set; }
        public int? Salary { get; set; }
    }
}
