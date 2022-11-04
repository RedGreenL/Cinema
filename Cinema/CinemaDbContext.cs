using Cinema.Domain;
using Cinema.Domain.Configuration;
using Cinema.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema
{
    public class CinemaDbContext : DbContext, ICinemaDbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>().HasKey(e => new { e.GenreId, e.MovieId });

            modelBuilder.ApplyConfiguration(new MovieConfig());
            modelBuilder.ApplyConfiguration(new MovieTypeConfig());
            modelBuilder.ApplyConfiguration(new GenreConfig());
        }

        public DbSet<Movie> Movies { get; set; } = null!;
        
        public DbSet<MovieInfo> MovieInfos { get; set; } = null!;
        
        public DbSet<MovieType> Types { get; set; } = null!;

        public DbSet<MovieGenre> MovieGenres { get; set; } = null!;
        
        public DbSet<Genre> Genres { get; set; } = null!;

        public DbContext Instance => this;
    }
}
