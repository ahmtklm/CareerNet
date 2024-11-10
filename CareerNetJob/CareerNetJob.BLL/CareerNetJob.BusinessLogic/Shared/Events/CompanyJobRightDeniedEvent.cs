﻿using CareerNetJob.BusinessLogic.Shared.Events.Common;

namespace CareerNetJob.BusinessLogic.Shared.Events
{
    /// <summary>
    /// Firmanın ilan yayınlama hakkı olmadığı durumda tetiklenir.
    /// İlan yayınlanmaz.
    /// Exception döner.
    /// </summary>
    public class CompanyJobRightDeniedEvent : IEvent
    {
        public Guid CompanyId { get; set; }
        public bool HasJobRight { get; set; }
    }
}
