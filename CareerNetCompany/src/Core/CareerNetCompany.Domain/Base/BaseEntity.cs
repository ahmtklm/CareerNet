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

        /// <summary>
        /// Oluşturulma tarihi ve saati.
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Güncellenme tarihi ve saati.
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// Soft delete işlemi için entity'nin silinmiş olup olmadığını belirtir.
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
