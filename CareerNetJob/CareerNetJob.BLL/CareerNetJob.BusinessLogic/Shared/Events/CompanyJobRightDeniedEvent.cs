using CareerNetJob.BusinessLogic.Shared.Events.Common;

namespace CareerNetJob.BusinessLogic.Shared.Events
{
    public class CompanyJobRightDeniedEvent : IEvent
    {
        public Guid CompanyId { get; set; }
        public bool HasJobRight { get; set; }
    }
}
