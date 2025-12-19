using Moq;
using VideoContentReviews.BL.Common.Exceptions;
using VideoContentReviews.BL.Features.VideoContent.Managers;
using VideoContentReviews.BL.Features.VideoContent.DTOs;
using VideoContentReviews.BL.Features.VideoContent.ValidationServices;
using VideoContentReviews.BL.UnitTests.Mappers;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.DataAccess.Repositories;
using VideoContentReviews.DataAccess.Repositories.VideoContentRepository;

namespace VideoContentReviews.BL.UnitTests.VideoContent;

[TestFixture]
public class VideoContentManagerTests
{
    private VideoContentManager _videoContentManager;

    private Mock<IVideoContentRepository> _videoContentRepositoryMock;
    private Mock<IRepository<VideoContentGenreEntity>> _videoContentGenreRepositoryMock;
    private Mock<IVideoContentValidationService> _videoContentValidationServiceMock;

    private TypeOfContentEntity _testTypeOfContent;
    private DirectorEntity _testDirector;
    private ImageEntity _testImage;
    private List<GenreEntity> _testGenres;
    private VideoContentEntity _savedVideoContent;

    [SetUp]
    public void Setup()
    {
        _videoContentRepositoryMock = new Mock<IVideoContentRepository>();
        _videoContentGenreRepositoryMock = new Mock<IRepository<VideoContentGenreEntity>>();
        _videoContentValidationServiceMock = new Mock<IVideoContentValidationService>();

        _videoContentManager = new VideoContentManager(
            _videoContentRepositoryMock.Object,
            _videoContentValidationServiceMock.Object,
            _videoContentGenreRepositoryMock.Object,
            MapperHelper.Mapper
        );

        _testTypeOfContent = new TypeOfContentEntity
        {
            Id = 1,
            ExternalId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Title = "Movie"
        };

        _testDirector = new DirectorEntity
        {
            Id = 1,
            ExternalId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            FirstName = "Test",
            LastName = "Director"
        };

        _testImage = new ImageEntity
        {
            Id = 1,
            ExternalId = Guid.Parse("66666666-6666-6666-6666-666666666666"),
            FileName = "test-image"
        };

        _testGenres =
        [
            new GenreEntity
            {
                Id = 1,
                ExternalId = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                Title = "Action"
            },

            new GenreEntity
            {
                Id = 2,
                ExternalId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Title = "Comedy"
            }
        ];

        _savedVideoContent = new VideoContentEntity
        {
            Id = 1,
            ExternalId = Guid.NewGuid(),
            Name = "Test Movie",
            YearOfRelease = 2020,
            Description = "Test Description",
            TypeOfContentId = 1,
            DirectorId = 1,
            ImageId = 1,
            TypeOfContentEntity = _testTypeOfContent,
            DirectorEntity = _testDirector,
            ImageEntity = _testImage
        };
    }
    

