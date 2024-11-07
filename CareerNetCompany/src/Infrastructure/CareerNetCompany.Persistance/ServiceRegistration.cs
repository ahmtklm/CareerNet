using CareerNetCompany.Application.Extensions;
using CareerNetCompany.Application.Interfaces.Repositories;
using CareerNetCompany.Persistance.Concretes.Repositories;
using CareerNetCompany.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareerNetCompany.Persistance
{
    /// <summary>
    /// Persistence katmanındaki bağımlılıkları DI sistemine eklemek için kullanılan sınıf.
    /// </summary>
    public static class ServiceRegistration
    {

        public static void AddPersistanceServices(this IServiceCollection services,IConfiguration configuration)
        {
            // IRepository ve Repository sınıflarını DI sistemine ekler
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
           
            //DbContext DI sistemine ekler
            services.AddDbContext<CareerNetDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetPostgreSqlConnectionString());
            });
        }
    }
}
