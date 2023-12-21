using Microsoft.AspNetCore.Mvc;
using Proje_B201210567.Data;
using Proje_B201210567.Models;

namespace Proje_B201210567.Controllers
{
    public class RandevuController : Controller
    {

        private readonly AppDbContext _db;
        public RandevuController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult RandevuAra()
        {
            RandevuModelleri randevu = new RandevuModelleri()
            {
                poliklinikler = _db.Poliklinikler.ToList(),
            };

            return View(randevu);
        }

		[HttpGet]
		public JsonResult jsonData(int? BolumId)
		{
			if (BolumId.HasValue)
			{
				var DoktorListesi = (from d in _db.Doktorlar where d.poliklinikBolum_Id == BolumId select d).ToList();
                return Json(DoktorListesi);
			}
			return Json(null);
		}

        public IActionResult DoktorFiltere()
        {
            return View();
        }

	}
}
