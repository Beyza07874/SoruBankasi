namespace SoruBankasi.Entities;

public class Kullanici
{
    public int Id { get; set; }
    public string AdSoyad { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ParolaHash { get; set; } = string.Empty;
    public string Rol { get; set; } = "Ogrenci";

    public ICollection<Soru> Sorular { get; set; } = new List<Soru>();
    public ICollection<Sinav> Sinavlar { get; set; } = new List<Sinav>();
    public ICollection<Sonuc> Sonuclar { get; set; } = new List<Sonuc>();
}

public class Kazanim
{
    public int Id { get; set; }
    public string Kod { get; set; } = string.Empty;
    public string Aciklama { get; set; } = string.Empty;
    public string Ders { get; set; } = string.Empty;

    public ICollection<Soru> Sorular { get; set; } = new List<Soru>();
}

public class Soru
{
    public int Id { get; set; }
    public int KazanimId { get; set; }
    public int OgretmenId { get; set; }
    public string Metin { get; set; } = string.Empty;
    public string Tip { get; set; } = "CoktanSecmeli";
    public int Zorluk { get; set; }
    public string? AnahtarNoktalar { get; set; }

    public Kazanim Kazanim { get; set; } = null!;
    public Kullanici Ogretmen { get; set; } = null!;
    public ICollection<Secenek> Secenekler { get; set; } = new List<Secenek>();
    public ICollection<SinavSoru> SinavSorular { get; set; } = new List<SinavSoru>();
    public ICollection<Cevap> Cevaplar { get; set; } = new List<Cevap>();
}

public class Secenek
{
    public int Id { get; set; }
    public int SoruId { get; set; }
    public string Metin { get; set; } = string.Empty;
    public bool DogruMu { get; set; }

    public Soru Soru { get; set; } = null!;
}

public class Sinav
{
    public int Id { get; set; }
    public int OgretmenId { get; set; }
    public string Baslik { get; set; } = string.Empty;
    public DateTime Tarih { get; set; }
    public int SureDk { get; set; }

    public Kullanici Ogretmen { get; set; } = null!;
    public ICollection<SinavSoru> SinavSorular { get; set; } = new List<SinavSoru>();
    public ICollection<Sonuc> Sonuclar { get; set; } = new List<Sonuc>();
}

public class SinavSoru
{
    public int SinavId { get; set; }
    public int SoruId { get; set; }
    public int SiraNo { get; set; }
    public int Puan { get; set; }

    public Sinav Sinav { get; set; } = null!;
    public Soru Soru { get; set; } = null!;
}

public class Sonuc
{
    public int Id { get; set; }
    public int SinavId { get; set; }
    public int OgrenciId { get; set; }
    public decimal ToplamPuan { get; set; }

    public Sinav Sinav { get; set; } = null!;
    public Kullanici Ogrenci { get; set; } = null!;
    public ICollection<Cevap> Cevaplar { get; set; } = new List<Cevap>();
}

public class Cevap
{
    public int Id { get; set; }
    public int SonucId { get; set; }
    public int SoruId { get; set; }
    public string VerilenCevap { get; set; } = string.Empty;
    public decimal? Puan { get; set; }
    public decimal? AiOnPuan { get; set; }

    public Sonuc Sonuc { get; set; } = null!;
    public Soru Soru { get; set; } = null!;
}