using CareerNetCompany.Application.Extensions;
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
            //IRepository DI'ı olacak
            //ICompanyService DI'ı olacak.

            services.AddDbContext<CareerNetDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetPostgreSqlConnectionString());
            });
        }
    }
}
