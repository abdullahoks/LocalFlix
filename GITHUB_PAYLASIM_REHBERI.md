# GitHub'da Paylaşma Rehberi 🐙

Projeniz GitHub için hazırlandı! Aşağıdaki adımları takiperek projenizi GitHub'a yükleyebilirsiniz.

## 1. Hazırlık
Projenize şunlar eklendi:
- ✅ `.gitignore` (Gereksiz dosyaların yüklenmesini engeller)
- ✅ `README.md` (Proje hakkında bilgi veren kapak sayfası)

## 2. GitHub'da Depo (Repository) Oluşturma

1. [github.com](https://github.com) adresine gidin ve giriş yapın.
2. Sağ üstteki **+** ikonuna tıklayıp **New repository** seçin.
3. **Repository name** kısmına `LocalFlix` yazın.
4. **Public** (görülebilir) veya **Private** (gizli) seçin.
5. **Create repository** butonuna tıklayın.

## 3. Projeyi Yükleme (Otomatik Yöntem)

Sizin için bir script hazırladım. Sadece şunları yapın:

1. Masaüstündeki `SETUP_GITHUB.ps1` dosyasına sağ tıklayın ve **"Run with PowerShell"** deyin.
2. Script size GitHub linkini soracak.
3. GitHub'da oluşturduğunuz linki yapıştırın (Örn: `https://github.com/kullaniciadi/LocalFlix.git`).

-- VEYA --

## 3. Projeyi Yükleme (Manuel Yöntem)

Terminal penceresinde (VS Code içinde) sırasıyla şunları yazın:

```bash
# 1. Git'i başlat
git init

# 2. Dosyaları ekle
git add .

# 3. İlk kaydı oluştur
git commit -m "İlk sürüm: LocalFlix v1.0"

# 4. Ana dalı adlandır
git branch -M main

# 5. Uzak sunucuyu ekle (Link kısmını kendi linkinizle değiştirin!)
git remote add origin https://github.com/KULLANICI_ADI/LocalFlix.git

# 6. Yükle
git push -u origin main
```

## 🎉 Sonuç
Bu işlemlerden sonra GitHub sayfanızı yenileyin. Kodlarınızın orada olduğunu göreceksiniz!
