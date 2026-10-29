using Microsoft.EntityFrameworkCore;
using SoruBankasi.Data;
using SoruBankasi.Entities;

namespace SoruBankasi.Business;

// Kazanımlarla ilgili iş kuralları burada
public class KazanimService
{
    private readonly AppDbContext _db;

    public KazanimService(AppDbContext db)
    {
        _db = db;
    }

    // Listele + ara (filtreleme)
    public Task<List<Kazanim>> ListeleAsync(string? arama)
    {
        var sorgu = _db.Kazanimlar.AsQueryable();

        if (!string.IsNullOrWhiteSpace(arama))
        {
            sorgu = sorgu.Where(k => k.Kod.Contains(arama)
                                  || k.Aciklama.Contains(arama)
                                  || k.Ders.Contains(arama));
        }

        return sorgu.OrderBy(k => k.Kod).ToListAsync();
    }

    public async Task<Kazanim?> GetirAsync(int id)
    {
        return await _db.Kazanimlar.FindAsync(id);
    }

    // Ekle: aynı kod iki kez eklenemez
    public async Task<string?> EkleAsync(Kazanim kazanim)
    {
        if (await _db.Kazanimlar.AnyAsync(k => k.Kod == kazanim.Kod))
            return "Bu kazanım kodu zaten var.";

        _db.Kazanimlar.Add(kazanim);
        await _db.SaveChangesAsync();
        return null; // null = hata yok
    }

    // Güncelle: başka bir kazanımın kodunu alamaz
    public async Task<string?> GuncelleAsync(Kazanim kazanim)
    {
        if (await _db.Kazanimlar.AnyAsync(k => k.Kod == kazanim.Kod && k.Id != kazanim.Id))
            return "Bu kazanım kodu başka bir kayıtta kullanılıyor.";

        _db.Kazanimlar.Update(kazanim);
        await _db.SaveChangesAsync();
        return null;
    }

    // Sil: bağlı soru varsa silinmez
    public async Task<string?> SilAsync(int id)
    {
        var kazanim = await _db.Kazanimlar.FindAsync(id);
        if (kazanim == null)
            return "Kazanım bulunamadı.";

        if (await _db.Sorular.AnyAsync(s => s.KazanimId == id))
            return "Bu kazanıma bağlı sorular var, önce soruları silin.";

        _db.Kazanimlar.Remove(kazanim);
        await _db.SaveChangesAsync();
        return null;
    }
}