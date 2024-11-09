using CareerNetCompany.Application.Shared.Events.Common;

namespace CareerNetCompany.Application.Shared.Events
{
    /// <summary>
    /// Firmanın ilan yayınlama hakkı olduğu onaylandığında tetiklenir.
    /// </summary>
    public class CompanyJobRightConfirmedEvent : IEvent
    {
        public Guid CompanyId { get; set; }
        public bool HasJobRight { get; set; }
    }
}
