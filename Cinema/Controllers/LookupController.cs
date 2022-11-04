using Cinema.Domain;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LookupController : ControllerBase
{
    private readonly IRepository<Genre> _genreRepository;
    private readonly IRepository<MovieType> _movieTypeRepository;

    public LookupController(IRepository<Genre> genreRepository, IRepository<MovieType> movieTypeRepository)
    {
        _genreRepository = genreRepository;
        _movieTypeRepository = movieTypeRepository;
    }

    #region Genre

    [Authorize]
    [HttpGet("Genre/")]
    public IActionResult GetAllGenres()
    {
        return Ok(_genreRepository.GetAll());
    }

    [HttpPost("Genre/")]
    public IActionResult CreateGenre([FromBody] LookupCreateModel model)
    {
        var result = _genreRepository.Create(new Genre { Name = model.Name });
        return result.IsSuccess ? Ok() : BadRequest();
    }

    [HttpPut("Genre/")]
    public IActionResult UpdateGenre([FromBody] LookupUpdateModel model)
    {
        var result = _genreRepository.Update(new Genre
        {
            Id = model.Id,
            Name = model.Name,
        });

        return result.IsSuccess ? Ok() : BadRequest();
    }

    [HttpDelete("Genre/{id:guid}")]
    public IActionResult DeleteGenre(Guid id)
    {
        var result = _genreRepository.Delete(id);
        return result.IsSuccess ? Ok() : BadRequest();
    }

    #endregion

    #region MovieType

    [HttpGet("MovieType/")]
    public IActionResult GetAllMovieType()
    {
        return Ok(_movieTypeRepository.GetAll());
    }

    [HttpPost("MovieType/")]
    public IActionResult CreateMovieType([FromBody] LookupCreateModel createModel)
    {
        var result = _movieTypeRepository.Create(new MovieType { Name = createModel.Name });
        return result.IsSuccess ? Ok() : BadRequest();
    }

    [HttpPut("MovieType/")]
    public IActionResult UpdateMovieType([FromBody] LookupUpdateModel createModel)
    {
        var result = _movieTypeRepository.Update(new MovieType
        {
            Id = createModel.Id,
            Name = createModel.Name
        });

        return result.IsSuccess ? Ok() : BadRequest();
    }

    [HttpDelete("MovieType/{id:guid}")]
    public IActionResult DeleteMovieType(Guid id)
    {
        var result = _movieTypeRepository.Delete(id);
        return result.IsSuccess ? Ok() : BadRequest();
    }

    #endregion
}