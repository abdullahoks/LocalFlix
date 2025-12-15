using System.ComponentModel.DataAnnotations;

namespace NetflixPlayer.Models
{
    public class WatchHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MovieId { get; set; }

        public int LastWatchedPosition { get; set; } // Position in seconds

        public DateTime LastWatchedDate { get; set; } = DateTime.Now;

        public int PercentageWatched { get; set; } // 0-100

        // Navigation property
        public Movie? Movie { get; set; }
    }
}
