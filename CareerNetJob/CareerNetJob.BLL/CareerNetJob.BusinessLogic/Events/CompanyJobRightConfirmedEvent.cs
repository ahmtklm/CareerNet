using CareerNetJob.BusinessLogic.Events.Common;

namespace CareerNetJob.BusinessLogic.Events
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
