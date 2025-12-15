using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetflixPlayer.Data;
using NetflixPlayer.Models;

namespace NetflixPlayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchHistoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WatchHistoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/watchhistory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WatchHistory>>> GetWatchHistories()
        {
            var histories = await _context.WatchHistories
                .Include(w => w.Movie)
                .OrderByDescending(w => w.LastWatchedDate)
                .ToListAsync();

            return Ok(histories);
        }

        // GET: api/watchhistory/5
        [HttpGet("{movieId}")]
        public async Task<ActionResult<WatchHistory>> GetWatchHistory(int movieId)
        {
            var history = await _context.WatchHistories
                .FirstOrDefaultAsync(w => w.MovieId == movieId);

            if (history == null)
            {
                return NotFound();
            }

            return Ok(history);
        }

        // POST: api/watchhistory
        [HttpPost]
        public async Task<ActionResult<WatchHistory>> UpdateWatchHistory([FromBody] WatchHistoryUpdateDto dto)
        {
            var movie = await _context.Movies.FindAsync(dto.MovieId);
            if (movie == null)
            {
                return NotFound("Movie not found");
            }

            var history = await _context.WatchHistories
                .FirstOrDefaultAsync(w => w.MovieId == dto.MovieId);

            if (history == null)
            {
                history = new WatchHistory
                {
                    MovieId = dto.MovieId,
                    LastWatchedPosition = dto.Position,
                    LastWatchedDate = DateTime.Now,
                    PercentageWatched = CalculatePercentage(dto.Position, movie.Duration)
                };
                _context.WatchHistories.Add(history);
            }
            else
            {
                history.LastWatchedPosition = dto.Position;
                history.LastWatchedDate = DateTime.Now;
                history.PercentageWatched = CalculatePercentage(dto.Position, movie.Duration);
            }

            await _context.SaveChangesAsync();

            return Ok(history);
        }

        private int CalculatePercentage(int position, int duration)
        {
            if (duration == 0) return 0;
            return Math.Min(100, (int)((position / (double)duration) * 100));
        }
    }

    public class WatchHistoryUpdateDto
    {
        public int MovieId { get; set; }
        public int Position { get; set; }
    }
}
