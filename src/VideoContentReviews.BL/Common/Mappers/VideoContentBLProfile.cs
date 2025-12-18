using AutoMapper;
using VideoContentReviews.BL.Features.VideoContent.DTOs;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.BL.Common.Mappers;

public class VideoContentBLProfile : Profile
{
    public VideoContentBLProfile()
    {
        CreateMap<VideoContentEntity, VideoContentModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.YearOfRelease, opt => opt.MapFrom(src => src.YearOfRelease))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.UserAverageRating, opt => opt.MapFrom(src => src.UserAverageRating))
            .ForMember(dest => dest.TypeOfContentId,
                opt => opt.MapFrom(src => src.TypeOfContentEntity.ExternalId))
            .ForMember(dest => dest.DirectorId,
                opt => opt.MapFrom(src => src.DirectorEntity.ExternalId))
            .ForMember(dest => dest.ImageId,
                opt => opt.MapFrom(src => src.ImageEntity.ExternalId))
            .ForMember(dest => dest.TypeOfContentName,
                opt => opt.MapFrom(src => src.TypeOfContentEntity.Title))
            .ForMember(dest => dest.DirectorFullName,
                opt => opt.MapFrom(src => $"{src.DirectorEntity.FirstName} {src.DirectorEntity.LastName}"))
            .ForMember(dest => dest.ImageFileName,
                opt => opt.MapFrom(src => src.ImageEntity.FileName))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
                src.VideoContentsGenres.Select(vcg => vcg.GenreEntity).ToList()))
            .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.CreationTime))
            .ForMember(dest => dest.ModificationTime, opt => opt.MapFrom(src => src.ModificationTime));

        CreateMap<CreateVideoContentModel, VideoContentEntity>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.YearOfRelease, opt => opt.MapFrom(src => src.YearOfRelease))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.UserAverageRating, opt => opt.MapFrom(_ => 0.0))
            .ForMember(dest => dest.TypeOfContentId, opt => opt.Ignore())
            .ForMember(dest => dest.DirectorId, opt => opt.Ignore())
            .ForMember(dest => dest.ImageId, opt => opt.Ignore())
            .ForMember(dest => dest.TypeOfContentEntity, opt => opt.Ignore())
            .ForMember(dest => dest.DirectorEntity, opt => opt.Ignore())
            .ForMember(dest => dest.ImageEntity, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Favourites, opt => opt.Ignore())
            .ForMember(dest => dest.VideoContentsGenres, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalId, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.ModificationTime, opt => opt.Ignore());
    }
}