using CareerNetJob.DataAccess.Clients.Abstractions;
using CareerNetJob.DataAccess.Clients.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace CareerNetJob.DataAccess
{
    public static class DependencyRegistry
    {
        public static void RegisterDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IElasticSearchClientRepo<>), typeof(ElasticSearchClientRepo<>));
        }
    }
}
