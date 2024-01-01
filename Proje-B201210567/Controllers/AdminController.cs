using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proje_B201210567.Data;
using Proje_B201210567.Models;
using Proje_B201210567.Models.Authentication;
using Proje_B201210567.Repository;
using System.Text;

namespace Proje_B201210567.Controllers
{
	[Authorize(Roles = "admin")]
	public class AdminController : Controller
	{
        private readonly AppDbContext _db;
        private readonly UserManager<Kullanci> _userManager;
        public AdminController(AppDbContext db, UserManager<Kullanci> userManager, SignInManager<Kullanci> signInManager, IUserAuthenticationService authService)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
		{
			List <Kullanci> adminler = new List<Kullanci>();
			HttpClient client = new HttpClient();
			var response = await client.GetAsync("https://localhost:7100/api/AdminApi");
			var jsonResponse = await response.Content.ReadAsStringAsync();
            adminler = JsonConvert.DeserializeObject<List<Kullanci>>(jsonResponse);
			return View(adminler);
		}
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(RegistrationModel admin)
		{
            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            if (adminUsers.Any(k => k.Tc == admin.TC))
			{
				ModelState.AddModelError("Tc", "Bu Tc Zaten Mevcut");
				return View(admin);
			}
			if (adminUsers.Any(c => c.Email == admin.Email))
			{
				ModelState.AddModelError("Email", "Bu Mail Adresi zaten Mevcut");
				return View(admin);
			}

			HttpClient client = new HttpClient();
			var Json = JsonConvert.SerializeObject(admin);
			var content = new StringContent(Json, Encoding.UTF8, "application/json");
			var response = await client.PostAsync("https://localhost:7100/api/AdminApi", content);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Edit(string? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
			var admin = adminUsers.FirstOrDefault(k => k.Id == id);

			if(admin == null)
			{
				return NotFound();
			}

			return View(admin);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(Kullanci p)
		{
			if (ModelState.IsValid)
			{

				HttpClient client = new HttpClient();
				var Json = JsonConvert.SerializeObject(p);
				var content = new StringContent(Json, Encoding.UTF8, "application/json");
				var response = await client.PutAsync($"https://localhost:7100/api/AdminApi/{p.Id}", content);
				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
			}
			// If ModelState is not valid or the API request fails, return to the view with the original model
			return View(p);
		}


		public async Task<IActionResult> DeleteAsync(string? id)
		{

			if (id == null)
			{
				return NotFound();
			}

			var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
			var deleted = adminUsers.SingleOrDefault(k => k.Id == id);

			if (deleted == null)
			{
				return NotFound();
			}

			return View(deleted);
		}
		[HttpPost]
		public async Task<IActionResult> Deleted(Kullanci admin)
		{

			if (admin == null)
			{
				return NotFound();
			}
			HttpClient client = new HttpClient();
			var Json = JsonConvert.SerializeObject(admin);
			var response = await client.DeleteAsync($"https://localhost:7100/api/AdminApi/{admin.Id}");
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}

	}
}
