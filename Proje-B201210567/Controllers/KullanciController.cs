using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Kullancilar(Kullanci model)
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
        public async Task<IActionResult> KullanciGuncel(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var users = await _userManager.GetUsersInRoleAsync("user");

            var kullanci = users.SingleOrDefault(k => k.Id == id);

            if (kullanci == null)
            {
                return NotFound();
            }

            return View(kullanci);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KullanciGuncel(Kullanci kullanci)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.UpdateAsync(kullanci);
                if (result.Succeeded)
                {
                    return RedirectToAction("Kullancilar");
                }
                else
                {
                    // Handle errors, perhaps by returning to the view with error messages
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("ModelState is not valid", error.Description);
                    }
                }
            }

            // ModelState is not valid, return to the view with the current model
            return View(kullanci);
        }

        public async Task<IActionResult> KullanciSilme(string?id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var users = await _userManager.GetUsersInRoleAsync("user");

            var kullanci =users.FirstOrDefault(k => k.Id == id);
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
		public async Task<IActionResult> HastaSil(Kullanci kullanci1)
        {
            var users = await _userManager.GetUsersInRoleAsync("user");

            var kullanci = users.FirstOrDefault(k => k.Id == kullanci1.Id);
            var CvP = _db.calismaVePolikliniks.SingleOrDefault(t => t.KullanciId == kullanci1.Id);
            var randevu = _db.Rendevuler.Find(kullanci1.Id);
            if(randevu != null)
            {
                _db.Rendevuler.Remove(randevu);
                _db.SaveChanges();
            }
            if (kullanci == null || CvP == null)
            {
                return NotFound();
            }
            _db.calismaVePolikliniks.Remove(CvP);
            _db.SaveChanges();
            var result = await _userManager.DeleteAsync(kullanci);

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
