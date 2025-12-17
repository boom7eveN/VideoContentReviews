using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoContentReviews.BL.Image.Entities;
using VideoContentReviews.BL.Image.Manager;
using VideoContentReviews.Service.Controllers.Images.DTOs.Requests;
using VideoContentReviews.Service.Controllers.Images.DTOs.Responses;

namespace VideoContentReviews.Service.Controllers.Images;

[ApiController]
[Route("[controller]")]
public class ImageController(IImageManager service, IMapper mapper) : ControllerBase
{
    [HttpPost]
    [Route("create")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> CreateImage([FromQuery] CreateImageRequest request)
    {
        var createdModel = mapper.Map<CreateImageModel>(request);
        var imageModel = await service.CreateImageAsync(createdModel);
        return Ok(mapper.Map<ImageResponse>(imageModel));
    }
}