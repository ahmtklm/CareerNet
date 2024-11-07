using CareerNetCompany.Domain.Base;
using System.Linq.Expressions;

namespace CareerNetCompany.Application.Interfaces.Repositories
{
    /// <summary>
    /// Tüm entity’ler için genel veri erişim işlemleri sağlayan IRepository arayüzü.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        //Tüm entity kayıtlarını asenkron olarak döndürür.
        Task<IEnumerable<T>> GetAllAsync();

        //Belirli bir şarta uyan tüm entity kayıtlarını asenkron olarak döndürür.
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);

        //Belirtilen id'ye sahip tek bir entity kaydını asenkron olarak döndürür.
        Task<T> GetByIdAsync(Guid id);

        //Belirli bir şarta uyan tek bir entity kaydını asenkron olarak döndürür.
        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        //Belirli bir şarta göre entity kayıtlarını sorgulamak için IQueryable döndürür.
        IQueryable<T> Where(Expression<Func<T, bool>>? expression = null);

        //Ekleme
        Task<T> AddAsync(T entity);

        //Güncelleme
        Task<T> UpdateAsync(T entity);

        //Mevcut bir entity kaydını veritabanından siler.
        Task DeleteAsync(T entity);

        //Verilen kriterlere ait veri var mı yok mu bilgisini döner
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    }
}
