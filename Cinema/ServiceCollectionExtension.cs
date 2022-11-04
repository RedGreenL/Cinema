using Cinema.Domain;
using Cinema.Interfaces;
using Cinema.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cinema;

public static class ServiceCollectionExtension
{
    private const string DbConnectionSection = "DefaultConnection";
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.AddRepositories();
        builder.AddDatabase();
    }

    private static void AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICinemaDbContext, CinemaDbContext>();
        builder.Services.AddScoped<IRepository<Genre>, Repository<Genre>>();
        builder.Services.AddScoped<IRepository<MovieType>, Repository<MovieType>>();
        builder.Services.AddScoped<IRepository<Movie>, Repository<Movie>>();
        builder.Services.AddScoped<IRepository<MovieInfo>, Repository<MovieInfo>>();
        builder.Services.AddScoped<IMovieRepository, MovieRepository>();
    }

    private static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<CinemaDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString(DbConnectionSection)));
    }
}