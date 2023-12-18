using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje_B201210567.Data;
using Proje_B201210567.Models;

namespace Proje_B201210567.Controllers
{
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
        public IActionResult PoliklinikEklepost(Poliklinik pol)
        {
            if (pol.DoktorList == null) 
            {
                pol.DoktorList = pol.DoktorList ?? new List<Doktor>();
            }
            if (pol.Bolum_Id!=null && pol.DoktorList!=null) 

            {
                _db.Add(pol);
                _db.SaveChanges();
                return RedirectToAction("PoliklinikGet");
            }

            else
            {
                return View(pol);
            }

           

        }

       






        public IActionResult PoliklinikSilme(int ? id) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                var Deleted = _db.Poliklinikler.SingleOrDefault(x => x.Bolum_Id == id);
                _db.Remove(Deleted);
                _db.SaveChanges();
                return RedirectToAction("PoliklinikGet");
            }


        }
        
        [HttpPost]
        public IActionResult PoliklinikSilPost(int? id) 
        {
            return View();

        }
    }
}
