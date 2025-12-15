using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetflixPlayer.Data;
using NetflixPlayer.Models;
using NetflixPlayer.Services;

namespace NetflixPlayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly MovieScannerService _scannerService;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(AppDbContext context, MovieScannerService scannerService, ILogger<MoviesController> logger)
        {
            _context = context;
            _scannerService = scannerService;
            _logger = logger;
        }

        // GET: api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies([FromQuery] string? category = null)
        {
            IQueryable<Movie> query = _context.Movies;

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(m => m.Category == category);
            }

            var movies = await query.OrderByDescending(m => m.DateAdded).ToListAsync();
            return Ok(movies);
        }

        // GET: api/movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // GET: api/movies/categories
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<string>>> GetCategories()
        {
            var categories = await _context.Movies
                .Select(m => m.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            return Ok(categories);
        }

        // POST: api/movies/scan
        [HttpPost("scan")]
        public async Task<ActionResult<object>> ScanMovies()
        {
            try
            {
                var count = await _scannerService.ScanMoviesAsync();
                return Ok(new { message = $"Scan complete. Added {count} new movies.", count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error scanning movies");
                return StatusCode(500, new { message = "Error scanning movies", error = ex.Message });
            }
        }

        // POST: api/movies/cleanup
        [HttpPost("cleanup")]
        public async Task<ActionResult<object>> CleanupMovies()
        {
            try
            {
                var count = await _scannerService.CleanupDeletedMovies();
                return Ok(new { message = $"Cleanup complete. Removed {count} deleted movies.", count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cleaning up movies");
                return StatusCode(500, new { message = "Error cleaning up movies", error = ex.Message });
            }
        }

        // GET: api/movies/5/stream
        [HttpGet("{id}/stream")]
        public async Task<IActionResult> StreamVideo(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null || !System.IO.File.Exists(movie.FilePath))
            {
                return NotFound();
            }

            var fileStream = new FileStream(movie.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var mimeType = GetMimeType(movie.FilePath);

            return File(fileStream, mimeType, enableRangeProcessing: true);
        }

        // GET: api/movies/5/subtitle
        [HttpGet("{id}/subtitle")]
        public async Task<IActionResult> GetSubtitle(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null || string.IsNullOrEmpty(movie.SubtitlePath) || !System.IO.File.Exists(movie.SubtitlePath))
            {
                return NotFound();
            }

            try
            {
                // Read raw bytes
                var bytes = await System.IO.File.ReadAllBytesAsync(movie.SubtitlePath);
                
                // Detect encoding (simple heuristic)
                // If it looks like UTF-8, use it. Otherwise, assume it might be Turkish legacy encoding (Windows-1254)
                string content;
                if (IsUtf8(bytes))
                {
                    content = System.Text.Encoding.UTF8.GetString(bytes);
                }
                else
                {
                    // Fallback to Windows-1254 (Turkish) for legacy subtitles
                    // Note: .NET Core might need System.Text.Encoding.CodePages package for this,
                    // but we can try Default or Latin1 as fallback if 1254 isn't available by default context,
                    // however, best effort is to register provider.
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    var turkishEncoding = System.Text.Encoding.GetEncoding("windows-1254");
                    content = turkishEncoding.GetString(bytes);
                }

                var extension = Path.GetExtension(movie.SubtitlePath).ToLower();
                var mimeType = extension == ".vtt" ? "text/vtt" : "application/x-subrip";

                return Content(content, mimeType, System.Text.Encoding.UTF8); // Always serve as UTF-8
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading subtitle file");
                return StatusCode(500);
            }
        }
        
        private bool IsUtf8(byte[] buffer)
        {
            // Simple UTF-8 validation
            int i = 0;
            while (i < buffer.Length)
            {
                if (buffer[i] < 0x80)
                {
                    i++;
                }
                else if ((buffer[i] & 0xE0) == 0xC0)
                {
                    if (i + 1 >= buffer.Length || (buffer[i + 1] & 0xC0) != 0x80) return false;
                    i += 2;
                }
                else if ((buffer[i] & 0xF0) == 0xE0)
                {
                    if (i + 2 >= buffer.Length || (buffer[i + 1] & 0xC0) != 0x80 || (buffer[i + 2] & 0xC0) != 0x80) return false;
                    i += 3;
                }
                else if ((buffer[i] & 0xF8) == 0xF0)
                {
                    if (i + 3 >= buffer.Length || (buffer[i + 1] & 0xC0) != 0x80 || (buffer[i + 2] & 0xC0) != 0x80 || (buffer[i + 3] & 0xC0) != 0x80) return false;
                    i += 4;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        // GET: api/movies/5/cover
        [HttpGet("{id}/cover")]
        public async Task<IActionResult> GetCover(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null || string.IsNullOrEmpty(movie.CoverImagePath) || !System.IO.File.Exists(movie.CoverImagePath))
            {
                return NotFound();
            }

            var fileStream = new FileStream(movie.CoverImagePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var mimeType = GetMimeType(movie.CoverImagePath);

            return File(fileStream, mimeType);
        }

        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();
            return extension switch
            {
                ".mp4" => "video/mp4",
                ".mkv" => "video/x-matroska",
                ".webm" => "video/webm",
                ".avi" => "video/x-msvideo",
                ".mov" => "video/quicktime",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };
        }
    }
}
