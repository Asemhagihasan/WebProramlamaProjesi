using Microsoft.AspNetCore.Mvc;
using Proje_B201210567.Data;
using Proje_B201210567.Models;

namespace Proje_B201210567.Controllers
{
	public class AdminController : Controller
	{
		private readonly AppDbContext _db;
		public AdminController(AppDbContext db)
		{
			_db = db;
		}
		public IActionResult Kullancilar()
		{
			IEnumerable<Kullanci> objCategortList = _db.Kullancilar.ToList();
			return View(objCategortList);
		}
		public IActionResult KullanciEkleme()
		{
			return View();
		}
		public IActionResult KullanciSilme()
		{
			return View();
		}
		public IActionResult Randevuler()
		{
			return View();
		}
		public IActionResult RendevuAlmak()
		{
			return View();
		}
		public IActionResult RandevuSilmek()
		{
			return View();
		}
		public IActionResult RandevuDegistir()
		{
			return View();
		}
		public IActionResult DoktorEklemk()
		{
			return View();
		}
		public IActionResult DoktorSilmek()
		{
			return View();
		}
		public IActionResult PoliklinikOlusturma()
		{
			return View();
		}
	}
}
