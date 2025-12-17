
using System.ComponentModel;
using System.Net;

namespace VideoContentReviews.Service.Exceptions;

public enum ServiceErrorCode
{
    [Description("Invalid image format.")]
    InvalidImageFormat = 1000,
}