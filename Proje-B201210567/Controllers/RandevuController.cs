using Microsoft.AspNetCore.Mvc;
using Proje_B201210567.Data;
using Proje_B201210567.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

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

        private List<CalismaSaatlarGroup> CalismaKontrol(List<Doktor> doktors, DateTime BitisTarihi, DateTime BaslangisTarihi,int KullanciId)
        {
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
					calismaSaatlarGroup.KullanciId = KullanciId;

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
                    calismaSaatlarGroup.BaslangisTarih = BaslangisTarihi;
                    calismaSaatlarGroup.BitisTarih = BitisTarihi;
                    calismaSaatlarGroup.KullanciId = KullanciId;
                    foreach (var calismaSaat in calismaSaatler)
                    {                   
                            calismaSaatlarGroup.CalismaSaatiListesi.Add(calismaSaat);  
                    }
					if(calismaSaatlarGroup.CalismaSaatiListesi.Count != 0)
					{
                        calismaSaatlarGroups.Add(calismaSaatlarGroup);
                    }

                }
			}
			return calismaSaatlarGroups;
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


            string HataMsj = "";

			if (kullanci == null)
			{
                HataMsj = "Hasta bulunamamıştır";

                return RedirectToAction("Hata",new {Msj = HataMsj });
			}
            //kullanci doktor secmezise otomatik olark sectigi poliklinike ait tum doktorlara getirir
            if (randevu.Doktor.DoktorId == 0)
			{
				var doktorlar = (from d in _db.Doktorlar where d.poliklinikBolum_Id == randevu.Poliklinik.Bolum_Id select d).ToList();
			    ActiveCalisma = CalismaKontrol(doktorlar, randevu.BitisTarihi, randevu.BaslangicTarihi,kullanci.KullaniciId);

                if(ActiveCalisma.Count == 0)
                {
                    HataMsj = "Aradığınız Formata alınabilir uygun randevu bulunamamıştır.";
                    return RedirectToAction("Hata", new { Msj = HataMsj });
                }
				return View(ActiveCalisma);
			}

			if (poliklinik == null || doktor == null)
			{
				return NotFound();

			}

			ActiveCalisma =  CalismaKontrol(new List<Doktor> { doktor} ,randevu.BitisTarihi,randevu.BaslangicTarihi, kullanci.KullaniciId);

            if(ActiveCalisma.Count == 0)
            {
                HataMsj = "Aradığınız Formata alınabilir uygun randevu bulunamamıştır.";
                return RedirectToAction("Hata", new { Msj = HataMsj });
            }

			return View(ActiveCalisma);
        }

		public IActionResult Hata(string Msj)
		{
            return View((object)Msj);
        }

        [HttpGet]
        public IActionResult CalismaSaatiGoster(int ?id1,int ?id2)
		{
			if (id1 == null || id1 == 0 || id2 == null || id2 == 0)
			{
				return NotFound();
			}

			var calisma = _db.CalismaSaati.Find(id1);
			var doktor = _db.Doktorlar.Find(calisma.DoktorId);
			var poliklinikId = doktor.poliklinikBolum_Id;
			var kullanci = _db.Kullancilar.Find(id2);

            var poliklinik = _db.Poliklinikler.Find(poliklinikId);

			if (calisma == null || poliklinik == null)
			{ 
				return NotFound(); 
			}

			CalismaVePoliklinik model = new CalismaVePoliklinik()
			{
                CalismaSaati = calisma,
				Poliklinik = poliklinik,
				Doktor = doktor,
				Kullanci = kullanci,
            };
            _db.calismaVePolikliniks.RemoveRange(_db.calismaVePolikliniks);
            _db.calismaVePolikliniks.Add(model);
			_db.SaveChanges();

            return View(model); 
		}


		[HttpPost]
		public IActionResult RandevuOnay(int ?RandevId)
		{
			if(RandevId == 0 || RandevId == null)
			{
				return NotFound();
			}

			var randevu = _db.calismaVePolikliniks.Find(RandevId);

			if(randevu == null)
			{
				return NotFound();
			}

			Randevu randevuObj = new Randevu() 
			{
                DoktorId = randevu.DoktorId,
                KullaniciId =randevu.KullanciId,
                RandevuOlasanTarih = DateTime.Now.ToShortDateString(),
				Durum = "Beklemede",
				BolumId = randevu.poliklinikId,
            }; 
			
			_db.Rendevuler.Add(randevuObj);
			_db.SaveChanges();
			return RedirectToAction("RandevuListesi");
		}

		public IActionResult RandevuListesi()
		{
			var Randevular = _db.Rendevuler.ToList();
			var Randevuler1 = new List<RandevuYazdir>();


            foreach (var item in Randevular)
			{
				var kullanci = _db.Kullancilar.Find(item.KullaniciId);
				var Doktor = _db.Doktorlar.Find(item.DoktorId);
				var Bolum = _db.Poliklinikler.Find(item.BolumId);

                var Randevu = new RandevuYazdir()
				{
					KullanciAdi = kullanci.Kullanci_Adi + " " +  kullanci.Kullanci_Soyad,
					Tc = kullanci.Tc,
					KullanciId = kullanci.KullaniciId,
					DoktorAdi = Doktor.Doktor_Adi +" " +  Doktor.Doktor_Soyad,
					DokrorId = Doktor.DoktorId,
					BolumAdi = Bolum.Bolum_Adi,
					BolumId = Bolum.Bolum_Id,
                };
                Randevuler1.Add(Randevu);
            }
			
			return View(Randevuler1);
		}
    }
}
