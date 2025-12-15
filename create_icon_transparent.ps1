# Netflix Player Icon Converter - TRANSPARENT VERSION
# PNG (transparent) to ICO converter for Windows shortcut

Add-Type -AssemblyName System.Drawing

$pngPath = "C:\Users\Abdullah OK\Desktop\VsCode\Netflix\netflix_logo_transparent.png"
$icoPath = "C:\Users\Abdullah OK\netflix_icon.ico"

try {
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "  NETFLIX PLAYER - IKON OLUSTURUCU" -ForegroundColor Red
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "[*] Seffaf logo yukleniyor..." -ForegroundColor Yellow
    
    # Load the PNG image (with transparency)
    $img = [System.Drawing.Image]::FromFile($pngPath)
    
    Write-Host "[*] Ikon olusturuluyor (256x256)..." -ForegroundColor Yellow
    
    # Create a new bitmap with transparency support
    $bitmap = New-Object System.Drawing.Bitmap(256, 256, [System.Drawing.Imaging.PixelFormat]::Format32bppArgb)
    $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
    
    # Set high quality rendering
    $graphics.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
    $graphics.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::HighQuality
    $graphics.PixelOffsetMode = [System.Drawing.Drawing2D.PixelOffsetMode]::HighQuality
    $graphics.CompositingQuality = [System.Drawing.Drawing2D.CompositingQuality]::HighQuality
    
    # Clear with transparent background
    $graphics.Clear([System.Drawing.Color]::Transparent)
    
    # Draw the image
    $graphics.DrawImage($img, 0, 0, 256, 256)
    $graphics.Dispose()
    
    Write-Host "[*] ICO formatina donusturuluyor..." -ForegroundColor Yellow
    
    # Convert to icon and save
    $icon = [System.Drawing.Icon]::FromHandle($bitmap.GetHicon())
    $fileStream = [System.IO.File]::Create($icoPath)
    $icon.Save($fileStream)
    $fileStream.Close()
    
    # Clean up
    $img.Dispose()
    $bitmap.Dispose()
    
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Green
    Write-Host "  BASARILI! Seffaf ikon olusturuldu!" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "[+] Ikon konumu: $icoPath" -ForegroundColor Cyan
    Write-Host "[+] Seffaf arka plan: EVET" -ForegroundColor Cyan
    Write-Host "[+] Boyut: 256x256 piksel" -ForegroundColor Cyan
    Write-Host ""
    
    # Update shortcut icon
    Write-Host "[*] Kisayol guncelleniyor..." -ForegroundColor Yellow
    $WshShell = New-Object -ComObject WScript.Shell
    $Shortcut = $WshShell.CreateShortcut("$env:USERPROFILE\Desktop\Netflix Player.lnk")
    $Shortcut.IconLocation = $icoPath
    $Shortcut.Save()
    
    Write-Host "[+] Kisayol guncellendi!" -ForegroundColor Green
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "SON ADIM:" -ForegroundColor Yellow
    Write-Host "Masaustunu yenileyin (Sag tik -> Yenile)" -ForegroundColor White
    Write-Host ""
    Write-Host "Seffaf arka planli logo cok daha guzel gorunecek!" -ForegroundColor Cyan
    Write-Host ""
}
catch {
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Red
    Write-Host "  HATA!" -ForegroundColor Red
    Write-Host "========================================" -ForegroundColor Red
    Write-Host ""
    Write-Host "Hata: $_" -ForegroundColor Red
    Write-Host ""
    Write-Host "Detay: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
}

Write-Host "Devam etmek icin bir tusa basin..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
