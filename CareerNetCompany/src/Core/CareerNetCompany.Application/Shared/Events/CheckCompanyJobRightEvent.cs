using CareerNetCompany.Application.Shared.Events.Common;

namespace CareerNetCompany.Application.Shared.Events
{
    /// <summary>
    /// Firmanın iş ilanı yayınlama hakkını kontrol etmek için kullanılan event.CompanyId bilgisini kullanır İş ilanı yayınlanmadan önce tetiklenir.
    /// </summary>
    public class CheckCompanyJobRightEvent : IEvent
    {
        /// <summary>
        /// Firma Id Bilgisi.
        /// </summary>
        public Guid CompanyId { get; set; }
    }
}
