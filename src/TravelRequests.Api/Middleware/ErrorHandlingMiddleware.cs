using System.Net;
using System.Text.Json;
using TravelRequests.Application.Exceptions;

namespace TravelRequests.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error no manejado");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = new
        {
            Message = exception.Message,
            Errors = GetErrors(exception)
        };

        switch (exception)
        {
            case ValidationException validationException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse = new { Message = exception.Message, Errors = validationException.Errors };
                break;

            case NotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse = new { Message = exception.Message, Errors = (IDictionary<string, string[]>?)null };
                break;

            case UnauthorizedException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse = new { Message = exception.Message, Errors = (IDictionary<string, string[]>?)null };
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse = new { Message = "Ha ocurrido un error interno del servidor.", Errors = (IDictionary<string, string[]>?)null };
                break;
        }

        var result = JsonSerializer.Serialize(errorResponse);
        await response.WriteAsync(result);
    }

    private static IDictionary<string, string[]>? GetErrors(Exception exception)
    {
        return exception is ValidationException validationException
            ? validationException.Errors
            : null;
    }
} 