using System.ComponentModel;

namespace VideoContentReviews.BL.Exception;

public class BusinessLogicException : System.Exception
{
    public ResultCode? ResultCode { get; init; }

    public BusinessLogicException()
    {
    }

    public BusinessLogicException(string message) : base(message)
    {
    }

    public BusinessLogicException(ResultCode resultCode) : base(resultCode.ToString())
    {
        ResultCode = resultCode;
    }
    
    public BusinessLogicException(ResultCode resultCode, string message) : base($"{resultCode}: {message}")
    {
        ResultCode = resultCode;
    }
    
    public BusinessLogicException(ResultCode resultCode, string message, bool useDescription = false) 
        : base(useDescription ? GetEnumDescription(resultCode) + ": " + message : $"{resultCode}: {message}")
    {
        ResultCode = resultCode;
    }
    
    private static string GetEnumDescription(ResultCode value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute == null ? value.ToString() : attribute.Description;
    }
}