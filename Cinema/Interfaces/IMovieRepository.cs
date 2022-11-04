using Cinema.Domain;
using Cinema.Models;
using FluentResults;

namespace Cinema.Interfaces;

public interface IMovieRepository
{
    Result<Movie> Get(Guid id);
    
    Result<MovieFullModel> GetFull(Guid id);
    
    Result<Guid> Create(MovieCreateModel movieCreateModel);
    
    Result<Guid> Update(MovieUpdateModel updateModel);
    
    Result Delete(Guid movieId);
}