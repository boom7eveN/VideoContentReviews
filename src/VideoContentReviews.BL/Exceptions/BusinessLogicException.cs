using System.ComponentModel;

namespace VideoContentReviews.BL.Exceptions;

public class BusinessLogicException : System.Exception
{
    public BLResultCode? BlResultCode { get; init; }

    public BusinessLogicException()
    {
    }

    public BusinessLogicException(string message) : base(message)
    {
    }

    public BusinessLogicException(BLResultCode blResultCode) : base(blResultCode.ToString())
    {
        BlResultCode = blResultCode;
    }
    
    public BusinessLogicException(BLResultCode blResultCode, string message) : base($"{blResultCode}: {message}")
    {
        BlResultCode = blResultCode;
    }
    
    public BusinessLogicException(BLResultCode blResultCode, string message, bool useDescription = false) 
        : base(useDescription ? GetEnumDescription(blResultCode) + ": " + message : $"{blResultCode}: {message}")
    {
        BlResultCode = blResultCode;
    }
    
    private static string GetEnumDescription(BLResultCode value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute == null ? value.ToString() : attribute.Description;
    }
}