using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla.MagicApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("ErrorHandling")]
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorHandlingController : ControllerBase
    {
        [HttpGet("ProcessError")]
        [HttpGet]
        public IActionResult HandleError([FromServices] IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
                return Problem(
                    detail: feature.Error.StackTrace,
                    title: feature.Error.Message,
                    instance: environment.EnvironmentName
);

            }
            else
            {
                return Problem();

            }
            // Access exception details from HttpContext
            //var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            //// Log the exception details (replace with your logging mechanism)

            //// Determine appropriate HTTP status code based on exception type
            //var statusCode = exception is ValidationException ? 400 : 500;

            //// Create a ProblemDetails object with error information
            //var problemDetails = new ProblemDetails
            //{
            //    Status = statusCode,
            //    Title = "Internal Server Error",
            //    Detail = exception?.Message,
            //    Instance = HttpContext.Request.Path
            //};

            //return new ObjectResult(problemDetails)
            //{
            //    StatusCode = statusCode
            //};

        }
    }
}
