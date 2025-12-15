# Netflix Player - Local Video Player Application

A beautiful, Netflix-style video player application that runs completely offline on your PC. Built with ASP.NET Core backend and modern web frontend.

## 🎬 Features

### Core Features
- **Local Movie Library** - Scan and organize video files from your PC
- **Netflix-Style UI** - Modern dark theme with smooth animations
- **Advanced Video Player** - Professional playback with Video.js
- **Subtitle Support** - Load and display .srt subtitle files
- **Watch History** - Resume movies from where you left off
- **Category Organization** - Automatic categorization by folder structure
- **Search Functionality** - Quick search across your movie library

### Video Player Features
- Play / Pause
- Forward / Rewind (10 seconds)
- Volume control
- Fullscreen mode
- Playback speed control (0.5x - 2x)
- Subtitle toggle (ON/OFF)
- Keyboard shortcuts
- Range request support for seeking

## 🛠️ Tech Stack

- **Backend**: ASP.NET Core 8.0 Web API
- **Database**: SQLite (local, no server needed)
- **Frontend**: HTML5, CSS3, JavaScript
- **Video Player**: Video.js
- **Styling**: Custom CSS with Netflix-inspired design

## 📋 Prerequisites

- .NET 8.0 SDK or later
- Modern web browser (Chrome, Firefox, Edge)
- Video files in supported formats (.mp4, .mkv, .webm, .avi, .mov)

## 🚀 Getting Started

### 1. Install .NET SDK

If you don't have .NET SDK installed:
1. Download from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
2. Install the .NET 8.0 SDK
3. Verify installation: `dotnet --version`

### 2. Configure Movie Folder

Edit `appsettings.json` and set your movie folder path:

```json
{
  "MovieSettings": {
    "FolderPath": "C:\\Movies"
  }
}
```

**Note**: Use double backslashes (`\\`) in Windows paths.

### 3. Restore Dependencies

```bash
cd NetflixPlayer
dotnet restore
```

### 4. Run the Application

```bash
dotnet run
```

The application will start at: `http://localhost:5000`

### 5. Scan Your Movies

1. Open your browser and navigate to `http://localhost:5000`
2. Click the **"Scan Movies"** button in the header
3. Wait for the scan to complete
4. Your movies will appear organized by category

## 📁 Folder Structure

```
NetflixPlayer/
├── Controllers/          # API endpoints
│   ├── MoviesController.cs
│   └── WatchHistoryController.cs
├── Data/                 # Database context
│   └── AppDbContext.cs
├── Models/               # Data models
│   ├── Movie.cs
│   └── WatchHistory.cs
├── Services/             # Business logic
│   └── MovieScannerService.cs
├── wwwroot/              # Frontend files
│   ├── css/
│   │   └── styles.css
│   ├── js/
│   │   ├── app.js
│   │   └── player.js
│   ├── index.html
│   └── player.html
├── Program.cs            # Application entry point
├── appsettings.json      # Configuration
└── NetflixPlayer.csproj  # Project file
```

## 🎮 Keyboard Shortcuts

While watching a movie:

- **Space / K** - Play / Pause
- **F** - Toggle fullscreen
- **M** - Mute / Unmute
- **C** - Toggle subtitles
- **Arrow Left** - Rewind 10 seconds
- **Arrow Right** - Forward 10 seconds
- **Arrow Up** - Increase volume
- **Arrow Down** - Decrease volume

## 📝 Organizing Your Movies

### Folder Structure
The scanner automatically categorizes movies based on folder structure:

```
C:\Movies\
├── Action\
│   ├── movie1.mp4
│   └── movie1.srt
├── Comedy\
│   └── movie2.mp4
└── Drama\
    └── movie3.mp4
```

### Subtitle Files
Place `.srt` files with the same name as your video:
- `movie.mp4` → `movie.srt`

### Cover Images
Place image files with the same name as your video:
- `movie.mp4` → `movie.jpg` (or .png, .webp)

## 🔧 Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=netflix.db"
  },
  "MovieSettings": {
    "FolderPath": "C:\\Movies",
    "AllowedExtensions": [".mp4", ".mkv", ".webm", ".avi", ".mov"]
  }
}
```

## 🌐 API Endpoints

- `GET /api/movies` - Get all movies
- `GET /api/movies/{id}` - Get movie by ID
- `GET /api/movies/categories` - Get all categories
- `POST /api/movies/scan` - Scan for new movies
- `GET /api/movies/{id}/stream` - Stream video
- `GET /api/movies/{id}/subtitle` - Get subtitle file
- `GET /api/movies/{id}/cover` - Get cover image
- `GET /api/watchhistory` - Get watch history
- `POST /api/watchhistory` - Update watch position

## 🎨 Customization

### Change Theme Colors

Edit `wwwroot/css/styles.css` and modify CSS variables:

```css
:root {
    --netflix-red: #E50914;
    --netflix-black: #141414;
    /* ... other colors */
}
```

## 🐛 Troubleshooting

### Movies Not Showing
1. Check that the folder path in `appsettings.json` is correct
2. Ensure video files have supported extensions
3. Click "Scan Movies" to refresh the library

### Video Won't Play
- **MKV files**: May have limited browser support. Convert to MP4 for best compatibility
- **Codec issues**: Ensure videos use H.264 codec for MP4 files

### Subtitles Not Working
- Ensure subtitle file has the same name as the video
- Only `.srt` files are supported
- Check subtitle file encoding (UTF-8 recommended)

## 📦 Building for Production

```bash
dotnet publish -c Release -o publish
```

The compiled application will be in the `publish` folder.

## 🔒 Security Note

This application is designed for **local use only**. It does not include authentication or authorization. Do not expose it to the internet without proper security measures.

## 📄 License

This project is open source and available for personal use.

## 🙏 Credits

- **Video.js** - HTML5 video player
- **Inter Font** - Google Fonts
- Inspired by Netflix UI/UX design

---

**Enjoy your personal Netflix-style movie library! 🍿**
