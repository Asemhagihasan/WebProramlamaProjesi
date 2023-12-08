using Microsoft.AspNetCore.Mvc;
using Proje_B201210567.Data;
using Proje_B201210567.Models;

namespace Proje_B201210567.Controllers
{
    public class KullanciController : Controller
    {
		private readonly AppDbContext _db;
		public KullanciController(AppDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Create(Kullanci kullanci)
        {
            if(ModelState.IsValid)
            {
                _db.Add(kullanci);
                _db.SaveChanges();
				return RedirectToAction("Login", "Hastane");

			}
			return View(kullanci);
		}
	}
}
