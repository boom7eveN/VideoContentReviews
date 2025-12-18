using AutoMapper;
using VideoContentReviews.BL.TypesOfContent.Entities;
using VideoContentReviews.BL.VideoContent.Entities;
using VideoContentReviews.Service.Controllers.TypeOfContent.DTOs.Requests;
using VideoContentReviews.Service.Controllers.TypeOfContent.DTOs.Responses;
using VideoContentReviews.Service.Controllers.VideoContent.Requests;
using VideoContentReviews.Service.Controllers.VideoContent.Responses;

namespace VideoContentReviews.Service.Mapper;

public class VideoContentServiceProfile : Profile
{
    public VideoContentServiceProfile()
    {
        CreateMap<CreateVideoContentRequest, CreateVideoContentModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.YearOfRelease, opt => opt.MapFrom(src => src.YearOfRelease))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.TypeOfContentExternalId,
                opt => opt.MapFrom(src => src.TypeOfContentId))
            .ForMember(dest => dest.DirectorExternalId,
                opt => opt.MapFrom(src => src.DirectorId))
            .ForMember(dest => dest.ImageExternalId,
                opt => opt.MapFrom(src => src.ImageId))
            .ForMember(dest => dest.GenreExternalIds,
                opt => opt.MapFrom(src => src.GenreIds));

        CreateMap<VideoContentModel, VideoContentResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.YearOfRelease, opt => opt.MapFrom(src => src.YearOfRelease))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.UserAverageRating, opt => opt.MapFrom(src => src.UserAverageRating))
            .ForMember(dest => dest.TypeOfContentId, opt => opt.MapFrom(src => src.TypeOfContentId))
            .ForMember(dest => dest.TypeOfContentName, opt => opt.MapFrom(src => src.TypeOfContentName))
            .ForMember(dest => dest.DirectorId, opt => opt.MapFrom(src => src.DirectorId))
            .ForMember(dest => dest.DirectorFullName, opt => opt.MapFrom(src => src.DirectorFullName))
            .ForMember(dest => dest.ImageId, opt => opt.MapFrom(src => src.ImageId))
            .ForMember(dest => dest.ImageFileName, opt => opt.MapFrom(src => src.ImageFileName))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
            .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.CreationTime))
            .ForMember(dest => dest.ModificationTime, opt => opt.MapFrom(src => src.ModificationTime));

        CreateMap<List<VideoContentModel>, VideoContentListResponse>()
            .ForMember(dest => dest.VideoContentResponses, opt => opt.MapFrom(src => src));
    }
}