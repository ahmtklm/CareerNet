namespace CareerNetCompany.Domain.Base
{
    /// <summary>
    /// BaseEntity sınıfı, tüm entity sınıflarında ortak olan özellikleri tanımlar.
    /// Diğer entity sınıflarının sadece kalıtım alması için abstract olarak tanımlanmıştır.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Tüm entity'ler için benzersiz kimlik numarası. GUID formatında tutulur.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();


        private DateTime _createDate;

        /// <summary>
        /// Oluşturulma tarihi ve saati.
        /// </summary>
        public DateTime CreateDate
        {
            get => _createDate;
            set => _createDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        private DateTime? _updateDate;

        /// <summary>
        /// Güncellenme tarihi ve saati.
        /// </summary>
        public DateTime? UpdateDate
        {
            get => _updateDate;
            set => _updateDate = DateTime.SpecifyKind(value!.Value, DateTimeKind.Utc);
        }
    }
}
