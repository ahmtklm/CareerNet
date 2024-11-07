namespace CareerNetJob.DataAccess.EntityModels
{
    /// <summary>
    /// İlan bilgilerini tutacak olan sınıf.ElasticSearch tarafında kullanılacak.
    /// </summary>
    public class Job : BaseEntityModel
    {
        /// <summary>
        /// İlanı yayınlayan Firma Id bilgisi.
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Pozisyon.
        /// </summary>
        public required string Position { get; set; }

        /// <summary>
        /// İlan Açıklaması
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// İlanın yayınlanma tarihi
        /// </summary>
        public required DateTime PostedDate  { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// İlan Kalite Skoru
        /// </summary>
        public int? QualityScore { get; set; }

        /// <summary>
        /// Yan Haklar-Opsiyonel alan
        /// </summary>
        public string? Benefits { get; set; }

        /// <summary>
        /// Çalışma Türü-Tam zamanı,yarı zamanlı vs.. Opsiyonel alan
        /// </summary>
        public string? EmploymentType { get; set; }

        /// <summary>
        /// Ücret,Maaş-Opsiyonel alan
        /// </summary>
        public int? Salary { get; set; }

    }
}
