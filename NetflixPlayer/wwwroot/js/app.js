// API Base URL
const API_BASE = '/api';

// State
let allMovies = [];
let categories = [];

// Initialize app
document.addEventListener('DOMContentLoaded', () => {
    initializeApp();
    setupEventListeners();
});

// Setup event listeners
function setupEventListeners() {
    // Scan button
    document.getElementById('scanBtn').addEventListener('click', scanMovies);

    // Cleanup button
    document.getElementById('cleanupBtn').addEventListener('click', cleanupMovies);

    // Search input
    const searchInput = document.getElementById('searchInput');
    searchInput.addEventListener('input', (e) => {
        filterMovies(e.target.value);
    });

    // Header scroll effect
    window.addEventListener('scroll', () => {
        const header = document.querySelector('.header');
        if (window.scrollY > 50) {
            header.classList.add('scrolled');
        } else {
            header.classList.remove('scrolled');
        }
    });
}

// Initialize application
async function initializeApp() {
    showLoading(true);
    try {
        await loadMovies();
        await loadCategories();
        renderMoviesByCategory();
        updateHeroSection();
    } catch (error) {
        console.error('Error initializing app:', error);
        showToast('Error loading movies. Please try scanning.', 'error');
    } finally {
        showLoading(false);
    }
}

// Load all movies
async function loadMovies() {
    try {
        const response = await fetch(`${API_BASE}/movies`);
        if (!response.ok) throw new Error('Failed to fetch movies');
        allMovies = await response.json();
    } catch (error) {
        console.error('Error loading movies:', error);
        allMovies = [];
    }
}

// Load categories
async function loadCategories() {
    try {
        const response = await fetch(`${API_BASE}/movies/categories`);
        if (!response.ok) throw new Error('Failed to fetch categories');
        categories = await response.json();
    } catch (error) {
        console.error('Error loading categories:', error);
        categories = [];
    }
}

// Scan for new movies
async function scanMovies() {
    const scanBtn = document.getElementById('scanBtn');
    scanBtn.disabled = true;
    scanBtn.textContent = 'Scanning...';

    try {
        const response = await fetch(`${API_BASE}/movies/scan`, {
            method: 'POST'
        });

        if (!response.ok) throw new Error('Scan failed');

        const result = await response.json();
        showToast(`Scan complete! Added ${result.count} new movies.`, 'success');

        // Reload movies
        await initializeApp();
    } catch (error) {
        console.error('Error scanning movies:', error);
        showToast('Error scanning movies. Check console for details.', 'error');
    } finally {
        scanBtn.disabled = false;
        scanBtn.textContent = 'Scan Movies';
    }
}

// Cleanup all movies from database
async function cleanupMovies() {
    const cleanupBtn = document.getElementById('cleanupBtn');
    cleanupBtn.disabled = true;
    cleanupBtn.textContent = 'Cleaning...';

    try {
        const response = await fetch(`${API_BASE}/movies/cleanup`, {
            method: 'POST'
        });

        if (!response.ok) throw new Error('Cleanup failed');

        const result = await response.json();

        if (result.count > 0) {
            showToast(`Cleanup complete! Removed all ${result.count} movies from database.`, 'success');
            // Reload movies
            await initializeApp();
        } else {
            showToast('Database is already empty!', 'info');
        }
    } catch (error) {
        console.error('Error cleaning up movies:', error);
        showToast('Error cleaning up movies. Check console for details.', 'error');
    } finally {
        cleanupBtn.disabled = false;
        cleanupBtn.textContent = 'Cleanup';
    }
}

// Render movies by category
function renderMoviesByCategory() {
    const container = document.getElementById('categoriesContainer');
    const emptyState = document.getElementById('emptyState');

    if (allMovies.length === 0) {
        container.innerHTML = '';
        emptyState.style.display = 'flex';
        return;
    }

    emptyState.style.display = 'none';
    container.innerHTML = '';

    // Group movies by category
    const moviesByCategory = {};
    allMovies.forEach(movie => {
        if (!moviesByCategory[movie.category]) {
            moviesByCategory[movie.category] = [];
        }
        moviesByCategory[movie.category].push(movie);
    });

    // Render each category
    Object.keys(moviesByCategory).sort().forEach(category => {
        const section = createCategorySection(category, moviesByCategory[category]);
        container.appendChild(section);
    });
}

// Create category section
function createCategorySection(categoryName, movies) {
    const section = document.createElement('div');
    section.className = 'category-section';

    const title = document.createElement('h2');
    title.className = 'category-title';
    title.textContent = categoryName;

    const row = document.createElement('div');
    row.className = 'movie-row';

    movies.forEach(movie => {
        const card = createMovieCard(movie);
        row.appendChild(card);
    });

    section.appendChild(title);
    section.appendChild(row);

    return section;
}

