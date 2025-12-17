using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoContentReviews.BL.Director.Entities;
using VideoContentReviews.BL.Director.Managers;
using VideoContentReviews.Service.Controllers.Directors.DTOs.Requests;
using VideoContentReviews.Service.Controllers.Directors.DTOs.Responses;

namespace VideoContentReviews.Service.Controllers.Directors;

[ApiController]
[Route("[controller]")]
public class DirectorsController(IDirectorManager directorManager, IMapper mapper) : ControllerBase
{
    [HttpPost]
    [Route("create")]
    [Authorize(Roles = "Moderator")]
    public async Task<ActionResult<DirectorManager>> CreateDirector([FromQuery] CreateDirectorRequest request)
    {
        var createdModel = mapper.Map<CreateDirectorModel>(request);
        var directorModel = await directorManager.CreateDirectorAsync(createdModel);
        return Ok(mapper.Map<DirectorResponse>(directorModel));
    }
}