namespace Cinema.Models;

public class MovieFullModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Date { get; set; }

    public string TypeName { get; set; } = null!;

    public string Actors { get; set; } = null!;

    public string Director { get; set; } = null!;

    public string Screenwriter { get; set; } = null!;

    public List<string> Genres { get; set; } = null!;
}