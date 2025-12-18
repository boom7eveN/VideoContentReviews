using AutoMapper;
using VideoContentReviews.BL.Images.Entities;
using VideoContentReviews.DataAccess.Entities.Primitives;
using VideoContentReviews.Service.Controllers.Images.DTOs.Requests;
using VideoContentReviews.Service.Controllers.Images.DTOs.Responses;
using VideoContentReviews.Service.Exceptions;

namespace VideoContentReviews.Service.Mapper;

public class ImageServiceProfile : Profile
{
    public ImageServiceProfile()
    {
        CreateMap<CreateImageRequest, CreateImageModel>()
            .ForMember(dest => dest.FileExtension,
                opt => opt.MapFrom(src => ParseImageFormat(src.Extension)));
        CreateMap<ImageModel, ImageResponse>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.FilenameWithExtension,
                opt => opt.MapFrom(src => $"{src.FileName}.{src.FileExtension.ToString().ToLower()}"));
    }

    internal static ImageFormat ParseImageFormat(string extension)
    {
        if (Enum.TryParse<ImageFormat>(extension, true, out var result))
        {
            return result;
        }

        throw new ServiceException(ServiceErrorCode.InvalidImageFormat);
    }
}