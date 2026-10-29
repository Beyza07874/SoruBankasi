# Kazanım Bazlı Soru Bankası ve Otomatik Sınav

Öğretmenlerin kazanımlara göre etiketlenmiş sorulardan otomatik sınav oluşturmasını ve açık uçlu cevapları yapay zekâ desteğiyle ön değerlendirmesini sağlayan web uygulaması.

## Teknolojiler
- ASP.NET Core MVC (.NET 10)
- SQL Server
- Entity Framework Core

## Mimari (Katmanlı)
- **Web** → Sunum katmanı (Controller, View)
- **SoruBankasi.Business** → İş kuralları
- **SoruBankasi.Data** → Veri erişimi
- **SoruBankasi.Entities** → Varlık sınıfları

## Modüller
- Soru bankası (kazanım etiketli)
- Otomatik sınav oluşturucu
- Sınav uygulama
- Otomatik / yarı otomatik değerlendirme
- Sonuç ve kazanım analizi
- PDF dışa aktarma

## Geliştirici
Beyza Genç – Bingöl Üniversitesi, Bilgisayar Programcılığı

## Veri Modeli (ER Diyagramı)

```mermaid
erDiagram
  KULLANICI ||--o{ SORU : yazar
  KULLANICI ||--o{ SINAV : hazirlar
  KULLANICI ||--o{ SONUC : girer
  KAZANIM ||--o{ SORU : etiketler
  SORU ||--o{ SECENEK : icerir
  SINAV ||--o{ SINAVSORU : icerir
  SORU ||--o{ SINAVSORU : yer_alir
  SINAV ||--o{ SONUC : uretir
  SONUC ||--o{ CEVAP : icerir
  SORU ||--o{ CEVAP : cevaplanir
  KULLANICI {
    int Id PK
    string AdSoyad
    string Email
    string ParolaHash
    string Rol
  }
  KAZANIM {
    int Id PK
    string Kod
    string Aciklama
    string Ders
  }
  SORU {
    int Id PK
    int KazanimId FK
    int OgretmenId FK
    string Metin
    string Tip
    int Zorluk
    string AnahtarNoktalar
  }
  SECENEK {
    int Id PK
    int SoruId FK
    string Metin
    bool DogruMu
  }
  SINAV {
    int Id PK
    int OgretmenId FK
    string Baslik
    datetime Tarih
    int SureDk
  }
  SINAVSORU {
    int SinavId PK
    int SoruId PK
    int SiraNo
    int Puan
  }
  SONUC {
    int Id PK
    int SinavId FK
    int OgrenciId FK
    decimal ToplamPuan
  }
  CEVAP {
    int Id PK
    int SonucId FK
    int SoruId FK
    string VerilenCevap
    decimal Puan
    decimal AiOnPuan
  }
```