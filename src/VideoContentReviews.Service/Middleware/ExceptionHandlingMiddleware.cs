using System.Net;
using System.Text.Json;
using AutoMapper;
using VideoContentReviews.BL.Exceptions;
using VideoContentReviews.Service.Exceptions;

namespace VideoContentReviews.Service.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (AutoMapperMappingException ex) when (ex.InnerException is ServiceException serviceEx)
        {
            await HandleServiceExceptionAsync(context, serviceEx);
        }
        catch (BusinessLogicException ex)
        {
            await HandleBusinessLogicExceptionAsync(context, ex);
        }
        catch (ServiceException ex)
        {
            await HandleServiceExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleGenericExceptionAsync(context, ex);
        }
    }

    private static async Task HandleBusinessLogicExceptionAsync(
        HttpContext context,
        BusinessLogicException exception)
    {
        var statusCode = exception.BlResultCode switch
        {
            BLResultCode.UserNotFound => HttpStatusCode.NotFound,
            BLResultCode.UserAlreadyExists => HttpStatusCode.Conflict,
            BLResultCode.EmailOrPasswordIsIncorrect => HttpStatusCode.Unauthorized,
            BLResultCode.ValidationError => HttpStatusCode.BadRequest,
            BLResultCode.IdentityServerError => HttpStatusCode.InternalServerError,
            BLResultCode.UserCreationFailure => HttpStatusCode.BadRequest,
            BLResultCode.VideoContentNotFound => HttpStatusCode.NotFound,
            BLResultCode.TypeOfContentNotFound => HttpStatusCode.NotFound,
            BLResultCode.DirectorNotFound => HttpStatusCode.NotFound,
            BLResultCode.DirectorAlreadyExists => HttpStatusCode.Conflict,
            BLResultCode.TypeOfContentAlreadyExists => HttpStatusCode.Conflict,
            BLResultCode.ImageAlreadyExists => HttpStatusCode.Conflict,
            _ => HttpStatusCode.BadRequest
        };

        await WriteErrorResponseAsync(context, statusCode, exception.BlResultCode?.ToString(), exception.Message);
    }

    private static async Task HandleServiceExceptionAsync(
        HttpContext context,
        ServiceException exception)
    {
        var statusCode = exception.HttpStatusCode
                         ?? (exception.ErrorCode.HasValue
                             ? (HttpStatusCode)exception.ErrorCode.Value
                             : HttpStatusCode.InternalServerError);

        await WriteErrorResponseAsync(
            context,
            statusCode,
            exception.ErrorCode?.ToString(),
            exception.Message,
            (int?)exception.ErrorCode);
    }

    private async Task HandleGenericExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        logger.LogError(exception, "Unhandled exception occurred");

        var response = new
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = "An internal server error occurred",
            Details = context.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment()
                ? exception.Message
                : null,
            StackTrace = context.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment()
                ? exception.StackTrace
                : null
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static async Task WriteErrorResponseAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string? errorCode,
        string message,
        int? numericErrorCode = null)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            StatusCode = (int)statusCode,
            ErrorCode = errorCode ?? statusCode.ToString(),
            Message = message,
            NumericErrorCode = numericErrorCode
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}