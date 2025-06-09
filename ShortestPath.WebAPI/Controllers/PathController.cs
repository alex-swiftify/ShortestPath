using Microsoft.AspNetCore.Mvc;
using ShortestPath.BusinessLogic.Services;

namespace ShortestPath.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PathController : ControllerBase
{
    private readonly MovieService _movieService;

    public PathController()
    {
        _movieService = new MovieService();
        _movieService.LoadMovies();
    }

    [HttpGet("actors")]
    public IActionResult GetActors()
    {
        var actors = _movieService.GetActors();
        return Ok(actors);
    }

    [HttpGet("path")]
    public IActionResult GetPath([FromQuery] string fromActor)
    {
        var path = _movieService.FindShortestPath(fromActor, "Tom Cruise");
        return Ok(path?.Select(p => p.DisplayName).ToList());
    }
}