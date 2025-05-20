Gercek Zamanlı Chat Uygulaması
Bu proje, C#, ASP.NET Core, SignalR ve FTP protokolü kullanılarak geliştirilmiştir. Kullanıcı yönetimi, arkadaş listesi, anlık mesajlaşma, medya gönderimi, ekran paylaşımı gibi birçok özelliği içermektedir.

Kullanılan Teknolojiler
C# / .NET Core: Backend geliştirme

ASP.NET Core: Web API ve kullanıcı işlemleri

SignalR: Gerçek zamanlı mesajlaşma ve bildirim sistemi

FTP Protokolü: Kullanıcı görsellerinin sunucuya yüklenmesi

Localhost Test Ortamı: Geliştirme ve test süreci

Yeni Hesap Oluşturma
Kullanıcıdan e-posta adresi ve telefon numarası istenir.

Bu bilgiler sistemde kayıtlıysa, kullanıcıdan farklı bir e-posta ve telefon numarası girmesi talep edilir.

"Resim Seç" alanında:

Maksimum dosya boyutu: 50 MB

50 MB üzerindeki dosyalar reddedilir.

Yüklenen resimler, FTP sunucusuna gönderilir ve orada saklanır.

Şifremi Unuttum
Kullanıcının e-posta adresi sistemde kontrol edilir.

Eğer e-posta adresi kayıtlıysa:

Kullanıcının e-posta adresine token (şifre sıfırlama anahtarı) gönderilir.

Kullanıcı, bu token ile şifresini sıfırlayabilir.

Ana Sayfa
Kullanıcılar telefon numarası ile arkadaş ekleyebilir.

Eklenen arkadaşlar, isimlerine göre filtrelenebilir.

Her arkadaşın üzerine sağ tıklanarak aşağıdaki işlemler yapılabilir:

Silme

Engelleme

Sohbet başlatmak için:

Arkadaş üzerine sol tıklanması yeterlidir.

Alternatif olarak, bildirim panelindeki mesajlara tıklayarak ilgili sohbete direkt geçiş yapılabilir.

Bir arkadaş:

Adını veya profil resmini değiştirdiğinde,

Bu değişiklik anlık olarak sizin ekranınıza da yansır.

Yeni mesaj geldiğinde:

Sohbet ekranının üst kısmında bildirim videosu oynatılır.

Bildirim panelinde anlık güncelleme yapılır.



Sohbet Ekranı
Gerçek zamanlı mesajlaşma desteklenir (SignalR ile).

Kullanıcılar şu dosya türlerini gönderebilir:

Video

Resim

Klasör

Emoji

Maksimum 5 dakikalık ses kaydı

Sesli görüşme ve ekran paylaşımı özellikleri bulunmaktadır:

Bu özellikler yalnızca local (yerel) ortamda test edilmiştir.

Ekran paylaşımı SignalR ile sağlanmaktadır ve:

Maksimum 144p çözünürlük desteklenmektedir.

Daha yüksek çözünürlüklerde sistem çökme sorunu yaşamaktadır.

Bu sınırlama, SignalR’in yüksek bant genişliği gerektiren veri akışlarında yetersiz kalmasından kaynaklanmaktadır.
