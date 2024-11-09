using CareerNetJob.BusinessLogic.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace CareerNetJob.API.Middlewares
{
    /// <summary>
    /// Tüm uygulama genelindeki hataları yakalayarak, anlamlı ve kullanıcı dostu hata mesajları dönen global hata yönetimi middleware'i.
    /// </summary>
    public class JobExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        public JobExceptionHandlingMiddleware(RequestDelegate next)
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
                NotFoundException => (int)HttpStatusCode.NotFound,
                ClientSideException => (int)HttpStatusCode.Conflict,
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
