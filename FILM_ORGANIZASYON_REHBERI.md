# 🎬 Film Klasörü Organizasyon Rehberi

## 📁 Klasör Yapısı

Film klasörünüz şu şekilde organize edildi:

```
C:\Movies\
├── Aksiyon\
├── Komedi\
├── Drama\
├── Bilim Kurgu\
├── Korku\
├── Romantik\
├── Animasyon\
└── Belgesel\
```

## 📝 Film Ekleme Kuralları

### 1. Dosya Adlandırma
Film dosyalarınızı şu formatta adlandırın:
```
Film Adı (Yıl).mp4
```

**Örnekler:**
- `Inception (2010).mp4`
- `The Dark Knight (2008).mp4`
- `Interstellar (2014).mp4`

### 2. Altyazı Dosyaları
Altyazı dosyası, video dosyası ile **aynı isimde** olmalı:
```
Film Adı (Yıl).mp4
Film Adı (Yıl).srt
```

**Örnek:**
```
C:\Movies\Bilim Kurgu\
├── Interstellar (2014).mp4
└── Interstellar (2014).srt
```

### 3. Kapak Resimleri (Opsiyonel)
Daha güzel görünüm için kapak resmi ekleyebilirsiniz:
```
Film Adı (Yıl).mp4
Film Adı (Yıl).jpg
```

**Örnek:**
```
C:\Movies\Aksiyon\
├── The Dark Knight (2008).mp4
├── The Dark Knight (2008).srt
└── The Dark Knight (2008).jpg
```

## 🎯 Örnek Tam Klasör Yapısı

```
C:\Movies\
│
├── Aksiyon\
│   ├── The Dark Knight (2008).mp4
│   ├── The Dark Knight (2008).srt
│   ├── The Dark Knight (2008).jpg
│   ├── Mad Max Fury Road (2015).mp4
│   └── Mad Max Fury Road (2015).srt
│
├── Bilim Kurgu\
│   ├── Interstellar (2014).mp4
│   ├── Interstellar (2014).srt
│   ├── Inception (2010).mp4
│   └── Inception (2010).srt
│
├── Komedi\
│   ├── The Grand Budapest Hotel (2014).mp4
│   └── The Grand Budapest Hotel (2014).srt
│
└── Drama\
    ├── The Shawshank Redemption (1994).mp4
    └── The Shawshank Redemption (1994).srt
```

## ✅ Desteklenen Video Formatları

- ✅ `.mp4` (En iyi uyumluluk - **Önerilen**)
- ✅ `.webm`
- ✅ `.mkv` (Tarayıcı desteği sınırlı olabilir)
- ✅ `.avi`
- ✅ `.mov`

> **Not:** En iyi uyumluluk için `.mp4` formatı önerilir (H.264 codec)

## 🔄 Film Tarama İşlemi

Filmlerinizi ekledikten sonra:

1. Tarayıcıda http://localhost:5000 adresine gidin
2. Üst menüden **"Scan Movies"** butonuna tıklayın
3. Tarama tamamlanana kadar bekleyin
4. Filmleriniz kategorilere göre görünecektir!

## 💡 İpuçları

### Kapak Resmi Bulma
- Google'da "Film Adı poster" araması yapın
- IMDb veya TMDB'den indirebilirsiniz
- Boyut: 300x450 px veya daha büyük önerilir

### Altyazı Bulma
- **OpenSubtitles.org**
- **Subscene.com**
- **YIFY Subtitles**

### Dosya Organizasyonu
- Her kategori için ayrı klasör kullanın
- Dosya isimlerinde özel karakterler kullanmayın
- Türkçe karakter kullanabilirsiniz
- Yıl bilgisi eklemeyi unutmayın

## 🎨 Kategori Önerileri

Kendi kategorilerinizi de ekleyebilirsiniz:

```powershell
New-Item -ItemType Directory -Force -Path "C:\Movies\Gerilim"
New-Item -ItemType Directory -Force -Path "C:\Movies\Macera"
New-Item -ItemType Directory -Force -Path "C:\Movies\Fantastik"
```

## 📊 Örnek Kullanım

### Film Ekleme Adımları:

1. **Film dosyasını indirin** (örn: `Inception.mp4`)
2. **Yeniden adlandırın**: `Inception (2010).mp4`
3. **Doğru kategoriye taşıyın**: `C:\Movies\Bilim Kurgu\`
4. **Altyazı ekleyin** (varsa): `Inception (2010).srt`
5. **Kapak resmi ekleyin** (opsiyonel): `Inception (2010).jpg`
6. **Uygulamada "Scan Movies"** butonuna tıklayın

---

**Hazır! Artık filmlerinizi ekleyip Netflix tarzı kütüphanenizin keyfini çıkarabilirsiniz! 🍿**
