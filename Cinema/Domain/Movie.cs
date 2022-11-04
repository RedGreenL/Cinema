namespace Cinema.Domain
{
    public class Movie : BaseEntity
    {
        public string Name { get; set; } = null!;

        public DateTime Date { get; set; }

        public Guid TypeId { get; set; }
        
        public MovieType Type { get; set; } = null!;

        public IList<MovieGenre> Genres { get; set; } = null!;

        public MovieInfo? AdditionalInfo { get; set; }
    }
}
