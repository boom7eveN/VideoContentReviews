using Moq;
using VideoContentReviews.BL.Common.Exceptions;
using VideoContentReviews.BL.Features.VideoContent.Providers;
using VideoContentReviews.BL.UnitTests.Mappers;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.DataAccess.Entities.Primitives;
using VideoContentReviews.DataAccess.Repositories.VideoContentRepository;

namespace VideoContentReviews.BL.UnitTests.VideoContent;

[TestFixture]
public class VideoContentProviderTests
{
    private VideoContentProvider _videoContentProvider;
    private Mock<IVideoContentRepository> _videoContentRepositoryMock;
    private List<TypeOfContentEntity> _typesOfContent;
    private List<DirectorEntity> _directors;
    private List<ImageEntity> _images;
    private List<GenreEntity> _genres;
    private List<VideoContentGenreEntity> _videoContentGenres;
    private List<VideoContentEntity> _videoContent;

    [SetUp]
    public void Setup()
    {
        _videoContentRepositoryMock = new Mock<IVideoContentRepository>();
        _videoContentProvider = new VideoContentProvider(_videoContentRepositoryMock.Object, MapperHelper.Mapper);
        _images =
        [
            new ImageEntity
            {
                Id = 1,
                ExternalId = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                CreationTime = DateTime.UtcNow.AddDays(-80),
                ModificationTime = DateTime.UtcNow.AddDays(-7),
                FileName = "WTF",
                FileExtension = ImageFormat.WebP
            },

            new ImageEntity
            {
                Id = 2,
                ExternalId = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                CreationTime = DateTime.UtcNow.AddDays(-75),
                ModificationTime = DateTime.UtcNow.AddDays(-6),
                FileName = "REAL_BULLSHIT4K",
                FileExtension = ImageFormat.Png
            }
        ];
        _typesOfContent =
        [
            new TypeOfContentEntity
            {
                Id = 1,
                ExternalId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                CreationTime = DateTime.UtcNow.AddDays(-100),
                ModificationTime = DateTime.UtcNow.AddDays(-10),
                Title = "Movie",
            },

            new TypeOfContentEntity
            {
                Id = 2,
                ExternalId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                CreationTime = DateTime.UtcNow.AddDays(-90),
                ModificationTime = DateTime.UtcNow.AddDays(-9),
                Title = "TV Series",
            }
        ];

        _directors =
        [
            new DirectorEntity
            {
                Id = 1,
                ExternalId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                CreationTime = DateTime.UtcNow.AddDays(-120),
                ModificationTime = DateTime.UtcNow.AddDays(-15),
                FirstName = "Glad",
                LastName = "Valakas",
                Patronymic = "Gadzovich"
            },

            new DirectorEntity
            {
                Id = 2,
                ExternalId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                CreationTime = DateTime.UtcNow.AddDays(-110),
                ModificationTime = DateTime.UtcNow.AddDays(-12),
                FirstName = "Valeri",
                LastName = "Zhmishenko",
                Patronymic = "Albertovich"
            }
        ];

        _genres =
        [
            new GenreEntity
            {
                Id = 1,
                ExternalId = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                CreationTime = DateTime.UtcNow.AddDays(-150),
                ModificationTime = DateTime.UtcNow.AddDays(-20),
                Title = "NewGadzaGenre"
            },

            new GenreEntity
            {
                Id = 2,
                ExternalId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                CreationTime = DateTime.UtcNow.AddDays(-145),
                ModificationTime = DateTime.UtcNow.AddDays(-19),
                Title = "OldGadzaGenre"
            }
        ];

        _videoContent =
        [
            new VideoContentEntity
            {
                Id = 1,
                ExternalId = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000001"),
                CreationTime = DateTime.UtcNow.AddDays(-60),
                ModificationTime = DateTime.UtcNow.AddDays(-4),
                Name = "GLACK TWO",
                YearOfRelease = 2010,
                Description = "NO",
                UserAverageRating = 4.9,
                TypeOfContentId = 1,
                DirectorId = 1,
                ImageId = 1,
                TypeOfContentEntity = _typesOfContent[0],
                DirectorEntity = _directors[0],
                ImageEntity = _images[0]
            },


            new VideoContentEntity
            {
                Id = 2,
                ExternalId = Guid.Parse("bbbbbbbb-0000-0000-0000-000000000002"),
                CreationTime = DateTime.UtcNow.AddDays(-55),
                ModificationTime = DateTime.UtcNow.AddDays(-3),
                Name = "CHUPEP: back of the legend",
                YearOfRelease = 1994,
                Description = "-.",
                UserAverageRating = 5,
                TypeOfContentId = 1,
                DirectorId = 2,
                ImageId = 2,
                TypeOfContentEntity = _typesOfContent[1],
                DirectorEntity = _directors[1],
                ImageEntity = _images[1],
            }
        ];

        _videoContentGenres =
        [
            new VideoContentGenreEntity
            {
                Id = 1,
                ExternalId = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow.AddDays(-59),
                ModificationTime = DateTime.UtcNow.AddDays(-4),
                AddedTime = DateTime.UtcNow.AddDays(-59),
                VideoContentId = 1,
                GenreId = 1,
                VideoContentEntity = _videoContent[0],
                GenreEntity = _genres[0]
            },

            new VideoContentGenreEntity
            {
                Id = 2,
                ExternalId = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow.AddDays(-59),
                ModificationTime = DateTime.UtcNow.AddDays(-4),
                AddedTime = DateTime.UtcNow.AddDays(-59),
                VideoContentId = 1,
                GenreId = 1,
                VideoContentEntity = _videoContent[0],
                GenreEntity = _genres[0]
            },

            new VideoContentGenreEntity
            {
                Id = 3,
                ExternalId = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow.AddDays(-59),
                ModificationTime = DateTime.UtcNow.AddDays(-4),
                AddedTime = DateTime.UtcNow.AddDays(-59),
                VideoContentId = 2,
                GenreId = 2,
                VideoContentEntity = _videoContent[1],
                GenreEntity = _genres[1]
            }
        ];
    }

