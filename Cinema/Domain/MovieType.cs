namespace Cinema.Domain;

public class MovieType : BaseEntity
{
    public string Name { get; set; } = null!;

    public IList<Movie> Movies { get; set; } = null!;
}