    [Test]
    public async Task CreateVideoContentAsync_Success_ReturnsVideoContentModel()
    {
        // arrange
        var model = new CreateVideoContentModel
        {
            Name = "Test Movie",
            YearOfRelease = 2020,
            Description = "Test Description",
            TypeOfContentExternalId = _testTypeOfContent.ExternalId,
            DirectorExternalId = _testDirector.ExternalId,
            ImageExternalId = _testImage.ExternalId,
            GenreExternalIds = _testGenres.Select(g => g.ExternalId).ToList()
        };



        _videoContentValidationServiceMock
            .Setup(x => x.ValidateAndGetTypeOfContentAsync(_testTypeOfContent.ExternalId))
            .ReturnsAsync(_testTypeOfContent);

        _videoContentValidationServiceMock
            .Setup(x => x.ValidateAndGetDirectorAsync(_testDirector.ExternalId))
            .ReturnsAsync(_testDirector);

        _videoContentValidationServiceMock
            .Setup(x => x.ValidateAndGetImageAsync(_testImage.ExternalId))
            .ReturnsAsync(_testImage);

        _videoContentValidationServiceMock
            .Setup(x => x.ValidateAndGetGenresAsync(model.GenreExternalIds))
            .ReturnsAsync(_testGenres);

        _videoContentRepositoryMock
            .Setup(x => x.SaveAsync(It.IsAny<VideoContentEntity>()))
            .ReturnsAsync((VideoContentEntity entity) =>
            {
                entity.Id = 1;
                entity.ExternalId = Guid.NewGuid();
                return entity;
            });

        _videoContentRepositoryMock
            .Setup(x => x.GetByIdWithRelationsAsync(It.IsAny<int>()))
            .ReturnsAsync(_savedVideoContent);

        _videoContentGenreRepositoryMock
            .Setup(x => x.SaveAsync(It.IsAny<VideoContentGenreEntity>()))
            .ReturnsAsync((VideoContentGenreEntity entity) => entity);

        // act
        var result = await _videoContentManager.CreateVideoContentAsync(model);

        // assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo(model.Name));
            Assert.That(result.YearOfRelease, Is.EqualTo(model.YearOfRelease));
            Assert.That(result.Description, Is.EqualTo(model.Description));
        });


        _videoContentRepositoryMock.Verify(
            x => x.SaveAsync(It.IsAny<VideoContentEntity>()), Times.Once);
        _videoContentGenreRepositoryMock.Verify(
            x => x.SaveAsync(It.IsAny<VideoContentGenreEntity>()), Times.Exactly(2));
    }

    [Test]
    public void CreateVideoContentAsync_RelatedEntityNotFound_ThrowsException()
    {
        // arrange
        var model = new CreateVideoContentModel
        {
            Name = "Test Movie",
            YearOfRelease = 2020,
            TypeOfContentExternalId = Guid.NewGuid(),
            DirectorExternalId = _testDirector.ExternalId,
            ImageExternalId = _testImage.ExternalId,
            GenreExternalIds = new List<Guid>()
        };

        _videoContentValidationServiceMock
            .Setup(x => x.ValidateAndGetTypeOfContentAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new BusinessLogicException(BLResultCode.TypeOfContentNotFound));

        // act
        var exception = Assert.ThrowsAsync<BusinessLogicException>(
            async () => await _videoContentManager.CreateVideoContentAsync(model));

        //assert
        Assert.That(exception.BlResultCode, Is.EqualTo(BLResultCode.TypeOfContentNotFound));


        _videoContentRepositoryMock.Verify(
            x => x.SaveAsync(It.IsAny<VideoContentEntity>()), Times.Never);
    }



    [Test]
    public async Task UpdateVideoContent_Success_ReturnsUpdatedVideoContentModel()
    {
        // arrange
        var videoContentId = Guid.NewGuid();
        var existingEntity = new VideoContentEntity
        {
            Id = 1,
            ExternalId = videoContentId,
            Name = "Old Name",
            YearOfRelease = 2010,
            Description = "Old Description",
            TypeOfContentId = 1,
            DirectorId = 1,
            ImageId = 1
        };

        var updateModel = new UpdateVideoContentModel
        {
            Name = "New Name",
            YearOfRelease = 2020,
            Description = "New Description"
        };

        var updatedEntity = new VideoContentEntity
        {
            Id = 1,
            ExternalId = videoContentId,
            Name = "New Name",
            YearOfRelease = 2020,
            Description = "New Description",
            TypeOfContentId = 1,
            DirectorId = 1,
            ImageId = 1,
            TypeOfContentEntity = _testTypeOfContent,
            DirectorEntity = _testDirector,
            ImageEntity = _testImage
        };

        _videoContentRepositoryMock
            .Setup(x => x.GetByIdAsync(videoContentId))
            .ReturnsAsync(existingEntity);

        _videoContentRepositoryMock
            .Setup(x => x.SaveAsync(It.IsAny<VideoContentEntity>()))
            .ReturnsAsync((VideoContentEntity entity) => entity);

        _videoContentRepositoryMock
            .Setup(x => x.GetByIdWithRelationsAsync(It.IsAny<int>()))
            .ReturnsAsync(updatedEntity);

        // act
        var result = await _videoContentManager.UpdateVideoContentAsync(videoContentId, updateModel);

        // assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo(updateModel.Name));
            Assert.That(result.YearOfRelease, Is.EqualTo(updateModel.YearOfRelease));
            Assert.That(result.Description, Is.EqualTo(updateModel.Description));
        });

        _videoContentRepositoryMock.Verify(
            x => x.GetByIdAsync(videoContentId), Times.Once);
        _videoContentRepositoryMock.Verify(
            x => x.SaveAsync(It.IsAny<VideoContentEntity>()), Times.Once);
    }

    [Test]
    public void UpdateVideoContent_VideoContentNotFound_ThrowsException()
    {
        // arrange
        var videoContentId = Guid.NewGuid();
        var updateModel = new UpdateVideoContentModel
        {
            Name = "New Name",
            YearOfRelease = 2020
        };

        _videoContentRepositoryMock
            .Setup(x => x.GetByIdAsync(videoContentId))
            .ReturnsAsync((VideoContentEntity?)null);

        // act
        var exception = Assert.ThrowsAsync<BusinessLogicException>(
            async () => await _videoContentManager.UpdateVideoContentAsync(videoContentId, updateModel));
        //assert
        Assert.That(exception.BlResultCode, Is.EqualTo(BLResultCode.VideoContentNotFound));

        _videoContentRepositoryMock.Verify(
            x => x.GetByIdAsync(videoContentId), Times.Once);
        _videoContentRepositoryMock.Verify(
            x => x.SaveAsync(It.IsAny<VideoContentEntity>()), Times.Never);
    }



    [Test]
    public async Task DeleteVideoContent_Success_DeleteVideoContent()
    {
        //arrange
        var videoContentId = Guid.NewGuid();
        var existingEntity = new VideoContentEntity
        {
            Id = 1,
            ExternalId = videoContentId,
            Name = "Test Movie",
            YearOfRelease = 2020
        };

        _videoContentRepositoryMock
            .Setup(x => x.GetByIdAsync(videoContentId))
            .ReturnsAsync(existingEntity);

        _videoContentRepositoryMock
            .Setup(x => x.DeleteAsync(It.IsAny<VideoContentEntity>()))
            .Returns(Task.CompletedTask);

        // act
        await _videoContentManager.DeleteVideoContentAsync(videoContentId);

        // assert
        _videoContentRepositoryMock.Verify(
            x => x.GetByIdAsync(videoContentId), Times.Once);
        _videoContentRepositoryMock.Verify(
            x => x.DeleteAsync(It.Is<VideoContentEntity>(e => e.ExternalId == videoContentId)), 
            Times.Once);
    }

    [Test]
    public void DeleteVideoContent_VideoContentNotFound_ThrowsException()
    {
        // arrange
        var videoContentId = Guid.NewGuid();

        _videoContentRepositoryMock
            .Setup(x => x.GetByIdAsync(videoContentId))
            .ReturnsAsync((VideoContentEntity?)null);

        // act
        var exception = Assert.ThrowsAsync<BusinessLogicException>(
            async () => await _videoContentManager.DeleteVideoContentAsync(videoContentId));

        //assert
        Assert.That(exception.BlResultCode, Is.EqualTo(BLResultCode.VideoContentNotFound));

        _videoContentRepositoryMock.Verify(
            x => x.GetByIdAsync(videoContentId), Times.Once);
        _videoContentRepositoryMock.Verify(
            x => x.DeleteAsync(It.IsAny<VideoContentEntity>()), Times.Never);
    }

}