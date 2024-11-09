using CareerNetCompany.Application.Shared.Events.Common;

namespace CareerNetCompany.Application.Shared.Events
{
    /// <summary>
    /// Firmanın ilan yayınlama hakkı olmadığında tetiklenir.
    /// </summary>
    public class CompanyJobRightDeniedEvent : IEvent
    {
        public Guid CompanyId { get; set; }
        public bool HasJobRight { get; set; }
    }
}
