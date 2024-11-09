namespace CareerNetJob.BusinessLogic.Events
{
    /// <summary>
    /// Firmanın iş ilanı yayınlama hakkını kontrol etmek için kullanılan event.CompanyId bilgisini kullanır İş ilanı yayınlanmadan önce tetiklenir.
    /// </summary>
    public class CheckCompanyJobRightEvent
    {
        /// <summary>
        /// Firma Id Bilgisi.
        /// </summary>
        public Guid CompanyId { get; set; }
    }
}
