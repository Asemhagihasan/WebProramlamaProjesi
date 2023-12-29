using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Proje_B201210567.Data;
using Proje_B201210567.Models;
using System.Collections;
using System.Collections.Generic;

namespace Proje_B201210567.Controllers
{
    public class KullanciController : Controller
    {
		private readonly AppDbContext _db;
        private readonly IHtmlLocalizer<KullanciController> _localizer;
        public KullanciController(AppDbContext db , IHtmlLocalizer<KullanciController> localizer)
		{
            _localizer = localizer;
			_db = db;
		}

        public IActionResult Kullancilar(Kullanci model)
        {
            @ViewData["Patients List"] = _localizer["Patients List"];
            @ViewData["Add Patient"] = _localizer["Add Patient"];
            @ViewData["Search"] = _localizer["Search"];
            @ViewData["Update"] = _localizer["Update"];
            @ViewData["Delete"] = _localizer["Delete"];
            @ViewData["Id Number"] = _localizer["Id Number"];
            @ViewData["Name"] = _localizer["Name"];
            @ViewData["Surname"] = _localizer["Surname"];
            @ViewData["Number"] = _localizer["Number"];
            @ViewData["Gender"] = _localizer["Gender"];





            List<Kullanci> objCategortList;

            if (model.KullaniciId != 0)
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
			@ViewData["Create Patient"] = _localizer["Create Patient"];
			@ViewData["Id Number"] = _localizer["Id Number"];
			return View();
        }
        public IActionResult setLanguage(string Culture, string returnUrl)
        {

            Response.Cookies.Append
                (
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(Culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }

                );
            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult KullanciEkleme(Kullanci kullanci)
        {
            

            if (_db.Kullancilar.Any(k => k.Tc == kullanci.Tc ))

			{
				ModelState.AddModelError("Tc", "Bu Tc Zaten Mevcut");
				return View(kullanci);
			}
            if(_db.Kullancilar.Any(c=>c.Email == kullanci.Email))
            {
				ModelState.AddModelError("Email", "Bu Mail Adresi zaten Mevcut");
				return View(kullanci);

			}
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
                return RedirectToAction("Kullancilar");
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
		public IActionResult HastaSil(int? KullaniciId)
        {
           var kullanci = _db.Kullancilar.Find(KullaniciId);
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

            var Hasta = _db.Kullancilar.SingleOrDefault(x => x.Tc == Tc);

            if (Hasta == null)
            {
                return NotFound();
            }

            return RedirectToAction("Kullancilar", Hasta);
        }
    }
}
