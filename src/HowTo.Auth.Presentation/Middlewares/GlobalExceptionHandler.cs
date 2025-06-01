namespace HowTo.Auth.Presentation.Middlewares;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;

        await HandleException(exception, httpContext, cancellationToken);

        return true;
    }
    private async Task HandleException(Exception exception, HttpContext httpContext, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unexpected error occurred: {exceptionMessage}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new ProblemDetails
        {
            Title = "An unexpected error occurred. Please, try again later.",
            Status = StatusCodes.Status500InternalServerError
        };

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
    }
}
