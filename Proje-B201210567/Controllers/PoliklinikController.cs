using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje_B201210567.Data;
using Proje_B201210567.Models;

namespace Proje_B201210567.Controllers
{
	[Authorize(Roles = "admin")]
	public class PoliklinikController : Controller
    {
        private readonly AppDbContext _db;
        public PoliklinikController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult PoliklinikGet()
        {
            IEnumerable<Poliklinik> PoliklinkListe = _db.Poliklinikler.ToList();

			return View(PoliklinkListe);
        }
        [HttpGet]
        public JsonResult jsonData(int ? BolumId)
        {
            if(BolumId.HasValue) 
            {
                var data = _db.Poliklinikler.Find(BolumId);
                // Return data in JSON format
                return Json(data);
            }
            return Json(null);
        }

        public IActionResult PoliklinikEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PoliklinikEkle(Poliklinik model)
        { 
            if(model.DoktorList == null)
            {
                model.DoktorList = new List<Doktor>() { };
            }

            if(model.DoktorList != null && model.Bolum_Id != null)
            {
                _db.Poliklinikler.Add(model);
                _db.SaveChanges();
                return RedirectToAction("PoliklinikGet");
            }
            return View(model); 
        }

        public IActionResult poliklinikSil(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var model = _db.Poliklinikler.FirstOrDefault(pol => pol.Bolum_Id == id);

            if(model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult poliklinikSilPost(int ?id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }

            var pol = _db.Poliklinikler.FirstOrDefault(p => p.Bolum_Id == id);

            if(pol == null)
            {
                return NotFound();
            }

            var DoktorList = _db.Doktorlar.Where(d => d.poliklinikBolum_Id == id).ToList();
            DoktorList.Clear();

            _db.Poliklinikler.Remove(pol);
            _db.SaveChanges();

            return RedirectToAction("PoliklinikGet");
        }
    }
}
