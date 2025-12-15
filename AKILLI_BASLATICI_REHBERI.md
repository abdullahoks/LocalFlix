# 🎬 Netflix Player - Akıllı Başlatıcı Kullanım Rehberi

## ✨ Yeni Özellikler

Artık **Netflix Player** ikonu çok daha akıllı!

### 🚀 Akıllı Başlatma Sistemi

Masaüstündeki **Netflix Player** ikonuna çift tıkladığınızda:

1. **Sunucu Kontrolü Yapar**
   - ✅ Sunucu zaten çalışıyorsa → Direkt tarayıcıyı açar
   - ❌ Sunucu çalışmıyorsa → Önce sunucuyu başlatır, sonra tarayıcıyı açar

2. **Otomatik Tarayıcı Açma**
   - Sunucu hazır olduğunda otomatik olarak http://localhost:5000 açılır
   - Manuel olarak adres yazmanıza gerek yok!

3. **Akıllı Bekleme**
   - Sunucu başlatılırken hazır olmasını bekler
   - Hazır olunca tarayıcıyı açar

## 🎯 Kullanım Senaryoları

### Senaryo 1: İlk Başlatma
```
[Siz] → İkona çift tıklarsınız
[Sistem] → "Sunucu çalışmıyor, başlatılıyor..."
[Sistem] → Sunucu başlatılır (yeni terminal penceresi)
[Sistem] → Sunucu hazır olana kadar bekler
[Sistem] → Tarayıcı otomatik açılır
[Siz] → Film izlemeye başlarsınız! 🍿
```

### Senaryo 2: Sunucu Zaten Çalışıyor
```
[Siz] → İkona çift tıklarsınız
[Sistem] → "Sunucu zaten çalışıyor!"
[Sistem] → Tarayıcı hemen açılır
[Siz] → Film izlemeye devam edersiniz! 🎬
```

## 🎨 İkon Hakkında

- **Tasarım**: Netflix tarzı kırmızı-siyah tema
- **Format**: Windows .ico formatı
- **Konum**: `C:\Users\Abdullah OK\Desktop\VsCode\Netflix\netflix_icon.ico`

### İkon Görünmüyorsa?

Eğer ikon hala görünmüyorsa:

1. **Masaüstünü Yenileyin**
   - Masaüstünde sağ tık → "Yenile"

2. **Icon Cache Temizleme**
   - Bilgisayarı yeniden başlatın
   - Veya şu komutu çalıştırın:
   ```powershell
   ie4uinit.exe -show
   ```

3. **Manuel İkon Değiştirme**
   - Kısayola sağ tık → Özellikler
   - "Simgeyi Değiştir" butonuna tıklayın
   - `C:\Users\Abdullah OK\Desktop\VsCode\Netflix\netflix_icon.ico` dosyasını seçin

## 📋 Özellikler Özeti

| Özellik | Açıklama |
|---------|----------|
| ✅ Akıllı Başlatma | Sunucu durumunu kontrol eder |
| ✅ Otomatik Tarayıcı | Hazır olunca tarayıcıyı açar |
| ✅ Çift Başlatma Önleme | Sunucu zaten çalışıyorsa tekrar başlatmaz |
| ✅ Görsel Geri Bildirim | Renkli terminal çıktısı (NETFLIX ASCII art) |
| ✅ Özel İkon | Netflix temalı kırmızı-siyah ikon |

## 🎭 Terminal Görünümü

İkona tıkladığınızda göreceğiniz terminal:

```
  ███╗   ██╗███████╗████████╗███████╗██╗     ██╗██╗  ██╗
  ████╗  ██║██╔════╝╚══██╔══╝██╔════╝██║     ██║╚██╗██╔╝
  ██╔██╗ ██║█████╗     ██║   █████╗  ██║     ██║ ╚███╔╝ 
  ██║╚██╗██║██╔══╝     ██║   ██╔══╝  ██║     ██║ ██╔██╗ 
  ██║ ╚████║███████╗   ██║   ██║     ███████╗██║██╔╝ ██╗
  ╚═╝  ╚═══╝╚══════╝   ╚═╝   ╚═╝     ╚══════╝╚═╝╚═╝  ╚═╝

  PLAYER - Film Kutuphanesi

========================================

[*] Sunucu kontrol ediliyor...
[✓] Sunucu zaten calisiyor!
[*] Tarayici aciliyor...
[✓] Tarayici acildi!

Iyi seyirler! 🍿
```

## 💡 İpuçları

1. **Sunucuyu Kapatmak İçin**
   - Terminal penceresinde `Ctrl+C` yapın
   - Veya terminal penceresini kapatın

2. **Hızlı Erişim**
   - İkonu görev çubuğuna sabitleyin
   - Başlat menüsüne ekleyin

3. **Sorun Giderme**
   - İkon çalışmıyorsa → Sağ tık → "Yönetici olarak çalıştır"
   - Port 5000 meşgulse → Önceki sunucuyu kapatın

---

**Artık tek tıkla Netflix deneyiminiz başlıyor! 🎬✨**
