using CareerNetCompany.Application.Shared.Events.Common;

namespace CareerNetCompany.Application.Shared.Events
{
    /// <summary>
    /// Elastic Search'e ilan eklenirken bir hata alma durumunda tetiklenecek Event.
    /// Firmanın İlan yayınlama hakkı bir artırılmalıdır.Company'de güncellenir.
    /// </summary>
    public class HasExceptionJobCreateEvent : IEvent
    {
        public Guid CompanyId { get; set; }
    }
}
