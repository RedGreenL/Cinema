using Cinema.Interfaces;
using Cinema.Models;
using Cinema.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieRepository _repository;

    public MovieController(IMovieRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult Get(Guid id)
    {
        var result = _repository.Get(id);
        return result.IsSuccess ? Ok(result.Value) : BadRequest();
    }
    
    [HttpGet("Full/{id:guid}")]
    public IActionResult GetFull(Guid id)
    {
        var result = _repository.GetFull(id);
        return result.IsSuccess ? Ok(result.Value) : BadRequest();
    }

    [HttpPost]
    public IActionResult Create([FromBody] MovieCreateModel model)
    {
        var result = _repository.Create(model);
        return result.IsSuccess ? Ok(result.Value) : BadRequest();
    }

    [HttpPut]
    public IActionResult UpdateGenre([FromBody] MovieUpdateModel model)
    {
        var result = _repository.Update(model);
        return result.IsSuccess ? Ok(result.Value) : BadRequest();
    }

    [HttpDelete]
    public IActionResult DeleteGenre(Guid id)
    {
        var result = _repository.Delete(id);
        return result.IsSuccess ? Ok() : BadRequest();
    }
}