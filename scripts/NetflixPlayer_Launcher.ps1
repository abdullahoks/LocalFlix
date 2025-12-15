# Netflix Player - Akilli Baslatici
# Sunucu kontrolu yapar, gerekirse baslatir ve tarayiciyi acar

$serverUrl = "http://localhost:5000"
$projectPath = "C:\Users\Abdullah OK\Desktop\VsCode\Netflix\NetflixPlayer"

# Renk ayarlari
$host.UI.RawUI.BackgroundColor = "Black"
$host.UI.RawUI.ForegroundColor = "Red"
Clear-Host

Write-Host ""
Write-Host "  ███╗   ██╗███████╗████████╗███████╗██╗     ██╗██╗  ██╗" -ForegroundColor Red
Write-Host "  ████╗  ██║██╔════╝╚══██╔══╝██╔════╝██║     ██║╚██╗██╔╝" -ForegroundColor Red
Write-Host "  ██╔██╗ ██║█████╗     ██║   █████╗  ██║     ██║ ╚███╔╝ " -ForegroundColor Red
Write-Host "  ██║╚██╗██║██╔══╝     ██║   ██╔══╝  ██║     ██║ ██╔██╗ " -ForegroundColor Red
Write-Host "  ██║ ╚████║███████╗   ██║   ██║     ███████╗██║██╔╝ ██╗" -ForegroundColor Red
Write-Host "  ╚═╝  ╚═══╝╚══════╝   ╚═╝   ╚═╝     ╚══════╝╚═╝╚═╝  ╚═╝" -ForegroundColor Red
Write-Host ""
Write-Host "  PLAYER - Film Kutuphanesi" -ForegroundColor White
Write-Host ""
Write-Host "========================================" -ForegroundColor DarkRed
Write-Host ""

# Sunucu kontrolu
Write-Host "[*] Sunucu kontrol ediliyor..." -ForegroundColor Yellow

try {
    $response = Invoke-WebRequest -Uri $serverUrl -Method Head -TimeoutSec 2 -ErrorAction Stop
    Write-Host "[✓] Sunucu zaten calisiyor!" -ForegroundColor Green
    Write-Host ""
    Write-Host "[*] Tarayici aciliyor..." -ForegroundColor Yellow
    Start-Process $serverUrl
    Write-Host "[✓] Tarayici acildi!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Iyi seyirler! 🍿" -ForegroundColor Cyan
    Start-Sleep -Seconds 2
}
catch {
    Write-Host "[!] Sunucu calismiyyor. Baslatiliyor..." -ForegroundColor Yellow
    Write-Host ""
    
    # Sunucuyu baslatmak icin yeni terminal penceresi ac
    $startCommand = "cd '$projectPath'; dotnet run"
    
    Start-Process powershell -ArgumentList "-NoExit", "-Command", $startCommand -WindowStyle Normal
    
    Write-Host "[*] Sunucu baslatildi!" -ForegroundColor Green
    Write-Host "[*] Sunucu hazir olana kadar bekleniyor..." -ForegroundColor Yellow
    
    # Sunucunun hazir olmasini bekle (max 30 saniye)
    $maxAttempts = 30
    $attempt = 0
    $serverReady = $false
    
    while ($attempt -lt $maxAttempts -and -not $serverReady) {
        Start-Sleep -Seconds 1
        $attempt++
        Write-Host "." -NoNewline -ForegroundColor DarkYellow
        
        try {
            $testResponse = Invoke-WebRequest -Uri $serverUrl -Method Head -TimeoutSec 1 -ErrorAction Stop
            $serverReady = $true
        }
        catch {
            # Sunucu henuz hazir degil, devam et
        }
    }
    
    Write-Host ""
    
    if ($serverReady) {
        Write-Host ""
        Write-Host "[✓] Sunucu hazir!" -ForegroundColor Green
        Write-Host "[*] Tarayici aciliyor..." -ForegroundColor Yellow
        Start-Process $serverUrl
        Write-Host "[✓] Tarayici acildi!" -ForegroundColor Green
        Write-Host ""
        Write-Host "Iyi seyirler! 🍿" -ForegroundColor Cyan
        Start-Sleep -Seconds 2
    }
    else {
        Write-Host ""
        Write-Host "[!] Sunucu baslatildi ama hazir olmasi biraz zaman alabilir." -ForegroundColor Yellow
        Write-Host "[*] Lutfen birkaç saniye bekleyip tarayicinizda $serverUrl adresine gidin." -ForegroundColor Cyan
        Write-Host ""
        Read-Host "Devam etmek icin Enter'a basin"
    }
}
