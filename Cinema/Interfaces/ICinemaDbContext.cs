using Cinema.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Interfaces;

public interface ICinemaDbContext
{
    public DbSet<Movie> Movies { get; set; }
    
    public DbSet<MovieInfo> MovieInfos { get; set; }
    
    public DbSet<MovieType> Types { get; set; }
    
    public DbSet<MovieGenre> MovieGenres { get; set; }
    
    public DbSet<Genre> Genres { get; set; }

    public DbContext Instance { get; }
}