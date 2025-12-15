using System.ComponentModel.DataAnnotations;

namespace NetflixPlayer.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string FilePath { get; set; } = string.Empty;

        public string? CoverImagePath { get; set; }

        public string? SubtitlePath { get; set; }

        public int Duration { get; set; } // Duration in seconds

        [MaxLength(100)]
        public string Category { get; set; } = "Uncategorized";

        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
