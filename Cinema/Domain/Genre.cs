namespace Cinema.Domain;

public class Genre : BaseEntity
{
    public string Name { get; set; } = null!;

    public IList<MovieGenre> Genres { get; set; } = null!;
}