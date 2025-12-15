# GitHub Kurulum Scripti v3
Write-Host "GitHub Kurulumu Basliyor..." -ForegroundColor Green

# 0. Calisma dizinini ayarla (KRITIK ADIM)
# Scriptin kendi oldugu klasöre gecis yapiyoruz
Set-Location $PSScriptRoot
Write-Host "Calisma dizini ayarlandi: $PWD" -ForegroundColor Gray

# 1. Git kontrolu
if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    Write-Host "HATA: Git yuklu degil." -ForegroundColor Red
    Read-Host "Cikis icin Enter'a basin"
    exit
}

# 2. Kimlik Kontrolu ve Ayarlama
$gitEmail = git config --global user.email
if ([string]::IsNullOrWhiteSpace($gitEmail)) {
    Write-Host "Git kimlik bilgileri eksik. Lutfen asagidaki bilgileri girin:" -ForegroundColor Yellow
    $email = Read-Host "Email Adresiniz"
    $name = Read-Host "Adiniz Soyadiniz"
    
    if (-not [string]::IsNullOrWhiteSpace($email)) {
        git config --global user.email "$email"
        git config --global user.name "$name"
        Write-Host "Kimlik bilgileri kaydedildi." -ForegroundColor Green
    }
}

# 3. Repo baslatma
if (-not (Test-Path ".git")) {
    Write-Host "Git deposu baslatiliyor..."
    git init
}

# 4. Dosyalari ekleme
Write-Host "Dosyalar hazirlaniyor..."
git add .

# 5. Commit
Write-Host "Versiyon olusturuluyor..."
git commit --allow-empty -m "Initial commit: LocalFlix v1.0"

# 6. Branch
git branch -M main

# 7. Remote URL
Write-Host "`nGitHub Repository Linki:" -ForegroundColor Yellow
$remoteUrl = Read-Host "Link (Ornek: https://github.com/abdullahoks/LocalFlix.git)"

if ([string]::IsNullOrWhiteSpace($remoteUrl)) {
    Write-Host "Link girilmedi. Cikis yapiliyor." -ForegroundColor Red
    exit
}

# Remote ekle/guncelle ve baglantiyi dogrula
if (git remote | Select-String "origin") {
    git remote set-url origin $remoteUrl
}
else {
    git remote add origin $remoteUrl
}

# 8. Push (Zorla Yukleme - Force Push)
# Not: Eger GitHub'da README varsa, cakisma olmamasi icin -f kullaniyoruz.
Write-Host "`nGitHub'a yukleniyor..." -ForegroundColor Cyan
git push -u origin main -f

if ($?) {
    Write-Host "`nBASARILI! Projeniz GitHub'a yuklendi. Sayfayi yenileyin." -ForegroundColor Green
}
else {
    Write-Host "`nHATA: Yukleme basarisiz. Linki kontrol edin." -ForegroundColor Red
}

Read-Host "Cikis icin Enter'a basin"
