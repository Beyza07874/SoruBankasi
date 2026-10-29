using Microsoft.AspNetCore.Mvc;
using SoruBankasi.Business;
using SoruBankasi.Entities;

namespace SoruBankasi.Web.Controllers;

public class KazanimController : Controller
{
    private readonly KazanimService _servis;

    public KazanimController(KazanimService servis)
    {
        _servis = servis;
    }

    // LİSTELE + ARA
    public async Task<IActionResult> Index(string? arama)
    {
        ViewBag.Arama = arama;
        var liste = await _servis.ListeleAsync(arama);
        return View(liste);
    }

    // EKLE (formu göster)
    [HttpGet]
    public IActionResult Ekle()
    {
        return View("Form", new Kazanim());
    }

    // EKLE (formu kaydet)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Ekle(Kazanim kazanim)
    {
        if (!ModelState.IsValid) return View("Form", kazanim);

        var hata = await _servis.EkleAsync(kazanim);
        if (hata != null)
        {
            ModelState.AddModelError("", hata);
            return View("Form", kazanim);
        }

        TempData["Mesaj"] = "Kazanım eklendi.";
        return RedirectToAction(nameof(Index));
    }

    // DÜZENLE (formu göster)
    [HttpGet]
    public async Task<IActionResult> Duzenle(int id)
    {
        var kazanim = await _servis.GetirAsync(id);
        if (kazanim == null) return NotFound();
        return View("Form", kazanim);
    }

    // DÜZENLE (formu kaydet)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Duzenle(Kazanim kazanim)
    {
        if (!ModelState.IsValid) return View("Form", kazanim);

        var hata = await _servis.GuncelleAsync(kazanim);
        if (hata != null)
        {
            ModelState.AddModelError("", hata);
            return View("Form", kazanim);
        }

        TempData["Mesaj"] = "Kazanım güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    // SİL
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Sil(int id)
    {
        var hata = await _servis.SilAsync(id);
        TempData["Mesaj"] = hata ?? "Kazanım silindi.";
        return RedirectToAction(nameof(Index));
    }
}