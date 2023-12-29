using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Proje_B201210567.Data;
using Proje_B201210567.Models;

namespace Proje_B201210567.Controllers
{
    public class CalismaSaatiController : Controller
    { 
         private readonly AppDbContext _db;
        private IHtmlLocalizer<CalismaSaatiController> _localizer;
        public CalismaSaatiController(AppDbContext db ,IHtmlLocalizer<CalismaSaatiController> localizer)
        {
            _localizer = localizer;
            _db = db;
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
                var Doktor1 = _db.Doktorlar.Find(calisma.DoktorId);
                _db.CalismaSaati.Update(calisma);
                _db.SaveChanges();
                return RedirectToAction("DoktorListesi", "Doktor", new {  id = Doktor1.poliklinikBolum_Id });
				
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
                var Doktor2 = _db.Doktorlar.Find(calisma.DoktorId);
                _db.CalismaSaati.Remove(calisma);
                _db.SaveChanges();
                return RedirectToAction("DoktorListesi", "Doktor" , new { id = Doktor2.poliklinikBolum_Id });

            }
            return View(calisma);
        }
    }
}
