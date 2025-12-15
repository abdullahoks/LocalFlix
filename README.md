# LocalFlix 🎬

LocalFlix, kendi bilgisayarınızdaki film arşivinizi Netflix tarzı modern bir arayüzle izlemenizi sağlayan, açık kaynaklı bir video oynatıcıdır.

<img width="1200" alt="image" src="https://github.com/user-attachments/assets/bd2a81f1-701f-47d6-978c-16b46bbd67b2" />


## ✨ Özellikler

- **Modern Arayüz:** Netflix tarzı karanlık tema ve akıcı animasyonlar.
- **Otomatik Tarama:** Klasördeki filmleri, kapak fotoğraflarını ve altyazıları otomatik tanır.
- **Akıllı Altyazı:** Türkçe karakter sorunlarını otomatik çözer (UTF-8 dönüşümü).
- **Kaldığı Yerden Devam:** Filmi kapatsanız bile kaldığınız yerden devam eder.
- **Hız Kontrolü:** 0.5x, 1x, 1.5x, 2x oynatma hızı seçenekleri.
- **Gelişmiş Player:** Siyah kontrol barı, büyük play butonu ve entegre kontroller.

## 🚀 Kurulum

1. **Gereksinimler:**
   - .NET SDK 8.0 veya üzeri (10.0 önerilir)
   - Windows/Mac/Linux işletim sistemi

2. **Projeyi Çalıştırma:**
   ```bash
   git clone https://github.com/KULLANICI_ADINIZ/LocalFlix.git
   cd LocalFlix/NetflixPlayer
   dotnet run
   ```

3. **Tarayıcıda Açma:**
   - http://localhost:5000 adresine gidin.

## 📁 Klasör Yapısı

Filmlerinizi aşağıdaki yapıda düzenlerseniz otomatik tanınır:
```
C:\Movies\
    ├── Aksiyon\
    │   ├── Matrix.mp4
    │   ├── Matrix.srt
    │   └── Matrix.jpg
```

## 🛠️ Teknolojiler

- **Backend:** ASP.NET Core 10.0, Entity Framework Core (SQLite)
- **Frontend:** HTML5, CSS3, JavaScript (Vanilla)
- **Player:** Video.js

## 📄 Lisans

Bu proje MIT lisansı ile lisanslanmıştır.
