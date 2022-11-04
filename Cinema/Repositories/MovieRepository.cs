using Cinema.Domain;
using Cinema.Interfaces;
using Cinema.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly IRepository<Movie> _movieRepository;
    private readonly IRepository<MovieInfo> _movieInfoRepository;
    private readonly ICinemaDbContext _db;

    public MovieRepository(IRepository<Movie> movieRepository, 
        IRepository<MovieInfo> movieInfoRepository, 
        ICinemaDbContext db)
    {
        _movieRepository = movieRepository;
        _movieInfoRepository = movieInfoRepository;
        _db = db;
    }

    public Result<Movie> Get(Guid id)
    {
        if (id == Guid.Empty)
            return Result.Fail("id is empty");

        var movie = _db.Movies.FirstOrDefault(x => x.Id == id);
        
        return movie is null 
            ? Result.Fail("Movie not found") 
            : Result.Ok(movie);
    }

    public Result<MovieFullModel> GetFull(Guid id)
    {
        if (id == Guid.Empty)
            return Result.Fail("id is empty");

        var movie = _db.Movies
            .Include(x => x.AdditionalInfo)
            .Include(x => x.Type)
            .Include(x => x.Genres)
            .ThenInclude(x => x.Genre)
            .FirstOrDefault(x => x.Id == id);

        if (movie is null)
            return Result.Fail("Movie not found");
        
        return Result.Ok(new MovieFullModel
        {
            Id = movie.Id,
            Date = movie.Date,
            Name = movie.Name,
            TypeName = movie.Type.Name,
            Director = movie.AdditionalInfo!.Director,
            Screenwriter = movie.AdditionalInfo!.Screenwriter,
            Actors = movie.AdditionalInfo!.Actors,
            Genres = movie.Genres.Select(x => x.Genre.Name).ToList()
        });
    }

    public Result<Guid> Create(MovieCreateModel movieCreateModel)
    {
        // Validations
        
        var insertMovieResult = InsertMovie(movieCreateModel);
        if (insertMovieResult.IsFailed)
        {
            return insertMovieResult;
        }

        InsertAssociations(movieCreateModel.GenreIds, insertMovieResult.Value);
        _db.Instance.SaveChanges();
        
        return insertMovieResult;
    }
    
    public Result<Guid> Update(MovieUpdateModel updateModel)
    {
        // Validations

        // Update movie
        var movie = _db.Movies.FirstOrDefault(x => x.Id == updateModel.Id);
        if(movie is null) return Result.Fail("Movie not found");
        
        movie.Date = updateModel.Date;
        movie.Name = updateModel.Name;
        movie.TypeId = updateModel.TypeId;
        
        _db.Movies.Update(movie);

        // Update movie info
        var movieInfo = _db.MovieInfos.FirstOrDefault(x => x.MovieId == movie.Id);
        if(movieInfo is null) return Result.Fail("MovieInfo not found.");

        movieInfo.Actors = updateModel.Actors;
        movieInfo.Director = updateModel.Actors;
        movieInfo.Screenwriter = updateModel.Screenwriter;

        _db.MovieInfos.Update(movieInfo);

        // Update associations
        var movieGenres = _db.MovieGenres.Where(x => x.MovieId == movie.Id);
        _db.MovieGenres.RemoveRange(movieGenres);
        
        InsertAssociations(updateModel.GenreIds, movie.Id);
        _db.Instance.SaveChanges();

        return Result.Ok(movie.Id);
    }

    public Result Delete(Guid movieId)
    {
        return _movieRepository.Delete(movieId);
    }
    
    private void InsertAssociations(IEnumerable<Guid> genreIds, Guid movieId)
    {
        var genres = genreIds.Select(genreId => new MovieGenre
        {
            GenreId = genreId,
            MovieId = movieId
        });

        _db.MovieGenres.AddRange(genres);
    }

    private Result<Guid> InsertMovie(MovieCreateModel movieCreateModel)
    {
        var movie = new Movie
        {
            Date = movieCreateModel.Date,
            Name = movieCreateModel.Name,
            TypeId = movieCreateModel.TypeId
        };

        var insertMovieResult = _movieRepository.Create(movie);
        if (insertMovieResult.IsFailed)
        {
            return insertMovieResult;
        }

        var movieInfo = new MovieInfo
        {
            Actors = movieCreateModel.Actors,
            Director = movieCreateModel.Director,
            Screenwriter = movieCreateModel.Screenwriter,
            MovieId = insertMovieResult.Value
        };

        var insertMovieInfoResult = _movieInfoRepository.Create(movieInfo);
        if (insertMovieInfoResult.IsFailed)
        {
            return insertMovieInfoResult;
        }

        return insertMovieResult;
    }
}