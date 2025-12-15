# 🐙 GitHub Paylaşım Rehberi

Projeniz **LocalFlix** artık GitHub'a yüklenmeye hazır!

Sizin için gerekli tüm hazırlıkları yaptım:
1.  ✅ `.gitignore` dosyası oluşturuldu (Gereksiz dosyalar hariç tutuldu).
2.  ✅ `README.md` dosyası düzenlendi (LocalFlix ismiyle güncellendi).
3.  ✅ Git deposu oluşturuldu ve ilk kayıt (commit) yapıldı.

Şimdi sadece aşağıdaki adımları takip ederek kodlarınızı internete yüklemeniz kaldı.

## 🚀 Adım Adım Yükleme

### 1. GitHub'da Repo Oluşturun
1. [github.com](https://github.com) adresine gidin ve giriş yapın.
2. Sağ üst köşedeki **+** işaretine tıklayın ve **"New repository"** seçin.
3. **Repository name** kısmına `LocalFlix` yazın.
4. **Public** (herkese açık) veya **Private** (sadece size özel) seçin.
5. **ÖNEMLİ:** "Add a README file", ".gitignore" veya "license" seçeneklerini **İŞARETLEMEYİN** (boş bırakın).
6. **"Create repository"** butonuna tıklayın.

### 2. Kodları Yükleyin

Repo oluşturulduktan sonra karşınıza çıkan sayfada **"…or push an existing repository from the command line"** başlığını bulun. Oradaki 3 satırlık kodu kopyalayıp terminale yapıştıracağız.

Kodlar şuna benzer olacaktır (kendi kullanıcı adınızı dikkate alın):

```bash
git remote add origin https://github.com/abdullahoks/LocalFlix.git
git branch -M main
git push -u origin main
```

**Bu işlemi yapmak için:**

1. VS Code terminalini açın (Ctrl + `).
2. Yukarıdaki gibi size özel verilen kodları sırasıyla yapıştırıp Enter'a basın.
3. Eğer GitHub giriş yapmanızı isterse, açılan pencereden giriş yapın/izin verin.

### 🎉 Sonuç

İşlem bitince terminalde şuna benzer bir mesaj göreceksiniz:
`Branch 'main' set up to track remote branch 'main' from 'origin'.`

Artık GitHub sayfanızı yenilediğinizde projenizin yüklendiğini göreceksiniz!

## 💡 İpuçları

- **Güncelleme Yapmak İçin:**
  İleride kodda değişiklik yaparsanız, terminalde şu komutları yazarak güncelleyebilirsiniz:
  ```bash
  git add .
  git commit -m "Yeni özellik eklendi"
  git push
  ```

Tebrikler! Projeniz artık dünyayla paylaşılmaya hazır. 🌍
