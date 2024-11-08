using CareerNetJob.BusinessLogic.Abstractions;
using CareerNetJob.BusinessLogic.AutoMappings;
using CareerNetJob.BusinessLogic.Concretes;
using CareerNetJob.BusinessLogic.Validations;
using CareerNetJob.DataAccess;
using FluentValidation;
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
    }
}
