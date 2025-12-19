using AutoMapper;
using Microsoft.Extensions.Logging;
using VideoContentReviews.BL.Common.Mappers;

namespace VideoContentReviews.BL.UnitTests.Mappers;

public static class MapperHelper
{
    public static IMapper Mapper;

    static MapperHelper()
    {
        ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var config = new MapperConfiguration(x =>
            {
                x.AddProfile<VideoContentBLProfile>();
            },
            loggerFactory);
        Mapper = config.CreateMapper();
    }
}