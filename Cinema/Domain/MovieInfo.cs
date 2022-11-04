namespace Cinema.Domain;

public class MovieInfo : BaseEntity
{
    public string Actors { get; set; } = null!;

    public string Director { get; set; } = null!;

    public string Screenwriter { get; set; } = null!;

    public Guid MovieId { get; set; }

    public Movie Movie { get; set; } = null!;
}