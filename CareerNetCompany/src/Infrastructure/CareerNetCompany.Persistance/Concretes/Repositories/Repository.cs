using CareerNetCompany.Application.Interfaces.Repositories;
using CareerNetCompany.Domain.Base;
using CareerNetCompany.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CareerNetCompany.Persistance.Concretes.Repositories
{
    /// <summary>
    /// IRepository arayüzünü implement eden, veri erişim işlemlerini gerçekleştiren genel Repository sınıfı.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T: BaseEntity
    {
        private readonly CareerNetDbContext _context;
        public Repository(CareerNetDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var dbSet = _context.Set<T>();

            entity.CreateDate = dbSet.Where(x => x.Id == entity.Id).Select(z => z.CreateDate).FirstOrDefault();

            try
            {
                dbSet.Attach(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                dbSet.Update(entity);
            }

            await _context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>>? expression = null)
        {
            return expression == null ? _context.Set<T>() : _context.Set<T>().Where(expression);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }
    }
}
