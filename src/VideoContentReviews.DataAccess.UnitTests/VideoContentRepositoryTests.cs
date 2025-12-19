using Microsoft.EntityFrameworkCore;
using Moq;
using VideoContentReviews.DataAccess.Context;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.DataAccess.Entities.Primitives;
using VideoContentReviews.DataAccess.Repositories.VideoContentRepository;

namespace VideoContentReviews.DataAccess.UnitTests;

[TestFixture]
public class VideoContentRepositoryTests
{
    private DbContextOptions<VideoContentReviewsDbContext> _dbOptions;
    private Mock<IDbContextFactory<VideoContentReviewsDbContext>> _factoryMock;
    private VideoContentReviewsDbContext _context;
    private VideoContentRepository _repository;

    [SetUp]
    public async Task Setup()
    {
        _dbOptions = new DbContextOptionsBuilder<VideoContentReviewsDbContext>()
            .UseInMemoryDatabase(databaseName: $"VideoContentReviews_{Guid.NewGuid()}")
            .Options;

        _context = new VideoContentReviewsDbContext(_dbOptions);

        _factoryMock = new Mock<IDbContextFactory<VideoContentReviewsDbContext>>();
        _factoryMock
            .Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new VideoContentReviewsDbContext(_dbOptions));

        var typesOfContent = new[]
        {
            new TypeOfContentEntity
            {
                Id = 1,
                ExternalId = Guid.Parse("10000000-0000-0000-0000-000000000000"),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Title = "Movie"
            },
            new TypeOfContentEntity
            {
                Id = 2,
                ExternalId = Guid.Parse("20000000-0000-0000-0000-000000000000"),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Title = "TV Series"
            }
        };

        var directors = new[]
        {
            new DirectorEntity
            {
                Id = 1,
                ExternalId = Guid.Parse("11000000-0000-0000-0000-000000000000"),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                FirstName = "John",
                LastName = "Director",
                Patronymic = "Test"
            },
            new DirectorEntity
            {
                Id = 2,
                ExternalId = Guid.Parse("22000000-0000-0000-0000-000000000000"),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                FirstName = "Jane",
                LastName = "Smith"
            }
        };

        var images = new[]
        {
            new ImageEntity
            {
                Id = 1,
                ExternalId = Guid.Parse("11100000-0000-0000-0000-000000000000"),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                FileName = "movie-poster",
                FileExtension = ImageFormat.Jpeg
            },
            new ImageEntity
            {
                Id = 2,
                ExternalId = Guid.Parse("22200000-0000-0000-0000-000000000000"),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                FileName = "series-poster",
                FileExtension = ImageFormat.Png
            }
        };

        var genres = new[]
        {
            new GenreEntity
            {
                Id = 1,
                ExternalId = Guid.Parse("11110000-0000-0000-0000-000000000000"),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Title = "Action"
            },
            new GenreEntity
            {
                Id = 2,
                ExternalId = Guid.Parse("22220000-0000-0000-0000-000000000000"),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Title = "Comedy"
            },
            new GenreEntity
            {
                Id = 3,
                ExternalId = Guid.Parse("33330000-0000-0000-0000-000000000000"),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Title = "Drama"
            }
        };

