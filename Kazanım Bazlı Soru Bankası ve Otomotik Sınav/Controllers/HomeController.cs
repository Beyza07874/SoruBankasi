using Kazanım_Bazlı_Soru_Bankası_ve_Otomotik_Sınav.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kazanım_Bazlı_Soru_Bankası_ve_Otomotik_Sınav.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