// Create movie card
function createMovieCard(movie) {
    const card = document.createElement('div');
    card.className = 'movie-card';
    card.onclick = () => playMovie(movie.id);

    if (movie.coverImagePath) {
        const img = document.createElement('img');
        img.className = 'movie-card-image';
        img.src = `${API_BASE}/movies/${movie.id}/cover`;
        img.alt = movie.title;
        img.onerror = () => {
            // If image fails to load, show placeholder
            card.innerHTML = '';
            card.appendChild(createPlaceholder(movie));
        };
        card.appendChild(img);
    } else {
        card.appendChild(createPlaceholder(movie));
    }

    const overlay = document.createElement('div');
    overlay.className = 'movie-card-overlay';

    const title = document.createElement('h3');
    title.className = 'movie-card-title';
    title.textContent = movie.title;

    const meta = document.createElement('div');
    meta.className = 'movie-card-meta';

    if (movie.duration > 0) {
        const duration = document.createElement('span');
        duration.className = 'movie-card-duration';
        duration.innerHTML = `
            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor">
                <circle cx="12" cy="12" r="10"></circle>
                <polyline points="12 6 12 12 16 14"></polyline>
            </svg>
            ${formatDuration(movie.duration)}
        `;
        meta.appendChild(duration);
    }

    const categoryBadge = document.createElement('span');
    categoryBadge.className = 'movie-card-category';
    categoryBadge.textContent = movie.category;
    meta.appendChild(categoryBadge);

    overlay.appendChild(title);
    overlay.appendChild(meta);
    card.appendChild(overlay);

    return card;
}

// Create placeholder for movies without cover
function createPlaceholder(movie) {
    const placeholder = document.createElement('div');
    placeholder.className = 'movie-card-placeholder';

    placeholder.innerHTML = `
        <svg width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor">
            <rect x="2" y="2" width="20" height="20" rx="2.18" ry="2.18"></rect>
            <line x1="7" y1="2" x2="7" y2="22"></line>
            <line x1="17" y1="2" x2="17" y2="22"></line>
            <line x1="2" y1="12" x2="22" y2="12"></line>
            <line x1="2" y1="7" x2="7" y2="7"></line>
            <line x1="2" y1="17" x2="7" y2="17"></line>
            <line x1="17" y1="17" x2="22" y2="17"></line>
            <line x1="17" y1="7" x2="22" y2="7"></line>
        </svg>
        <h3 class="movie-card-title">${movie.title}</h3>
    `;

    return placeholder;
}

// Play movie
function playMovie(movieId) {
    window.location.href = `player.html?id=${movieId}`;
}

// Update hero section with featured movie
function updateHeroSection() {
    if (allMovies.length === 0) return;

    // Get a random movie for hero
    const featuredMovie = allMovies[Math.floor(Math.random() * allMovies.length)];

    document.getElementById('heroTitle').textContent = featuredMovie.title;
    document.getElementById('heroDescription').textContent =
        featuredMovie.description || 'Enjoy watching this amazing movie from your local library.';

    const playBtn = document.getElementById('heroPlayBtn');
    playBtn.onclick = () => playMovie(featuredMovie.id);

    const infoBtn = document.getElementById('heroInfoBtn');
    infoBtn.onclick = () => playMovie(featuredMovie.id);

    // Set hero background if cover exists
    if (featuredMovie.coverImagePath) {
        const hero = document.querySelector('.hero');
        hero.style.backgroundImage = `url('${API_BASE}/movies/${featuredMovie.id}/cover')`;
        hero.style.backgroundSize = 'cover';
        hero.style.backgroundPosition = 'center';
    }
}

// Filter movies by search query
function filterMovies(query) {
    if (!query.trim()) {
        renderMoviesByCategory();
        return;
    }

    const filtered = allMovies.filter(movie =>
        movie.title.toLowerCase().includes(query.toLowerCase()) ||
        movie.description.toLowerCase().includes(query.toLowerCase()) ||
        movie.category.toLowerCase().includes(query.toLowerCase())
    );

    const container = document.getElementById('categoriesContainer');
    const emptyState = document.getElementById('emptyState');

    if (filtered.length === 0) {
        container.innerHTML = '';
        emptyState.style.display = 'flex';
        emptyState.querySelector('h2').textContent = 'No Results Found';
        emptyState.querySelector('p').textContent = `No movies match "${query}"`;
        return;
    }

    emptyState.style.display = 'none';
    container.innerHTML = '';

    const section = createCategorySection('Search Results', filtered);
    container.appendChild(section);
}

// Format duration (seconds to readable format)
function formatDuration(seconds) {
    if (seconds === 0) return 'Unknown';
    const hours = Math.floor(seconds / 3600);
    const minutes = Math.floor((seconds % 3600) / 60);

    if (hours > 0) {
        return `${hours}h ${minutes}m`;
    }
    return `${minutes}m`;
}

// Show/hide loading state
function showLoading(show) {
    const loadingState = document.getElementById('loadingState');
    loadingState.style.display = show ? 'flex' : 'none';
}

// Show toast notification
function showToast(message, type = 'info') {
    const toast = document.getElementById('toast');
    toast.textContent = message;
    toast.style.background = type === 'error' ? '#f44336' :
        type === 'success' ? '#4CAF50' :
            'rgba(255, 255, 255, 0.95)';
    toast.style.color = type === 'info' ? '#000' : '#fff';
    toast.classList.add('show');

    setTimeout(() => {
        toast.classList.remove('show');
    }, 3000);
}
