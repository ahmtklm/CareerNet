using CareerNetCompany.Domain.Base;
using CareerNetCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareerNetCompany.Persistance.Context
{
    /// <summary>
    /// PostgreSQL veritabanı bağlantısını ve Company tablosunu yöneten DbContext sınıfı.
    /// </summary>
    public class CareerNetDbContext : DbContext
    {
        public CareerNetDbContext(DbContextOptions<CareerNetDbContext> options):base(options){}

        /// <summary>
        /// Firma bilgilerini tutan Company tablosu.
        /// </summary>
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                // Firma Adı zorunlu ve maksimum 100 karakter
                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                // Telefon numarası zorunlu,benzersiz
                entity.Property(c => c.PhoneNumber)
                    .IsRequired();

                entity.HasIndex(c => c.PhoneNumber)
                    .IsUnique();

                // Adres zorunlu ve maksimum 250 karakter
                entity.Property(c => c.Address)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Değişiklikleri veritabanına kaydederken tarih bilgilerini otomatik ayarlamak için base olan SaveChanges metodunu ezecektir.
        /// </summary>
        public override int SaveChanges()
        {
            SetAuditInformation();
            return base.SaveChanges();
        }

        /// <summary>
        /// Asenkron olarak değişiklikleri veritabanına kaydederken tarih bilgilerini otomatik ayarlamak için SaveChangesAsync metodunu ezecektir.
        /// </summary>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditInformation();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Oluşturulma ve güncelleme tarihlerini otomatik olarak ayarlar.
        /// </summary>
        private void SetAuditInformation()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity entity)
                {
                    if (entry.State == EntityState.Added)
                        entity.CreateDate = DateTime.Now;
                    else if (entry.State == EntityState.Modified)
                        entity.UpdateDate = DateTime.Now;
                }
            }
        }

    }
}
