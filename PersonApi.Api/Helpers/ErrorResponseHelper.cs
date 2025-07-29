using Microsoft.AspNetCore.Mvc;
using PersonApi.Api.Models;

namespace PersonApi.Api.Helpers;

public class ErrorResponseHelper
{
    public static IActionResult NotFound(string? message, HttpContext context) =>
        CreateResponse(404, message ?? "Resource not found.", context);

    public static IActionResult BadRequest(string? message, HttpContext context) =>
        CreateResponse(400, message ?? "Invalid request.", context);

    public static IActionResult Conflict(string? message, HttpContext context) =>
        CreateResponse(409, message ?? "Conflict while processing the request.", context);

    public static IActionResult Internal(string? message, string? detail, HttpContext context) =>
        new ObjectResult(new ApiError
        {
            StatusCode = 500,
            Message = message ?? "Unexpected server error.",
            Details = detail,
            TraceId = context.TraceIdentifier
        })
        {
            StatusCode = 500
        };

    private static ObjectResult CreateResponse(int statusCode, string message, HttpContext context)
    {
        var error = new ApiError
        {
            StatusCode = statusCode,
            Message = message,
            TraceId = context.TraceIdentifier
        };

        return new ObjectResult(error)
        {
            StatusCode = statusCode
        };
    }
}