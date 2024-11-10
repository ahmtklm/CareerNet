using CareerNetCompany.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CareerNetCompany.Application
{
    /// <summary>
    /// Application katmanındaki bağımlılıkları DI (Dependency Injection) sistemine eklemek için kullanılan sınıf.
    /// Bu sınıf, Application katmanındaki servislerin DI'a eklenmesini sağlar.
    /// </summary>
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblyContaining<CompanyCreateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CompanyUpdateDtoValidator>();
        }

    }
}
