using Microsoft.AspNetCore.Mvc;
using Proje_B201210567.Data;
using Proje_B201210567.Models;

namespace Proje_B201210567.Controllers
{
	public class DoktorController : Controller
	{
		int BolumId;
        private readonly AppDbContext _db;
        public DoktorController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult DoktorListesi(int id)
		{
			if(id == 0 || id == null) { return NotFound(); }

			var Doktorlar = (from d in _db.Doktorlar where d.poliklinikBolum_Id == id select d).ToList();

			var calismaSaati = (from c in _db.CalismaSaati where c.DoktorId == id select c).ToList();


            if (Doktorlar == null)
			{
				return NotFound();

			}

			DoktorList_Id doktorList_Id = new DoktorList_Id()
			{
				doktorlar = Doktorlar,
				Id = id,
				calismasaati = calismaSaati,
			};

			return View(doktorList_Id);
		}

		public IActionResult DoktorEkle()
		{
			PoliklinikVeDoktor model = new PoliklinikVeDoktor();

			model.polikliniks = _db.Poliklinikler.ToList();
			
			return View (model);
		}

		[HttpPost]
		public IActionResult DoktorEkle(PoliklinikVeDoktor model)
		{
			if (model.Doktor == null || model.Doktor.poliklinikBolum_Id == null)
			{
				return NotFound();
			}

			var poliklinik = _db.Poliklinikler.FirstOrDefault(p => p.Bolum_Id == model.Doktor.poliklinikBolum_Id);

			if (poliklinik == null)
			{
				ModelState.AddModelError("Doktor.poliklinikBolum_Id", "Secteniz Bolum bulunmiyor.");
				model.polikliniks = _db.Poliklinikler.ToList();
				return View(model);
			}

			if(model.Doktor.CalismaSaatleri == null)
			{
				model.Doktor.CalismaSaatleri = new List<CalismaSaati> { };
			}
			//var id = model.Doktor.poliklinikBolum_Id;

			_db.Doktorlar.Add(model.Doktor);
			_db.SaveChanges();
			return RedirectToAction("PoliklinikGet","Poliklinik");

		}

		public IActionResult DoktorGuncel(int ?id)
		{
			if (id == null || id ==0)
			{
				return NotFound();
			}

            PoliklinikVeDoktor doktor = new PoliklinikVeDoktor()
            {
                polikliniks = _db.Poliklinikler.ToList(),
                Doktor = _db.Doktorlar.Find(id),
            };

			if(doktor == null)
			{
				return NotFound();
			}

            return View(doktor);
		}

		[HttpPost]
		public IActionResult DoktorGuncel(Doktor doktor)
		{
			CalismaSaati calismaSaati = new CalismaSaati()
			{
				Gun = doktor.CalismaSaatleri[0].Gun,
				BaslangicSaati = doktor.CalismaSaatleri[0].BaslangicSaati,
                BitisSaati = doktor.CalismaSaatleri[0].BitisSaati,
				DoktorId = doktor.DoktorId
            };
            _db.CalismaSaati.Add(calismaSaati);
            doktor.CalismaSaatleri.Clear();
			if (doktor == null)
			{
				return NotFound();
			}
			_db.Doktorlar.Update(doktor);
			_db.SaveChanges();
			return RedirectToAction("PoliklinikGet","Poliklinik");

		}

		public IActionResult DoktorSilme(int ?id)
		{
			if(id == null || id ==0)
			{
				return NotFound();
			}

			var model = _db.Doktorlar.SingleOrDefault(d => d.DoktorId == id);

			if(model == null)
			{
				return NotFound();
			}

			return View(model);
		}

		[HttpPost]
		public IActionResult DoktorSilmePost(int ?DoktorId)
		{
			if(DoktorId == null || DoktorId == 0)
			{
				return NotFound();
			}

			var model = _db.Doktorlar.SingleOrDefault(d => d.DoktorId == DoktorId);

			if(model == null)
			{
				return NotFound();
			}

			if(model.CalismaSaatleri == null)
			{
				model.CalismaSaatleri = new List<CalismaSaati> { };
            }


            _db.Doktorlar.Remove(model);
            _db.SaveChanges();
            return RedirectToAction("PoliklinikGet", "Poliklinik"); ;
        }

		[HttpGet]
		public JsonResult jsonData(int ? DoktorId)
		{
			if(DoktorId.HasValue)
			{
				var calismaListe = (from c in _db.CalismaSaati where c.DoktorId == DoktorId select c).ToList();
				if(calismaListe == null)
				{
					return Json(null);
				}
				return Json(calismaListe);
			}
			return Json(DoktorId);
		}
    }
}
