using AutoMapper;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoContentReviews.BL.VideoContent.Entities;
using VideoContentReviews.BL.VideoContent.Managers;
using VideoContentReviews.BL.VideoContent.Providers;
using VideoContentReviews.Service.Controllers.VideoContent.Requests;
using VideoContentReviews.Service.Controllers.VideoContent.Responses;

namespace VideoContentReviews.Service.Controllers.VideoContent;

[ApiController]
[Route("[controller]")]
public class VideoContentController(IVideoContentManager service, IVideoContentProvider provider, IMapper mapper)
    : ControllerBase
{
    [HttpPost]
    [Route("create")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> CreateVideoContent([FromBody] CreateVideoContentRequest request)
    {
        var createdModel = mapper.Map<CreateVideoContentModel>(request);
        var videoModel = await service.CreateVideoContentAsync(createdModel);
        return Ok(mapper.Map<VideoContentResponse>(videoModel));
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllVideoContent()
    {
        var videoModels = await provider.GetAllAsync();
        if (videoModels.IsNullOrEmpty()) return NotFound();
        return Ok(mapper.Map<VideoContentListResponse>(videoModels));
    }
}