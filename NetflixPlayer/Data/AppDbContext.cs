using Microsoft.EntityFrameworkCore;
using NetflixPlayer.Models;

namespace NetflixPlayer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<WatchHistory> WatchHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<WatchHistory>()
                .HasOne(w => w.Movie)
                .WithMany()
                .HasForeignKey(w => w.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            // Create indexes for better performance
            modelBuilder.Entity<Movie>()
                .HasIndex(m => m.Category);

            modelBuilder.Entity<WatchHistory>()
                .HasIndex(w => w.MovieId);
        }
    }
}
