# 🚀 Netflix Player Kullanım Rehberi

## Sunucu Nasıl Başlatılır?

### Yöntem 1: Batch Dosyası ile (En Kolay) ⭐
1. **"Netflix Player BASLAT.bat"** dosyasına çift tıklayın
2. Siyah terminal penceresi açılacak
3. "Now listening on: http://localhost:5000" yazısını görünce hazır!
4. Tarayıcıda http://localhost:5000 adresine gidin

### Yöntem 2: Manuel Başlatma
Terminal'de şu komutları çalıştırın:
```powershell
cd "C:\Users\Abdullah OK\Desktop\VsCode\Netflix\NetflixPlayer"
dotnet run
```

## ❓ Sık Sorulan Sorular

### Bilgisayarı kapattığımda ne olur?
- ❌ Sunucu kapanır
- ❌ Localhost erişilemez hale gelir
- ✅ Verileriniz (film listesi, izleme geçmişi) kaybolmaz - SQLite veritabanında saklanır

### Tekrar film izlemek istediğimde?
1. **"Netflix Player BASLAT.bat"** dosyasına çift tıklayın
2. Tarayıcıda http://localhost:5000 adresine gidin
3. Filmleriniz ve izleme geçmişiniz aynen duruyor!

### Terminal penceresini kapatırsam?
- ❌ Sunucu kapanır
- ✅ Tekrar başlatabilirsiniz

### Sunucuyu nasıl durdururum?
Terminal penceresinde **Ctrl+C** tuşlarına basın veya pencereyi kapatın.

## 💾 Verileriniz Güvende

Sunucu kapansa bile:
- ✅ Film listeniz kaybolmaz (netflix.db dosyasında)
- ✅ İzleme geçmişiniz kaybolmaz
- ✅ Kategoriler kaybolmaz

Sadece sunucuyu tekrar başlatmanız yeterli!

## 🎬 Kullanım Akışı

### İlk Kullanım:
1. **"Netflix Player BASLAT.bat"** çalıştır
2. Tarayıcıda http://localhost:5000 aç
3. "Scan Movies" butonuna tıkla
4. Filmlerini izle!

### Sonraki Kullanımlar:
1. **"Netflix Player BASLAT.bat"** çalıştır
2. Tarayıcıda http://localhost:5000 aç
3. İzlemeye devam et! (Tarama gerekmez)

## ⚡ Hızlı Başlatma İpucu

**"Netflix Player BASLAT.bat"** dosyasını:
- Masaüstüne kopyalayın
- Görev çubuğuna sabitleyin
- Başlat menüsüne ekleyin

Böylece her zaman hızlıca başlatabilirsiniz!

## 🔧 Sorun Giderme

### "dotnet komutu bulunamadı" hatası
- .NET SDK yüklü değil
- Bilgisayarı yeniden başlatın

### Port 5000 kullanımda hatası
- Başka bir uygulama 5000 portunu kullanıyor
- Terminal'de önceki sunucu hala çalışıyor olabilir
- Çözüm: Tüm terminal pencerelerini kapatıp tekrar deneyin

### Filmler görünmüyor
- "Scan Movies" butonuna tıklayın
- C:\Movies klasörüne film ekleyin

---

**Özet:** Bilgisayarı her açtığınızda veya film izlemek istediğinizde sadece **"Netflix Player BASLAT.bat"** dosyasına çift tıklayın! 🎬
