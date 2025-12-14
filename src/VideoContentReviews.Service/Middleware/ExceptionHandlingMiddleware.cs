using System.Net;
using System.Text.Json;
using VideoContentReviews.BL.User.Exception;

namespace VideoContentReviews.Service.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BusinessLogicException ex)
        {
            await HandleBusinessLogicExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleGenericExceptionAsync(context, ex);
        }
    }

    private static async Task HandleBusinessLogicExceptionAsync(HttpContext context, BusinessLogicException exception)
    {
        var statusCode = exception.ResultCode switch
        {
            ResultCode.UserNotFound => HttpStatusCode.NotFound,
            ResultCode.UserAlreadyExists => HttpStatusCode.Conflict,
            ResultCode.EmailOrPasswordIsIncorrect => HttpStatusCode.Unauthorized,
            ResultCode.ValidationError => HttpStatusCode.BadRequest,
            ResultCode.IdentityServerError => HttpStatusCode.InternalServerError,
            ResultCode.UserCreationFailure => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.BadRequest
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            StatusCode = (int)statusCode,
            ErrorCode = exception.ResultCode?.ToString() ?? "Unknown",
            Message = exception.Message,
            ResultCode = (int?)exception.ResultCode
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private async Task HandleGenericExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError(exception, "Unhandled exception occurred");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = "An internal server error occurred",
            Details = context.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment() 
                ? exception.Message 
                : null
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}