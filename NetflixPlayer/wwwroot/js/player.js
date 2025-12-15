// API Base URL
const API_BASE = '/api';

// Player instance
let player = null;
let currentMovie = null;
let subtitlesEnabled = false;
let watchHistoryInterval = null;

// Get movie ID from URL
const urlParams = new URLSearchParams(window.location.search);
const movieId = urlParams.get('id');

// Initialize player
document.addEventListener('DOMContentLoaded', async () => {
    if (!movieId) {
        alert('No movie selected');
        window.location.href = 'index.html';
        return;
    }

    await loadMovie();
    initializePlayer();
    loadWatchHistory();
});

// Load movie details
async function loadMovie() {
    try {
        const response = await fetch(`${API_BASE}/movies/${movieId}`);
        if (!response.ok) throw new Error('Movie not found');

        currentMovie = await response.json();

        // Update UI with movie info
        document.getElementById('movieTitle').textContent = currentMovie.title;

        // Update page title
        document.title = `${currentMovie.title} - LocalFlix`;
    } catch (error) {
        console.error('Error loading movie:', error);
        alert('Error loading movie');
        window.location.href = 'index.html';
    }
}

// Initialize Video.js player
function initializePlayer() {
    const videoElement = document.getElementById('videoPlayer');

    player = videojs(videoElement, {
        controls: true,
        autoplay: false,
        preload: 'auto',
        fluid: true,
        responsive: true,
        playbackRates: [0.5, 0.75, 1, 1.25, 1.5, 2],
        userActions: {
            hotkeys: true
        },
        controlBar: {
            children: [
                'playToggle',
                'volumePanel',
                'currentTimeDisplay',
                'timeDivider',
                'durationDisplay',
                'progressControl',
                'liveDisplay',
                'seekToLive',
                'remainingTimeDisplay',
                'customControlSpacer',
                'playbackRateMenuButton',
                'chaptersButton',
                'descriptionsButton',
                'subsCapsButton',
                'audioTrackButton',
                'fullscreenToggle'
            ]
        }
    });

    // Set video source
    player.src({
        type: getMimeType(currentMovie.filePath),
        src: `${API_BASE}/movies/${movieId}/stream`
    });

    // Load subtitles if available
    if (currentMovie.subtitlePath) {
        loadSubtitles();
    }

    // Start tracking watch history
    startWatchHistoryTracking();

    // Keyboard shortcuts
    setupKeyboardShortcuts();
}

// Load and parse subtitles
async function loadSubtitles() {
    try {
        const response = await fetch(`${API_BASE}/movies/${movieId}/subtitle`);
        if (!response.ok) return;

        // The backend now handles encoding conversion to UTF-8
        const subtitleText = await response.text();
        const vttContent = convertSRTtoVTT(subtitleText);
        const blob = new Blob([vttContent], { type: 'text/vtt;charset=utf-8' });
        const url = URL.createObjectURL(blob);

        player.addRemoteTextTrack({
            kind: 'captions', // 'captions' is often better supported for styling
            label: 'Turkish',
            srclang: 'tr',
            src: url,
            default: true
        }, false);

        // Show captions by default
        setTimeout(() => {
            const tracks = player.textTracks();
            for (let i = 0; i < tracks.length; i++) {
                if (tracks[i].kind === 'captions') {
                    tracks[i].mode = 'showing';
                }
            }
        }, 500);

    } catch (error) {
        console.error('Error loading subtitles:', error);
    }
}

// Convert SRT format to VTT format
function convertSRTtoVTT(srt) {
    if (srt.trim().startsWith('WEBVTT')) {
        return srt;
    }
    let vtt = 'WEBVTT\n\n';
    vtt += srt.replace(/(\d{2}:\d{2}:\d{2}),(\d{3})/g, '$1.$2');
    return vtt;
}

// Setup keyboard shortcuts
function setupKeyboardShortcuts() {
    // Video.js handles most shortcuts natively now, but we can add custom ones if needed
    document.addEventListener('keydown', (e) => {
        if (e.target.tagName === 'INPUT') return;

        switch (e.key) {
            case 'ArrowLeft':
                player.currentTime(player.currentTime() - 10);
                break;
            case 'ArrowRight':
                player.currentTime(player.currentTime() + 10);
                break;
            case ' ':
                if (player.paused()) player.play();
                else player.pause();
                break;
        }
    });
}

// Load watch history and resume playback
async function loadWatchHistory() {
    try {
        const response = await fetch(`${API_BASE}/watchhistory/${movieId}`);
        if (response.ok) {
            const history = await response.json();

            // Resume from last position if watched more than 5 seconds
            if (history.lastWatchedPosition > 5) {
                player.currentTime(history.lastWatchedPosition);

                // Show notification
                showNotification(`Resuming from ${formatTime(history.lastWatchedPosition)}`);
            }
        }
    } catch (error) {
        console.error('Error loading watch history:', error);
    }
}

// Start tracking watch history
function startWatchHistoryTracking() {
    // Save position every 5 seconds
    watchHistoryInterval = setInterval(async () => {
        if (player && !player.paused()) {
            const position = Math.floor(player.currentTime());

            try {
                await fetch(`${API_BASE}/watchhistory`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        movieId: parseInt(movieId),
                        position: position
                    })
                });
            } catch (error) {
                console.error('Error saving watch history:', error);
            }
        }
    }, 5000);

    // Save on page unload
    window.addEventListener('beforeunload', () => {
        if (watchHistoryInterval) {
            clearInterval(watchHistoryInterval);
        }
    });
}

// Get MIME type from file path
function getMimeType(filePath) {
    const extension = filePath.split('.').pop().toLowerCase();
    const mimeTypes = {
        'mp4': 'video/mp4',
        'mkv': 'video/x-matroska',
        'webm': 'video/webm',
        'avi': 'video/x-msvideo',
        'mov': 'video/quicktime'
    };
    return mimeTypes[extension] || 'video/mp4';
}

// Format time in seconds to readable format
function formatTime(seconds) {
    const hours = Math.floor(seconds / 3600);
    const minutes = Math.floor((seconds % 3600) / 60);
    const secs = Math.floor(seconds % 60);

    if (hours > 0) {
        return `${hours}:${minutes.toString().padStart(2, '0')}:${secs.toString().padStart(2, '0')}`;
    }
    return `${minutes}:${secs.toString().padStart(2, '0')}`;
}

// Show notification
function showNotification(message) {
    const notification = document.createElement('div');
    notification.style.cssText = `
        position: fixed;
        bottom: 100px;
        left: 50%;
        transform: translateX(-50%);
        background: rgba(0, 0, 0, 0.9);
        color: white;
        padding: 12px 24px;
        border-radius: 8px;
        font-size: 14px;
        z-index: 10000;
        animation: fadeInOut 3s ease;
    `;
    notification.textContent = message;
    document.body.appendChild(notification);

    setTimeout(() => {
        notification.remove();
    }, 3000);
}

// Add fade in/out animation
const style = document.createElement('style');
style.textContent = `
    @keyframes fadeInOut {
        0% { opacity: 0; transform: translateX(-50%) translateY(20px); }
        10% { opacity: 1; transform: translateX(-50%) translateY(0); }
        90% { opacity: 1; transform: translateX(-50%) translateY(0); }
        100% { opacity: 0; transform: translateX(-50%) translateY(-20px); }
    }
`;
document.head.appendChild(style);
