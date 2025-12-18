using System.ComponentModel;
using System.Net;

namespace VideoContentReviews.Service.Exceptions;

public class ServiceException : Exception
{
    public ServiceErrorCode? ErrorCode { get; init; }
    public HttpStatusCode? HttpStatusCode { get; init; }

    public ServiceException()
    {
    }

    public ServiceException(string message) : base(message)
    {
    }

    public ServiceException(ServiceErrorCode errorCode) : base(errorCode.ToString())
    {
        ErrorCode = errorCode;
    }

    public ServiceException(ServiceErrorCode errorCode, string message)
        : base($"{errorCode}: {message}")
    {
        ErrorCode = errorCode;
    }

    public ServiceException(HttpStatusCode httpStatusCode, string message)
        : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }

    public ServiceException(ServiceErrorCode errorCode, HttpStatusCode httpStatusCode, string message)
        : base($"{errorCode}: {message}")
    {
        ErrorCode = errorCode;
        HttpStatusCode = httpStatusCode;
    }

    public ServiceException(ServiceErrorCode errorCode, string message, bool useDescription = false)
        : base(useDescription ? GetEnumDescription(errorCode) + ": " + message : $"{errorCode}: {message}")
    {
        ErrorCode = errorCode;
    }

    private static string GetEnumDescription(ServiceErrorCode value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute == null ? value.ToString() : attribute.Description;
    }
}