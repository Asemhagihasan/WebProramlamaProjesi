using Microsoft.AspNetCore.Mvc;
using Proje_B201210567.Data;
using Proje_B201210567.Models;
using System;

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

        private List<CalismaSaatlarGroup> CalismaKontrol(List<Doktor> doktors, DateTime BitisTarihi, DateTime BaslangisTarihi)
        {

			var x = BitisTarihi < BaslangisTarihi; // dont get any thing
			var y = BitisTarihi == BaslangisTarihi; // get specific day
			var z = BitisTarihi.DayOfYear - BaslangisTarihi.DayOfYear;// if negatif => between two years else if more than 6 get all days else get specific days


			var calismaSaatlarGroups = new List<CalismaSaatlarGroup>();
			foreach (var d in doktors)
            {
                var calismaSaatler = _db.CalismaSaati.Where(a => a.DoktorId == d.DoktorId );
				var calismaSaatlarGroup = new CalismaSaatlarGroup();
				var poliklinik = _db.Poliklinikler.Find(d.poliklinikBolum_Id);


                if (BitisTarihi == BaslangisTarihi) 
                {
					calismaSaatlarGroup.DoctorId = d.DoktorId;
					calismaSaatlarGroup.DoctorName = d.Doktor_Adi;
					calismaSaatlarGroup.DoktorSoyad = d.Doktor_Soyad;
					calismaSaatlarGroup.BolumAdi = poliklinik.Bolum_Adi;
					calismaSaatlarGroup.BaslangisTarih = BaslangisTarihi;
					calismaSaatlarGroup.BitisTarih = BitisTarihi;

                    foreach (var calismaSaat in calismaSaatler)
					{

						if(calismaSaat.Gun == (BaslangisTarihi.DayOfWeek))
						{
                            calismaSaatlarGroup.CalismaSaatiListesi.Add(calismaSaat);
						}
					}
					calismaSaatlarGroups.Add(calismaSaatlarGroup);
				}
                
               else if (Math.Abs(BitisTarihi.DayOfYear - BaslangisTarihi.DayOfYear) > 6)
				{
                    calismaSaatlarGroup.DoctorId = d.DoktorId;
                    calismaSaatlarGroup.DoctorName = d.Doktor_Adi;
                    calismaSaatlarGroup.DoktorSoyad = d.Doktor_Soyad;
                    calismaSaatlarGroup.BolumAdi = poliklinik.Bolum_Adi;
                    foreach (var calismaSaat in calismaSaatler)
                    {                   
                            calismaSaatlarGroup.CalismaSaatiListesi.Add(calismaSaat);  
                    }
					if(calismaSaatlarGroup.CalismaSaatiListesi.Count != 0)
					{
                        calismaSaatlarGroups.Add(calismaSaatlarGroup);
                    }

                }

				else if(Math.Abs(BitisTarihi.DayOfYear - BaslangisTarihi.DayOfYear) < 6) 
				{
                    calismaSaatlarGroup.DoctorId = d.DoktorId;
                    foreach (var calismaSaat in calismaSaatler)
					{
						//if(calismaSaat.Gun)
						//{

						//}
					}
                }
			}
			return calismaSaatlarGroups;


			//List<List<CalismaSaati>> CalismaTablosu = new List<CalismaSaati>();
   //         foreach (var d in doktors)
   //         {
   //            var claismaList = (from  cla in _db.CalismaSaati where cla.DoktorId == d.DoktorId select cla).ToList();
   //             foreach (var doc in claismaList)
   //             {
   //                 int dayOfWeek =(int)doc.Gun;

			//		if (!(dayOfWeek <= BitisTarihi.Day && dayOfWeek >= BaslangisTarihi.Day))
   //                 {
			//			claismaList.Remove(doc);
			//			//var Doktor = _db.Doktorlar.Find(doc.DoktorId);
			//			//                  doktorlar.Add(Doktor);
			//		}
			//	}
   //             CalismaTablosu.Add(claismaList);
			//}
   //         return doktorlar;
        }

		[HttpPost]
        public IActionResult DoktorFiltere(RandevuModelleri randevu)
        {

            if (randevu == null)
            {
                return NotFound();
            }

			var doktor = _db.Doktorlar.Find(randevu.Doktor.DoktorId);
			var poliklinik = _db.Poliklinikler.Find(randevu.Poliklinik.Bolum_Id);
			var kullanci = _db.Kullancilar.SingleOrDefault(k => k.Tc == randevu.Kullanci.Tc);
			var ActiveCalisma = new List<CalismaSaatlarGroup>();


            string hata = "";

			if (kullanci == null)
			{
				hata = "Hasta bulunamadı";
				return RedirectToAction("RandevuAra", hata);
			}

			//kullanci doktor secmezise otomatik olark sectigi poliklinike ait tum doktorlara getirir
			if (randevu.Doktor.DoktorId == 0)
			{
				var doktorlar = (from d in _db.Doktorlar where d.poliklinikBolum_Id == randevu.Poliklinik.Bolum_Id select d).ToList();
			    ActiveCalisma = CalismaKontrol(doktorlar, randevu.BitisTarihi, randevu.BaslangicTarihi);

                if(ActiveCalisma.Count == 0)
                {
                    hata = "";
                    return RedirectToAction("",hata);
                }
				return View(ActiveCalisma);
			}

			if (poliklinik == null || doktor == null)
			{
				return RedirectToAction("RandevuAra", hata);

			}

			ActiveCalisma =  CalismaKontrol(new List<Doktor> { doktor} ,randevu.BitisTarihi,randevu.BaslangicTarihi);

            if(ActiveCalisma.Count == 0)
            {
                return RedirectToAction("",hata);
            }

			return View(ActiveCalisma);
        }


		public IActionResult CalismaSaatiGoster(int ?id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var calisma = _db.CalismaSaati.Find(id);

			if (calisma == null)
			{ 
				return NotFound(); 
			}

			return View(calisma); 
		}
	}
}
