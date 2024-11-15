﻿namespace CareerNetJob.DataAccess.EntityModels
{
    /// <summary>
    /// İlan Id değeri.
    /// </summary>
    public abstract class BaseEntityModel
    {
        public string Id { get; set; }

        /// <summary>
        /// İlanın yayında kalma süresi.İlan yayınlama tarihinden 15 gün sonrası olacak
        /// </summary>
        public required DateTime ExpireDate { get; set; }

        /// <summary>
        /// İlanın yayınlanma tarihi
        /// </summary>
        public required DateTime PostedDate { get; set; } = DateTime.UtcNow;
    }
}
