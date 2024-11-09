using CareerNetJob.BusinessLogic.Abstractions;
using CareerNetJob.BusinessLogic.AutoMappings;
using CareerNetJob.BusinessLogic.Concretes;
using CareerNetJob.BusinessLogic.Validations;
using CareerNetJob.DataAccess;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareerNetJob.BusinessLogic
{
    public static class ServiceRegistration
    {
        public static void AddBusinessLogicServices(this IServiceCollection services)
        {
            //AutoMapper
            services.AddAutoMapper(typeof(JobMappingProfile));
            //IJobService
            services.AddScoped<IJobService,JobService>();
            //DataAccess katmanındaki DI' kısımlarını burada çağırıyorum. API katmanında direkt data accesse bağımlı olmaması için
            services.AddDataAccessServices();
            //Fluent Validator
            services.AddValidatorsFromAssemblyContaining<JobCreateDtoValidator>();
        }

        //Elastic Search Client IServiceCollection'a ekler.
        public static void AddElasticClientServices(this IServiceCollection services,IConfiguration configuration)
        {
            var userName = configuration.GetSection("ElasticSearchSettings")["Username"];

            var password = configuration.GetSection("ElasticSearchSettings")["Password"];

            var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("ElasticSearchSettings")["Host"]!)).Authentication(new BasicAuthentication(userName!, password!));

            var client = new ElasticsearchClient(settings);

            services.AddSingleton(client);
        }
    }
}
