using Microsoft.AspNetCore.Mvc;
using Proje_B201210567.Data;
using Proje_B201210567.Models;

namespace Proje_B201210567.Controllers
{
    public class CalismaSaatiController : Controller
    { 
         private readonly AppDbContext _db;
        public CalismaSaatiController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult CalismaGuncel(int ? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
          var calismalar =  _db.CalismaSaati.Find(id);
            if (calismalar == null) 
            {
                return NotFound();
            }
            return View(calismalar);
        }
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CalismaGuncel(CalismaSaati calisma) 
        {
            if (ModelState.IsValid)
            {
                _db.CalismaSaati.Update(calisma);
                _db.SaveChanges();
                return RedirectToAction("DoktorListesi", "Doktor", new {  id = calisma.DoktorId});
				
			}
            return View(calisma);
        }
        public IActionResult CalismaSil(int ? id) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var calismalar = _db.CalismaSaati.Find(id);
            if (calismalar == null)
            {
                return NotFound();
            }
            return View(calismalar);
        }
        [HttpPost]
        public IActionResult CalismaSil (CalismaSaati calisma)
        {
            if (ModelState.IsValid)
            {
                _db.CalismaSaati.Remove(calisma);
                _db.SaveChanges();
                return RedirectToAction("DoktorListesi", "Doktor");

            }
            return View(calisma);
        }
    }
}
