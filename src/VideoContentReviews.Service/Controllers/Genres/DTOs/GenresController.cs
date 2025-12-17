using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoContentReviews.BL.Genres.Entities;
using VideoContentReviews.BL.Genres.Managers;
using VideoContentReviews.Service.Controllers.Genres.DTOs.Responses;

namespace VideoContentReviews.Service.Controllers.Genres.DTOs;

[ApiController]
[Route("[controller]")]
public class GenresController(IGenreManager service, IMapper mapper) : ControllerBase
{
    [HttpPost]
    [Route("create")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> CreateGenre([FromQuery] CreateGenreRequest request)
    {
        var createdModel = mapper.Map<CreateGenreModel>(request);
        var genreModel = await service.CreateGenreAsync(createdModel);
        return Ok(mapper.Map<GenreResponse>(genreModel));
    }
}