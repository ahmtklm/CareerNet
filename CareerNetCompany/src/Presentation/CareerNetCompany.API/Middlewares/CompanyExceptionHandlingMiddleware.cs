using Newtonsoft.Json;
using System.Net;
using FluentValidation;
using CareerNetCompany.Application.Exceptions;

namespace CareerNetCompany.API.Middlewares
{
    /// <summary>
    /// Tüm uygulama genelindeki hataları yakalayarak, anlamlı ve kullanıcı dostu hata mesajları dönen global hata yönetimi middleware'i.
    /// </summary>
    public class CompanyExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        public CompanyExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Methodun çalıştığı kısım.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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
            context.Response.StatusCode = exception switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                ConflictException => (int)HttpStatusCode.Conflict,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            var response = new
            {
                error = exception.Message,
                statusCode = context.Response.StatusCode
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
