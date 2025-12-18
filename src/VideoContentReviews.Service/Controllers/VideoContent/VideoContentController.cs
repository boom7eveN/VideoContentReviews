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

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetVideoContentById([FromRoute] Guid id)
    {
        var videoModel = await provider.GetByIdAsync(id);
        return Ok(mapper.Map<VideoContentResponse>(videoModel));
    }

    [HttpDelete]
    [Route("{id:guid}/delete")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> DeleteVideoContentById([FromRoute] Guid id)
    {
        await service.DeleteVideoContentAsync(id);
        return Ok();
    }

    [HttpPatch]
    [Route("{id:guid}/update")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> UpdateVideoContentById([FromRoute] Guid id,
        [FromBody] UpdateVideoContentRequest request)
    {
        var updateModel = mapper.Map<UpdateVideoContentModel>(request);
        var model = await service.UpdateVideoContentAsync(id, updateModel);
        return Ok(mapper.Map<VideoContentResponse>(model));
    }
}