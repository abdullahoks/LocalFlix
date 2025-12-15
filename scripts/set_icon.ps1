# Netflix Player Icon Converter
# PNG to ICO converter for Windows shortcut

Add-Type -AssemblyName System.Drawing

$pngPath = "C:\Users\Abdullah OK\Desktop\VsCode\Netflix\netflix_icon.png"
$icoPath = "C:\Users\Abdullah OK\Desktop\VsCode\Netflix\netflix_icon.ico"

try {
    # Load the PNG image
    $img = [System.Drawing.Image]::FromFile($pngPath)
    
    # Create icon from image
    $icon = [System.Drawing.Icon]::FromHandle(([System.Drawing.Bitmap]$img).GetHicon())
    
    # Save as ICO
    $stream = [System.IO.File]::Create($icoPath)
    $icon.Save($stream)
    $stream.Close()
    
    Write-Host "Icon created successfully: $icoPath" -ForegroundColor Green
    
    # Update shortcut icon
    $WshShell = New-Object -ComObject WScript.Shell
    $Shortcut = $WshShell.CreateShortcut("$env:USERPROFILE\Desktop\Netflix Player.lnk")
    $Shortcut.IconLocation = $icoPath
    $Shortcut.Save()
    
    Write-Host "Shortcut icon updated!" -ForegroundColor Green
}
catch {
    Write-Host "Error: $_" -ForegroundColor Red
}
