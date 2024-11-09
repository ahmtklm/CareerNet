using CareerNetJob.BusinessLogic.Events.Common;

namespace CareerNetJob.BusinessLogic.Events
{
    public class CompanyJobRightDeniedEvent : IEvent
    {
        public Guid CompanyId { get; set; }
        public bool HasJobRight { get; set; }
    }
}
