using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Proje_B201210567.Data;
using Proje_B201210567.Models;
using Proje_B201210567.Repository;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace Proje_B201210567.Controllers
{
	[Authorize(Roles = "admin")]
	public class KullanciController : Controller
    {
        private readonly UserManager<Kullanci> _userManager;
        private readonly SignInManager<Kullanci> _signInManager;

        private readonly AppDbContext _db;
        public KullanciController(AppDbContext db, UserManager<Kullanci> userManager, SignInManager<Kullanci> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> KullancilarAsync(Kullanci model)
        {
            List<Kullanci> objCategortList;

            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            var users = await _userManager.GetUsersInRoleAsync("user");
            if (model.Tc != null)
            {
                objCategortList = new List<Kullanci> { model }; 
                return View(objCategortList);
            }
            else
            {
                objCategortList = users.ToList();
            }

			return View(objCategortList);
        }

		public IActionResult KullanciEkleme()
        {
            return View();
        }
        public IActionResult KullanciGuncel(string ?id)
        {
            if(id == null)
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
            if (ModelState.IsValid)
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
