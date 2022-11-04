using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Domain.Configuration
{
    public class MovieConfig : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasOne(k => k.AdditionalInfo)
                .WithOne(k => k.Movie)
                .HasForeignKey<MovieInfo>(fk => fk.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
        }
    }
}
