using Newtonsoft.Json;
using System.Net;
using FluentValidation;
using CareerNetCompany.Application.Exceptions;

namespace CareerNetCompany.API.Middlewares
{
    /// <summary>
    /// Tüm uygulama genelindeki hataları yakalayarak, anlamlı ve kullanıcı dostu hata mesajları dönen global hata yönetimi middleware'i.
    /// Bu middleware, Fluent Validation hatalarını, özel iş kurallarına bağlı hataları ve beklenmeyen diğer tüm hataları yönetir.
    /// </summary>
    public class CompanyExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public CompanyExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new { message = exception.Message };
            string result;

            switch (exception)
            {
                case ValidationException validationException:
                    // Fluent Validation'dan gelen hatalar
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var errors = new List<string>();
                    foreach (var error in validationException.Errors)
                        errors.Add(error.ErrorMessage);
                    result = JsonConvert.SerializeObject(new { message = "Validation errors", errors });
                    break;

                case KeyNotFoundException:
                    // Veri bulunamadığında dönen hata
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    result = JsonConvert.SerializeObject(response);
                    break;

                case ClientSideException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    result = JsonConvert.SerializeObject(response);
                    break;

                default:
                    // Beklenmeyen tüm hatalar için genel bir yanıt
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result = JsonConvert.SerializeObject(new { message = "An unexpected error occurred." });
                    break;
            }

            return context.Response.WriteAsync(result);
        }
    }
}
