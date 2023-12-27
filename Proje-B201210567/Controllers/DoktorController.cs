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
			return RedirectToAction("DoktorListesi", "Doktor",new { id = model.Doktor.poliklinikBolum_Id});

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
			List<TimeSpan> randevuSaatlari = new List<TimeSpan>();
            List<TimeSpan> RandevuSaatlari1 = new List<TimeSpan>();
            bool[] bools = new bool[doktor.CalismaSaatleri[0].BitisSaati.Hours - doktor.CalismaSaatleri[0].BaslangicSaati.Hours];
			int count = 0;

            for (int i =0; i < doktor.CalismaSaatleri[0].BitisSaati.Hours - doktor.CalismaSaatleri[0].BaslangicSaati.Hours; i++)
			{
                int currentHour = doktor.CalismaSaatleri[0].BaslangicSaati.Hours + i;

                if (currentHour == 12)
                {
                    continue;
                }

				for(int j = 1; j < 3; j++)
				{
					TimeSpan RandevuSaat = new TimeSpan(currentHour, j*20, 0);
                    bools[i + count] = true;
					RandevuSaatlari1.Add(RandevuSaat);
					count++;
                }

                TimeSpan appointmentTime = new TimeSpan(currentHour, 0, 0);

                randevuSaatlari.Add(appointmentTime);
            }
			var gun = (int)(doktor.CalismaSaatleri[0].Gun);

            CalismaSaati calismaSaati = new CalismaSaati()
			{
				Gun = doktor.CalismaSaatleri[0].Gun,
				BaslangicSaati = doktor.CalismaSaatleri[0].BaslangicSaati,
				BitisSaati = doktor.CalismaSaatleri[0].BitisSaati,
				DoktorId = doktor.DoktorId,
				DayOfWeeks = randevuSaatlari,
				RandevuSaatlari = RandevuSaatlari1,
				Tarih = DateTime.Now.ToShortDateString(),
				IsAvailable = bools,
			};

            if (doktor == null)
			{
				return NotFound();
			}

            doktor.CalismaSaatleri.Clear();
            _db.Doktorlar.Update(doktor);
            _db.CalismaSaati.Add(calismaSaati);
            _db.SaveChanges();
			return RedirectToAction("DoktorListesi" , "Doktor",new { id = doktor.poliklinikBolum_Id });
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
