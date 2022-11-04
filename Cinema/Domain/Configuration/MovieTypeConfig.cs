using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Domain.Configuration;

public class MovieTypeConfig : IEntityTypeConfiguration<MovieType>
{
    private readonly List<MovieType> _movieTypes = new()
    {
        new MovieType { Id = Guid.NewGuid(), Name = "Cartoon" },
        new MovieType { Id = Guid.NewGuid(), Name = "Movie" },
        new MovieType { Id = Guid.NewGuid(), Name = "Series" }
    };

    public void Configure(EntityTypeBuilder<MovieType> builder)
    {
        builder.HasData(_movieTypes);
    }
}