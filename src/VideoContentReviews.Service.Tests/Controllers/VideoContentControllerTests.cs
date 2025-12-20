using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using VideoContentReviews.BL.Features.VideoContent.Managers;
using VideoContentReviews.Service.Controllers.VideoContent.Requests;

namespace VideoContentReviews.Service.Tests.Controllers;

public class VideoContentControllerTests : TestBase
{
    
    [Test]
    public async Task GetAllVideoContent_NotFound_WhenEmpty()
    {
        using var scope = GetService<IServiceScopeFactory>()!.CreateScope();
        var videoContentManager = scope.ServiceProvider.GetRequiredService<IVideoContentManager>();
        var videoContentProvider = scope.ServiceProvider
            .GetRequiredService<VideoContentReviews.BL.Features.VideoContent.Providers.IVideoContentProvider>();

        var allContent = await videoContentProvider.GetAllAsync();
        foreach (var content in allContent)
        {
            await videoContentManager.DeleteVideoContentAsync(content.ExternalId);
        }

        var response = await TestHttpClient.GetAsync(AppEndpoints.Endpoints.VideoContent);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }


    [Test]
    public async Task GetVideoContentById_NotFound()
    {
        var nonExistentId = Guid.NewGuid();

        var response = await TestHttpClient.GetAsync($"{AppEndpoints.Endpoints.VideoContent}/{nonExistentId}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    

    [Test]
    public async Task CreateVideoContent_Unauthorized_WithoutToken()
    {
        var request = new CreateVideoContentRequest
        {
            Name = "Test Movie",
            YearOfRelease = 2025,
            Description = "Test Description",
            DirectorId = Guid.NewGuid(),
            TypeOfContentId = Guid.NewGuid(),
            ImageId = Guid.NewGuid(),
            GenreIds = [Guid.NewGuid()]
        };

        var response = await TestHttpClient.PostAsJsonAsync(AppEndpoints.Endpoints.VideoContentCreate, request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }
    
    
}