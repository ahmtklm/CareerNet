using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CareerNetCompany.API.Middlewares
{
    /// <summary>
    /// ModelState bazında alınan hataları yakalayan filter.
    /// </summary>
    public class ValidationFilter : IAsyncActionFilter
    {
        /// <summary>
        /// Action olduğunda ModelState üzerindeki hataları yakalar.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Errors = errors
                });
                return;
            }
            await next();
        }
    }
}
