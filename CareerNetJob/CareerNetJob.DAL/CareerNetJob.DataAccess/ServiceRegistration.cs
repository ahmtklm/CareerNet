using CareerNetJob.DataAccess.Clients.Abstractions;
using CareerNetJob.DataAccess.Clients.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace CareerNetJob.DataAccess
{
    public static class ServiceRegistration
    {
        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IElasticSearchClientRepo<>), typeof(ElasticSearchClientRepo<>));
        }
    }
}
