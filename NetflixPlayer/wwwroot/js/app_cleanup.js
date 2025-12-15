// Cleanup deleted movies
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
            showToast(`Cleanup complete! Removed ${result.count} deleted movies.`, 'success');
            // Reload movies
            await initializeApp();
        } else {
            showToast('No deleted movies found. Database is clean!', 'info');
        }
    } catch (error) {
        console.error('Error cleaning up movies:', error);
        showToast('Error cleaning up movies. Check console for details.', 'error');
    } finally {
        cleanupBtn.disabled = false;
        cleanupBtn.textContent = 'Cleanup';
    }
}
