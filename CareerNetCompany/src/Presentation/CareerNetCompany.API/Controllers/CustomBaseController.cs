using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CareerNetCompany.API.Controllers
{
    /// <summary>
    /// Tüm controllerlarda kullanmak için oluşturulmuş base controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        /// <summary>
        /// Ok
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override OkObjectResult Ok(object? value)
        {
            return base.Ok(new
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = value
            });
        }

        /// <summary>
        /// Created
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected IActionResult Created(object? response)
        {
            return new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.Created
            };
        }
    }
}
