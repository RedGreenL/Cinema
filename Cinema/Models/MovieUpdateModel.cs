namespace Cinema.Models;

public class MovieUpdateModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;

    public DateTime Date { get; set; }

    public Guid TypeId { get; set; }

    public List<Guid> GenreIds { get; set; } = null!;

    public string Actors { get; set; } = null!;

    public string Director { get; set; } = null!;

    public string Screenwriter { get; set; } = null!;
}