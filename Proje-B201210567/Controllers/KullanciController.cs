using Microsoft.AspNetCore.Mvc;
using Proje_B201210567.Data;
using Proje_B201210567.Models;
using System.Collections;
using System.Collections.Generic;

namespace Proje_B201210567.Controllers
{
    public class KullanciController : Controller
    {
		private readonly AppDbContext _db;
		public KullanciController(AppDbContext db)
		{
			_db = db;
		}

        public IActionResult Kullancilar(Kullanci model)
        {
            List<Kullanci> objCategortList;

            if (model != null)
            {
                objCategortList = new List<Kullanci> { model };
            }
            else
            {
                objCategortList = _db.Kullancilar.ToList();
            }

            return View(objCategortList);
        }
        public IActionResult KullanciEkleme()
        {
            return View();
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult KullanciEkleme(Kullanci kullanci)
        {
            if (ModelState.IsValid)
            {
                _db.Add(kullanci);
                _db.SaveChanges();
                return RedirectToAction("Kullancilar","Kullanci");

            }
            return View(kullanci);
        }

        public IActionResult KullanciGuncel(int ?id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            var kullanci = _db.Kullancilar.Find(id);
            if(kullanci == null)
            {
                return NotFound();
            }
            return View(kullanci);
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult KullanciGuncel(Kullanci kullanci)
        {
            if(ModelState.IsValid)
            {
                _db.Kullancilar.Update(kullanci);
                _db.SaveChanges();
                return RedirectToAction("Kullancilar", "Kullanci");
            }
            return View(kullanci);
        }

        public IActionResult KullanciSilme(int?id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

             var kullanci = _db.Kullancilar.Find(id);
            //var queri = from c in _db.Categories where c.Id == id select c;
            //var CategoryFromDbFirst = _db.Categories.FirstOrDefault(c => c.Id == id);

            if (kullanci == null)
            {
                return NotFound();
            }

            return View(kullanci);
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult HastaSil(int?id)
        {
           var kullanci = _db.Kullancilar.Find(id);
          if(kullanci == null)
            {
                return NotFound();
            }
            _db.Remove(kullanci);
            _db.SaveChanges();
            return RedirectToAction("Kullancilar","Kullanci");
        }
        [HttpPost]
        public IActionResult HastaAra(string Tc)
        {
            if (string.IsNullOrEmpty(Tc))
            {
                return NotFound();
            }

            var Hasta = _db.Kullancilar.Where(x => x.Tc == Tc).ToList();

            if (!Hasta.Any())
            {
                return NotFound();
            }

            return RedirectToAction("Kullancilar", Hasta);
        }
    }
}
