using SoruBankasi.Entities;

namespace SoruBankasi.Data;

// Sentetik (örnek) veri: veritabanı boşsa bir kere eklenir
public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Kullanicilar.Any()) return; // zaten veri varsa tekrar ekleme

        var ogretmen = new Kullanici { AdSoyad = "Ayşe Yılmaz", Email = "ogretmen@ornek.com", ParolaHash = "HAFTA5", Rol = "Ogretmen" };
        var ogrenci1 = new Kullanici { AdSoyad = "Mehmet Kaya", Email = "ogrenci1@ornek.com", ParolaHash = "HAFTA5", Rol = "Ogrenci" };
        var ogrenci2 = new Kullanici { AdSoyad = "Zeynep Demir", Email = "ogrenci2@ornek.com", ParolaHash = "HAFTA5", Rol = "Ogrenci" };
        db.Kullanicilar.AddRange(ogretmen, ogrenci1, ogrenci2);

        var k1 = new Kazanim { Kod = "M.9.1.1", Ders = "Matematik", Aciklama = "Önermeleri ve doğruluk değerlerini açıklar." };
        var k2 = new Kazanim { Kod = "M.9.2.1", Ders = "Matematik", Aciklama = "Kümelerle ilgili temel kavramları açıklar." };
        var k3 = new Kazanim { Kod = "F.9.1.1", Ders = "Fizik", Aciklama = "Fizik biliminin alt alanlarını açıklar." };
        var k4 = new Kazanim { Kod = "T.9.1.1", Ders = "Türk Dili ve Edebiyatı", Aciklama = "Metnin konusunu ve ana fikrini belirler." };
        db.Kazanimlar.AddRange(k1, k2, k3, k4);

        db.Sorular.AddRange(
            new Soru
            {
                Kazanim = k1,
                Ogretmen = ogretmen,
                Tip = "CoktanSecmeli",
                Zorluk = 1,
                Metin = "\"2 bir asal sayıdır.\" önermesinin doğruluk değeri nedir?",
                Secenekler = new List<Secenek>
                {
                    new() { Metin = "Doğru (1)", DogruMu = true },
                    new() { Metin = "Yanlış (0)" }
                }
            },
            new Soru
            {
                Kazanim = k2,
                Ogretmen = ogretmen,
                Tip = "CoktanSecmeli",
                Zorluk = 2,
                Metin = "A = {1, 2, 3} kümesinin eleman sayısı kaçtır?",
                Secenekler = new List<Secenek>
                {
                    new() { Metin = "2" },
                    new() { Metin = "3", DogruMu = true },
                    new() { Metin = "4" },
                    new() { Metin = "6" }
                }
            },
            new Soru
            {
                Kazanim = k3,
                Ogretmen = ogretmen,
                Tip = "CoktanSecmeli",
                Zorluk = 1,
                Metin = "Aşağıdakilerden hangisi fiziğin alt alanlarından biridir?",
                Secenekler = new List<Secenek>
                {
                    new() { Metin = "Optik", DogruMu = true },
                    new() { Metin = "Genetik" },
                    new() { Metin = "Jeoloji" },
                    new() { Metin = "Botanik" }
                }
            },
            new Soru
            {
                Kazanim = k4,
                Ogretmen = ogretmen,
                Tip = "AcikUclu",
                Zorluk = 3,
                Metin = "Bir metnin ana fikrini bulurken hangi adımları izlersiniz? Açıklayınız.",
                AnahtarNoktalar = "konuyu belirleme; anahtar kelimeler; yazarın amacı; tek cümlelik özet"
            }
        );

        db.SaveChanges();
    }
}