        var videoContents = new[]
        {
            new VideoContentEntity
            {
                Id = 1,
                ExternalId = Guid.Parse("11111000-0000-0000-0000-000000000000"),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Name = "Test Movie",
                YearOfRelease = 2020,
                Description = "Test Description",
                UserAverageRating = 4.5,
                TypeOfContentId = 1,
                DirectorId = 1,
                ImageId = 1,
                VideoContentsGenres = new List<VideoContentGenreEntity>
                {
                    new VideoContentGenreEntity
                    {
                        Id = 1,
                        ExternalId = Guid.NewGuid(),
                        CreationTime = DateTime.UtcNow,
                        ModificationTime = DateTime.UtcNow,
                        VideoContentId = 1,
                        GenreId = 1,
                        AddedTime = DateTime.UtcNow
                    },
                    new VideoContentGenreEntity
                    {
                        Id = 2,
                        ExternalId = Guid.NewGuid(),
                        CreationTime = DateTime.UtcNow,
                        ModificationTime = DateTime.UtcNow,
                        VideoContentId = 1,
                        GenreId = 2,
                        AddedTime = DateTime.UtcNow
                    }
                }
            },
            new VideoContentEntity
            {
                Id = 2,
                ExternalId = Guid.Parse("22222000-0000-0000-0000-000000000000"),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Name = "Test Series",
                YearOfRelease = 2021,
                Description = "Test Series Description",
                UserAverageRating = 4.8,
                TypeOfContentId = 2,
                DirectorId = 2,
                ImageId = 2,
                VideoContentsGenres = new List<VideoContentGenreEntity>
                {
                    new VideoContentGenreEntity
                    {
                        Id = 3,
                        ExternalId = Guid.NewGuid(),
                        CreationTime = DateTime.UtcNow,
                        ModificationTime = DateTime.UtcNow,
                        VideoContentId = 2,
                        GenreId = 3,
                        AddedTime = DateTime.UtcNow
                    }
                }
            }
        };

        _context.TypeOfContents.AddRange(typesOfContent);
        _context.Directors.AddRange(directors);
        _context.Images.AddRange(images);
        _context.Genres.AddRange(genres);
        _context.VideoContents.AddRange(videoContents);

        await _context.SaveChangesAsync();

