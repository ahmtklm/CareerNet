using CareerNetCompany.Application.Extensions;
using CareerNetCompany.Application.Interfaces.Company;
using CareerNetCompany.Application.Interfaces.Repositories;
using CareerNetCompany.Persistance.Concretes.Companies;
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
    public static class DependencyRegistry
    {
        public static void RegisterPersistanceServices(this IServiceCollection services,IConfiguration configuration)
        {
            // IRepository ve Repository sınıflarını DI sistemine ekler
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //ICompanyService sınıfını DI sistemine ekler
            services.AddScoped<ICompanyService, CompanyService>();

            //DbContext DI sistemine ekler
            services.AddDbContext<CareerNetDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetPostgreSqlConnectionString());
            });
        }
    }
}
