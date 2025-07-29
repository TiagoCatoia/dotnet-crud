using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonApi.Api.Models;

namespace PersonApi.Api.Controllers;

public class ErrorController(ILogger<ErrorController> logger): ControllerBase
{
    [Route("error")]
    [HttpGet]
    public IActionResult HandleError()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error;
        
        logger.LogError(exception, "Unhandled error on path {Path} with TraceId {TraceId}", HttpContext.Request.Path, HttpContext.TraceIdentifier);

        var error = new ApiError
        {
            StatusCode = 500,
            Message = "An unexpected error occurred on the server.",
            Details = exception?.Message,
            TraceId = HttpContext.TraceIdentifier
        };

        return StatusCode(500, error);
    }
}