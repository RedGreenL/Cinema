namespace Cinema.Models;

public class MovieModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;

    public DateTime Date { get; set; }

    public Guid TypeId { get; set; }

    public Guid AdditionalInfoId { get; set; }
}