    [Test]
    public async Task GetVideoContent_Success_ReturnsListVideoContent()
    {
        _videoContentRepositoryMock.Setup(x
            => x.GetAllWithRelationsAsync()).ReturnsAsync(_videoContent);

        var result = await _videoContentProvider.GetAllAsync();
        var contentIds = result.Select(x => x.Id).ToList();

        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(contentIds, Is.Not.Null.Or.Empty);
        Assert.That(contentIds, Does.Contain(1));
        Assert.That(contentIds, Does.Contain(2));
        _videoContentRepositoryMock.Verify(repo => repo.GetAllWithRelationsAsync(), Times.Once);
    }

    [Test]
    public async Task GetVideoContent_ContentIsEmpty_ReturnNullOrEmpty()
    {
        _videoContentRepositoryMock.Setup(x => x.GetAllWithRelationsAsync())
            .ReturnsAsync(new List<VideoContentEntity>());

        var result = await _videoContentProvider.GetAllAsync();

        Assert.That(result, Is.Not.Null.Or.Empty);
        _videoContentRepositoryMock.Verify(repo => repo.GetAllWithRelationsAsync(), Times.Once);
    }

    [Test]
    public async Task GetVideoContentById_Success_ReturnVideoContent()
    {
        var target = _videoContent[1];
        _videoContentRepositoryMock.Setup(x => x.GetByIdWithRelationsAsync(target.ExternalId))
            .ReturnsAsync(_videoContent[1]);

        var result = await _videoContentProvider.GetByIdAsync(target.ExternalId);

        Assert.That(result, Is.Not.EqualTo(null));
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo(target.Name));
            Assert.That(result.YearOfRelease, Is.EqualTo(target.YearOfRelease));
            Assert.That(result.Description, Is.EqualTo(target.Description));
            Assert.That(result.CreationTime, Is.EqualTo(target.CreationTime));
            Assert.That(result.ModificationTime, Is.EqualTo(target.ModificationTime));
        });
        _videoContentRepositoryMock.Verify(repo =>
            repo.GetByIdWithRelationsAsync(target.ExternalId), Times.Once);
    }

    [Test]
    public void GetVideoContentById_ContentNotFound_ThrowsException()
    {
        var targetGuid = Guid.Parse("bbbbbbbb-1234-1234-1234-999999900002");
        _videoContentRepositoryMock
            .Setup(x => x.GetByIdWithRelationsAsync(targetGuid))
            .ReturnsAsync((VideoContentEntity?)null);
        
        var exception = Assert.ThrowsAsync<BusinessLogicException>(
            async () => await _videoContentProvider.GetByIdAsync(targetGuid));
        
        Assert.That(exception.BlResultCode, Is.EqualTo(BLResultCode.VideoContentNotFound));
        
        _videoContentRepositoryMock.Verify(
            repo => repo.GetByIdWithRelationsAsync(targetGuid), 
            Times.Once);
    }
}