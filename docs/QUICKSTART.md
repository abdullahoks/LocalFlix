# How to Run Netflix Player

## Quick Start

1. **Install .NET SDK** (if not already installed)
   - Download from: https://dotnet.microsoft.com/download
   - Install .NET 8.0 SDK

2. **Configure Movie Folder**
   - Open `NetflixPlayer/appsettings.json`
   - Change `FolderPath` to your movies folder:
     ```json
     "MovieSettings": {
       "FolderPath": "C:\\Movies"
     }
     ```

3. **Run the Application**
   ```bash
   cd NetflixPlayer
   dotnet restore
   dotnet run
   ```

4. **Open Browser**
   - Navigate to: http://localhost:5000
   - Click "Scan Movies" button
   - Start watching!

## First Time Setup

1. Create a folder for your movies (e.g., `C:\Movies`)
2. Organize movies in subfolders by category:
   ```
   C:\Movies\
   ├── Action\
   ├── Comedy\
   └── Drama\
   ```
3. Add subtitle files (.srt) with same name as video
4. Run the app and click "Scan Movies"

## Keyboard Shortcuts

- **Space** - Play/Pause
- **F** - Fullscreen
- **Arrow Keys** - Seek/Volume
- **C** - Toggle Subtitles

Enjoy! 🎬
