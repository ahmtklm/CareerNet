namespace CareerNetJob.DataAccess.EntityModels
{
    /// <summary>
    /// İlan Id değeri.
    /// </summary>
    public abstract class BaseEntityModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// İlanın yayında kalma süresi.İlan yayınlama tarihinden 15 gün sonrası olacak
        /// </summary>
        public required DateTime ExpireDate { get; set; }
    }
}
