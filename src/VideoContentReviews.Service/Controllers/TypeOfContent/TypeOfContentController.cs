using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoContentReviews.BL.TypesOfContent.Entities;
using VideoContentReviews.BL.TypesOfContent.Managers;
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
    public async Task<IActionResult> CreateDirector([FromBody] CreateTypeOfContentRequest request)
    {
        var createdModel = mapper.Map<CreateTypeOfContentModel>(request);
        var contentModel = await service.CreateTypeOfContentAsync(createdModel);
        return Ok(mapper.Map<TypeOfContentResponse>(contentModel));
    }
}