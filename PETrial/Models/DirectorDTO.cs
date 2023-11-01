namespace PETrial.Models
{
    public class DirectorDTORequest
    {
        public string FullName { get; set; } = null!;
        public bool Male { get; set; }
        public string Dob { get; set; } = null!;
        public string Nationality { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
    public class DirectorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string DobString { get; set; } = null!;
        public string Nationality { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

    public class MovieDTO
    {
        public MovieDTO() {
            Genres = new HashSet<Genre>();
            Stars = new HashSet<Star>();
        }
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? ReleaseDate { get; set; }
        public string? ReleaseYear { get; set; }
        public string? Description { get; set; }
        public string Language { get; set; } = null!;
        public int? ProducerId { get; set; }
        public int? DirectorId { get; set; }
        public string? ProducerName { get; set; }
        public string? DirectorName { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Star> Stars { get; set; }
    }

    public class DirectorDTOwithMovies
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string DobString { get; set; } = null!;
        public string Nationality { get; set; } = null!;
        public string Description { get; set; } = null!;
        public virtual ICollection<MovieDTO>? Movies { get; set; } = null!;
    }
}