        _repository = new VideoContentRepository(_factoryMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task GetAllWithRelationsAsync_Success()
    {
        var list = await _repository.GetAllWithRelationsAsync();

        Assert.That(list, Is.Not.Null);
        Assert.That(list.Count, Is.EqualTo(2));
        Assert.That(list[0].TypeOfContentEntity, Is.Not.Null);
        Assert.That(list[0].DirectorEntity, Is.Not.Null);
        Assert.That(list[0].ImageEntity, Is.Not.Null);
        Assert.That(list[0].VideoContentsGenres, Is.Not.Null);
        Assert.That(list[0].VideoContentsGenres.Count, Is.EqualTo(2));
        Assert.That(list[0].VideoContentsGenres.First().GenreEntity, Is.Not.Null);
    }

    [Test]
    public async Task GetByIdWithRelationsAsync_ByExternalId_Success()
    {
        var externalId = Guid.Parse("11111000-0000-0000-0000-000000000000");
        var result = await _repository.GetByIdWithRelationsAsync(externalId);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Test Movie"));
        Assert.That(result.TypeOfContentEntity, Is.Not.Null);
        Assert.That(result.TypeOfContentEntity.Title, Is.EqualTo("Movie"));
        Assert.That(result.DirectorEntity, Is.Not.Null);
        Assert.That(result.DirectorEntity.FirstName, Is.EqualTo("John"));
        Assert.That(result.ImageEntity, Is.Not.Null);
        Assert.That(result.ImageEntity.FileName, Is.EqualTo("movie-poster"));
        Assert.That(result.VideoContentsGenres, Is.Not.Null);
        Assert.That(result.VideoContentsGenres.Count, Is.EqualTo(2));
        Assert.That(result.VideoContentsGenres.First().GenreEntity, Is.Not.Null);
        Assert.That(result.VideoContentsGenres.First().GenreEntity.Title, Is.EqualTo("Action"));
    }

    [Test]
    public async Task GetByIdWithRelationsAsync_ByExternalId_NotFound_ReturnsNull()
    {
        var externalId = Guid.Parse("99999999-9999-9999-9999-999999999999");
        var result = await _repository.GetByIdWithRelationsAsync(externalId);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetByIdWithRelationsAsync_ById_Success()
    {
        var result = await _repository.GetByIdWithRelationsAsync(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Test Movie"));
        Assert.That(result.TypeOfContentEntity, Is.Not.Null);
        Assert.That(result.DirectorEntity, Is.Not.Null);
        Assert.That(result.ImageEntity, Is.Not.Null);
        Assert.That(result.VideoContentsGenres, Is.Not.Null);
        Assert.That(result.VideoContentsGenres.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetByIdWithRelationsAsync_ById_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByIdWithRelationsAsync(999);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetByIdAsync_ByExternalId_Success()
    {
        var externalId = Guid.Parse("11111000-0000-0000-0000-000000000000");
        var result = await _repository.GetByIdAsync(externalId);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Test Movie"));
        Assert.That(result.YearOfRelease, Is.EqualTo(2020));
    }

    [Test]
    public async Task GetByIdAsync_ByExternalId_NotFound_ReturnsNull()
    {
        var externalId = Guid.Parse("99999999-9999-9999-9999-999999999999");
        var result = await _repository.GetByIdAsync(externalId);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetByIdAsync_ById_Success()
    {
        var result = await _repository.GetByIdAsync(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Test Movie"));
    }

    [Test]
    public async Task GetByIdAsync_ById_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByIdAsync(999);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetAllAsync_Success()
    {
        var list = await _repository.GetAllAsync();

        var videoContentEntities = list as VideoContentEntity[] ?? list.ToArray();
        Assert.That(videoContentEntities, Is.Not.Null);
        Assert.That(videoContentEntities.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task SaveAsync_NewEntity_Success()
    {
        var newVideoContent = new VideoContentEntity
        {
            Name = "New Movie",
            YearOfRelease = 2023,
            Description = "New Description",
            UserAverageRating = 0.0,
            TypeOfContentId = 1,
            DirectorId = 1,
            ImageId = 1
        };

        var result = await _repository.SaveAsync(newVideoContent);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.GreaterThan(0));
        Assert.That(result.ExternalId, Is.Not.EqualTo(Guid.Empty));
        Assert.That(result.Name, Is.EqualTo("New Movie"));
        Assert.That(result.CreationTime, Is.Not.EqualTo(default(DateTime)));
        Assert.That(result.ModificationTime, Is.Not.EqualTo(default(DateTime)));

        var saved = await _repository.GetByIdAsync(result.Id);
        Assert.That(saved, Is.Not.Null);
        Assert.That(saved.Name, Is.EqualTo("New Movie"));
    }

    [Test]
    public async Task SaveAsync_UpdateEntity_Success()
    {
        var existing = await _repository.GetByIdAsync(1);
        Assert.That(existing, Is.Not.Null);

        existing.Name = "Updated Movie Name";
        existing.Description = "Updated Description";
        var originalModificationTime = existing.ModificationTime;

        await Task.Delay(10); 

        var result = await _repository.SaveAsync(existing);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Updated Movie Name"));
        Assert.That(result.Description, Is.EqualTo("Updated Description"));
        Assert.That(result.ModificationTime, Is.GreaterThan(originalModificationTime));

        var updated = await _repository.GetByIdAsync(1);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.Name, Is.EqualTo("Updated Movie Name"));
    }

    [Test]
    public async Task DeleteAsync_Success()
    {
        var existing = await _repository.GetByIdAsync(1);
        Assert.That(existing, Is.Not.Null);

        await _repository.DeleteAsync(existing);

        var deleted = await _repository.GetByIdAsync(1);
        Assert.That(deleted, Is.Null);
    }

    [Test]
    public async Task GetAllAsync_WithFilter_Success()
    {
        var list = await _repository.GetAllAsync(vc => vc.YearOfRelease == 2020);

        var videoContentEntities = list as VideoContentEntity[] ?? list.ToArray();
        Assert.That(videoContentEntities, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(videoContentEntities.Count(), Is.EqualTo(1));
            Assert.That(videoContentEntities.First().Name, Is.EqualTo("Test Movie"));
        });
    }
}