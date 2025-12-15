# Netflix Player Icon Converter - FIXED VERSION
# JPG to ICO converter for Windows shortcut

Add-Type -AssemblyName System.Drawing

$jpgPath = "C:\Users\Abdullah OK\Desktop\VsCode\Netflix\netflix_logo.jpg"
$icoPath = "C:\Users\Abdullah OK\netflix_icon.ico"

try {
    Write-Host "Logo yukleniyor..." -ForegroundColor Yellow
    
    # Load the JPG image
    $img = [System.Drawing.Image]::FromFile($jpgPath)
    
    Write-Host "Ikon olusturuluyor..." -ForegroundColor Yellow
    
    # Resize to 256x256 for icon
    $bitmap = New-Object System.Drawing.Bitmap(256, 256)
    $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
    $graphics.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
    $graphics.DrawImage($img, 0, 0, 256, 256)
    $graphics.Dispose()
    
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
    Write-Host "BASARILI! Ikon olusturuldu!" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "Ikon konumu: $icoPath" -ForegroundColor Cyan
    Write-Host ""
    
    # Update shortcut icon
    Write-Host "Kisayol guncelleniyor..." -ForegroundColor Yellow
    $WshShell = New-Object -ComObject WScript.Shell
    $Shortcut = $WshShell.CreateShortcut("$env:USERPROFILE\Desktop\Netflix Player.lnk")
    $Shortcut.IconLocation = $icoPath
    $Shortcut.Save()
    
    Write-Host "Kisayol guncellendi!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Simdi masaustunu yenileyin (Sag tik -> Yenile)" -ForegroundColor Cyan
    Write-Host ""
}
catch {
    Write-Host "HATA: $_" -ForegroundColor Red
    Write-Host ""
    Write-Host "Detay: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "Devam etmek icin bir tusa basin..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
