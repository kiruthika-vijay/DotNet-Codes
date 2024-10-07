using System.ComponentModel.DataAnnotations;

namespace MovieAppFSD.Server.Model
{
    public class Movie
    {
        [Key]
        public int MovieID { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Title { get; set; }

        [MaxLength(255)]
        public string? Director { get; set; }

        [Range(1800, 2100)] // Assuming movies are between 1800 and 2100
        public int ReleaseYear { get; set; }

        [MaxLength(100)]
        public string? Genre { get; set; }

        [Range(0.0, 10.0)]
        public decimal Rating { get; set; }
    }

}
