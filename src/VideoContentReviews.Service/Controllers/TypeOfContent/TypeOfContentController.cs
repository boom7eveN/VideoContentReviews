using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoContentReviews.BL.Director.Entities;
using VideoContentReviews.BL.Director.Managers;
using VideoContentReviews.BL.TypeOfContent.Entities;
using VideoContentReviews.BL.TypeOfContent.Managers;
using VideoContentReviews.Service.Controllers.Directors.DTOs.Requests;
using VideoContentReviews.Service.Controllers.Directors.DTOs.Responses;
using VideoContentReviews.Service.Controllers.TypeOfContent.DTOs.Requests;
using VideoContentReviews.Service.Controllers.TypeOfContent.DTOs.Responses;

namespace VideoContentReviews.Service.Controllers.TypeOfContent;

[ApiController]
[Route("[controller]")]
public class TypeOfContentController(ITypeOfContentManager service, IMapper mapper) : ControllerBase
{
    ITypeOfContentManager _service = service;
    IMapper _mapper = mapper;

    [HttpPost]
    [Route("create")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> CreateDirector([FromQuery] CreateTypeOfContentRequest request)
    {
        var createdModel = mapper.Map<CreateTypeOfContentModel>(request);
        var contentModel = await service.CreateTypeOfContentAsync(createdModel);
        return Ok(mapper.Map<TypeOfContentResponse>(contentModel));
    }
}