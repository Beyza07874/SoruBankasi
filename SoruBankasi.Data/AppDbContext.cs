using Microsoft.EntityFrameworkCore;
using SoruBankasi.Entities;

namespace SoruBankasi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Kullanici> Kullanicilar => Set<Kullanici>();
    public DbSet<Kazanim> Kazanimlar => Set<Kazanim>();
    public DbSet<Soru> Sorular => Set<Soru>();
    public DbSet<Secenek> Secenekler => Set<Secenek>();
    public DbSet<Sinav> Sinavlar => Set<Sinav>();
    public DbSet<SinavSoru> SinavSorular => Set<SinavSoru>();
    public DbSet<Sonuc> Sonuclar => Set<Sonuc>();
    public DbSet<Cevap> Cevaplar => Set<Cevap>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // N-N ara tablo: birleşik birincil anahtar
        modelBuilder.Entity<SinavSoru>()
            .HasKey(x => new { x.SinavId, x.SoruId });

        // Email benzersiz olsun (indeks)
        modelBuilder.Entity<Kullanici>()
            .HasIndex(x => x.Email)
            .IsUnique();

        // Kazanım koduna göre hızlı arama (indeks)
        modelBuilder.Entity<Kazanim>()
            .HasIndex(x => x.Kod);

        // Kullanıcı silinince bağlı kayıtlar otomatik silinmesin
        modelBuilder.Entity<Soru>()
            .HasOne(x => x.Ogretmen).WithMany(x => x.Sorular)
            .HasForeignKey(x => x.OgretmenId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Sinav>()
            .HasOne(x => x.Ogretmen).WithMany(x => x.Sinavlar)
            .HasForeignKey(x => x.OgretmenId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Sonuc>()
            .HasOne(x => x.Ogrenci).WithMany(x => x.Sonuclar)
            .HasForeignKey(x => x.OgrenciId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Cevap>()
            .HasOne(x => x.Soru).WithMany(x => x.Cevaplar)
            .HasForeignKey(x => x.SoruId)
            .OnDelete(DeleteBehavior.Restrict);

        // Ondalık alanların hassasiyeti
        modelBuilder.Entity<Sonuc>().Property(x => x.ToplamPuan).HasPrecision(6, 2);
        modelBuilder.Entity<Cevap>().Property(x => x.Puan).HasPrecision(6, 2);
        modelBuilder.Entity<Cevap>().Property(x => x.AiOnPuan).HasPrecision(6, 2);
    }
}