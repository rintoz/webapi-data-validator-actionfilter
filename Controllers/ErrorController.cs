using DataValidator.Model;
using DataValidator.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DataValidator.Controllers
{
    [Route("error")]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// DO NOT PROVIDE HTTP VERB FOR THIS ACTION
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public ActionResult<ErrorModel> Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (context == null)
            {
                return null;
            }
            var exception = context.Error;

            logger.LogError(exception, exception.Message);

            if (context.Error.TargetSite.DeclaringType == typeof(DataValidatorActionFilter)
                || context.Error.TargetSite.DeclaringType == typeof(BaseDataValidator))
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorModel { Error = exception?.Message });
            }

            _ = bool.TryParse(HttpContext.Request.Headers["debug"].ToString(), out bool debug);
            return StatusCode((int)HttpStatusCode.InternalServerError, debug ? new ErrorModel(exception) : new ErrorModel { Error = exception?.Message });
        }
    }
}