using AutoMapper;
using VideoContentReviews.BL.Images.Entities;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.BL.Mappers;

public class ImageBLProfile : Profile
{
    public ImageBLProfile()
    {
        CreateMap<ImageEntity, ImageModel>()
            .ForMember(dest => dest.ExternalId,
                opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.FileExtension,
                opt => opt.MapFrom(src => src.FileExtension))
            .ForMember(dest => dest.FileName,
                opt => opt.MapFrom(src => src.FileName));

        CreateMap<CreateImageModel, ImageEntity>()
            .ForMember(dest => dest.FileName,
                opt => opt.MapFrom(src => src.FileName))
            .ForMember(dest => dest.FileExtension,
                opt => opt.MapFrom(src => src.FileExtension));
    }
}