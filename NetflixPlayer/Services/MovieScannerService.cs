using NetflixPlayer.Data;
using NetflixPlayer.Models;
using Microsoft.EntityFrameworkCore;

namespace NetflixPlayer.Services
{
    public class MovieScannerService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MovieScannerService> _logger;

        private static readonly string[] VideoExtensions = { ".mp4", ".mkv", ".webm", ".avi", ".mov" };
        private static readonly string[] SubtitleExtensions = { ".srt", ".vtt" };

        public MovieScannerService(AppDbContext context, IConfiguration configuration, ILogger<MovieScannerService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<int> ScanMoviesAsync()
        {
            var movieFolder = _configuration["MovieSettings:FolderPath"] ?? "C:\\Movies";
            
            if (!Directory.Exists(movieFolder))
            {
                _logger.LogWarning($"Movie folder does not exist: {movieFolder}");
                return 0;
            }

            var videoFiles = Directory.GetFiles(movieFolder, "*.*", SearchOption.AllDirectories)
                .Where(file => VideoExtensions.Contains(Path.GetExtension(file).ToLower()))
                .ToList();

            int addedCount = 0;

            foreach (var videoFile in videoFiles)
            {
                try
                {
                    // Check if movie already exists in database
                    var existingMovie = await _context.Movies
                        .FirstOrDefaultAsync(m => m.FilePath == videoFile);

                    if (existingMovie != null)
                        continue;

                    var fileName = Path.GetFileNameWithoutExtension(videoFile);
                    var directory = Path.GetDirectoryName(videoFile) ?? "";
                    
                    // IMPROVED: Try to find subtitle file with flexible matching
                    string? subtitlePath = null;
                    
                    // First try exact match
                    foreach (var ext in SubtitleExtensions)
                    {
                        var srtPath = Path.Combine(directory, fileName + ext);
                        if (File.Exists(srtPath))
                        {
                            subtitlePath = srtPath;
                            break;
                        }
                    }
                    
                    // If no exact match, look for ANY subtitle file in the same directory
                    if (subtitlePath == null)
                    {
                        var subtitleFiles = Directory.GetFiles(directory, "*.srt")
                            .Concat(Directory.GetFiles(directory, "*.vtt"))
                            .ToList();
                        
                        if (subtitleFiles.Count == 1)
                        {
                            // If there's only one subtitle file, use it
                            subtitlePath = subtitleFiles[0];
                        }
                        else if (subtitleFiles.Count > 1)
                        {
                            // If multiple, try to find one with similar name
                            var baseFileName = fileName.ToLower();
                            subtitlePath = subtitleFiles.FirstOrDefault(f => 
                                Path.GetFileNameWithoutExtension(f).ToLower().Contains(baseFileName.Substring(0, Math.Min(20, baseFileName.Length))));
                            
                            // If still no match, just use the first one
                            if (subtitlePath == null)
                                subtitlePath = subtitleFiles[0];
                        }
                    }

                    // IMPROVED: Try to find cover image with flexible matching
                    string? coverPath = null;
                    var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                    
                    // First try exact match
                    foreach (var ext in imageExtensions)
                    {
                        var imgPath = Path.Combine(directory, fileName + ext);
                        if (File.Exists(imgPath))
                        {
                            coverPath = imgPath;
                            break;
                        }
                    }
                    
                    // If no exact match, look for ANY image file in the same directory
                    if (coverPath == null)
                    {
                        var imageFiles = new List<string>();
                        foreach (var ext in imageExtensions)
                        {
                            imageFiles.AddRange(Directory.GetFiles(directory, "*" + ext));
                        }
                        
                        if (imageFiles.Count == 1)
                        {
                            // If there's only one image file, use it
                            coverPath = imageFiles[0];
                        }
                        else if (imageFiles.Count > 1)
                        {
                            // If multiple, try to find one with similar name or common cover names
                            var baseFileName = fileName.ToLower();
                            var commonCoverNames = new[] { "cover", "poster", "folder", "thumb" };
                            
                            // First try similar filename
                            coverPath = imageFiles.FirstOrDefault(f => 
                                Path.GetFileNameWithoutExtension(f).ToLower().Contains(baseFileName.Substring(0, Math.Min(20, baseFileName.Length))));
                            
                            // Then try common cover names
                            if (coverPath == null)
                            {
                                coverPath = imageFiles.FirstOrDefault(f => 
                                    commonCoverNames.Any(name => Path.GetFileNameWithoutExtension(f).ToLower().Contains(name)));
                            }
                            
                            // If still no match, use the first image
                            if (coverPath == null)
                                coverPath = imageFiles[0];
                        }
                    }

                    // Determine category from folder structure
                    var category = DetermineCategoryFromPath(videoFile, movieFolder);

                    var movie = new Movie
                    {
                        Title = FormatTitle(fileName),
                        Description = $"A great movie to watch - {fileName}",
                        FilePath = videoFile,
                        CoverImagePath = coverPath,
                        SubtitlePath = subtitlePath,
                        Duration = 0, // Will be set to 0, can be updated later with actual duration
                        Category = category,
                        DateAdded = DateTime.Now
                    };

                    _context.Movies.Add(movie);
                    addedCount++;
                    
                    _logger.LogInformation($"Added movie: {movie.Title} | Cover: {(coverPath != null ? "Yes" : "No")} | Subtitle: {(subtitlePath != null ? "Yes" : "No")}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error processing video file: {videoFile}");
                }
            }

            if (addedCount > 0)
            {
                await _context.SaveChangesAsync();
            }

            _logger.LogInformation($"Scan complete. Added {addedCount} new movies.");
            return addedCount;
        }

        private string DetermineCategoryFromPath(string filePath, string rootFolder)
        {
            var relativePath = Path.GetRelativePath(rootFolder, filePath);
            var parts = relativePath.Split(Path.DirectorySeparatorChar);
            
            if (parts.Length > 1)
            {
                return parts[0]; // Use the first subdirectory as category
            }
            
            return "Movies";
        }

        private string FormatTitle(string fileName)
        {
            // Remove common patterns like year, quality indicators
            var title = fileName;
            
            // Remove year patterns like (2023) or [2023]
            title = System.Text.RegularExpressions.Regex.Replace(title, @"\s*[\(\[]?\d{4}[\)\]]?\s*", " ");
            
            // Remove quality indicators
            var qualityPatterns = new[] { "1080p", "720p", "4K", "BluRay", "WEB-DL", "HDRip", "x264", "x265" };
            foreach (var pattern in qualityPatterns)
            {
                title = title.Replace(pattern, "", StringComparison.OrdinalIgnoreCase);
            }
            
            // Replace dots, underscores with spaces
            title = title.Replace(".", " ").Replace("_", " ");
            
            // Remove extra spaces
            title = System.Text.RegularExpressions.Regex.Replace(title, @"\s+", " ").Trim();
            
            return title;
        }

        public async Task<int> CleanupDeletedMovies()
        {
            _logger.LogInformation("Starting cleanup - removing ALL movies from database...");
            
            var allMovies = await _context.Movies.ToListAsync();
            int deletedCount = allMovies.Count;

            if (deletedCount > 0)
            {
                _logger.LogInformation($"Removing all {deletedCount} movies from database...");
                _context.Movies.RemoveRange(allMovies);
                await _context.SaveChangesAsync();
            }

            _logger.LogInformation($"Cleanup complete. Removed all {deletedCount} movies from database.");
            return deletedCount;
        }
    }
}
