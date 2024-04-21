using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.API.Exceptions.Handlers;

internal class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            "Error Message: {exceptionMessage}, Occurred at:{time}"
            , exception.Message, DateTime.UtcNow);

        ProblemDetails problemDetails = new()
        {
            Title = exception.GetType().Name,
            Detail = exception.Message,
            Status = GetStatusCodeFromException(exception),
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
        }

        httpContext.Response.StatusCode = (int)problemDetails.Status;
            
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }

    private int GetStatusCodeFromException(Exception exception) =>
        exception switch
        {
            InternalServerException => StatusCodes.Status500InternalServerError,
            ValidationException => StatusCodes.Status400BadRequest,
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

}
