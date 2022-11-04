using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Domain.Configuration;

public class GenreConfig : IEntityTypeConfiguration<Genre>
{
    private readonly List<Genre> _genres = new()
    {
        new Genre { Id = Guid.NewGuid(), Name = "Action" },
        new Genre { Id = Guid.NewGuid(), Name = "Horror" },
        new Genre { Id = Guid.NewGuid(), Name = "Thriller" }
    };

    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasData(_genres);
    }
}