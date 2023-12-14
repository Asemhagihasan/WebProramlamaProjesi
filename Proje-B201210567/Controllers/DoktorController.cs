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

			if (Doktorlar == null)
			{
				return NotFound();

			}

			DoktorList_Id doktorList_Id = new DoktorList_Id()
			{
				doktorlar = Doktorlar,
				Id = id
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
			var id = model.Doktor.poliklinikBolum_Id;

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
		public IActionResult DoktorSilmePost(int ?id)
		{

			return RedirectToAction("");
		}
	}
}
