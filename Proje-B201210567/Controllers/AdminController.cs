using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proje_B201210567.Data;
using Proje_B201210567.Models;
using System.Text;

namespace Proje_B201210567.Controllers
{
	public class AdminController : Controller
	{
		private readonly AppDbContext _db;
		public AdminController(AppDbContext db)
		{
			_db = db;
		}
       
        public async Task<IActionResult> Index()
        {
			List<Admin> yazarlar = new List<Admin>();
			HttpClient client = new HttpClient();
			var response = await client.GetAsync("https://localhost:7100/api/AdminApi");
			var jsonResponse = await response.Content.ReadAsStringAsync();
			yazarlar = JsonConvert.DeserializeObject<List<Admin>>(jsonResponse);


			//IEnumerable<Admin> list = yazarlar.ToList();
			return View(yazarlar);
           
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Admin admin)
        {
            // Local validation logic
            if (_db.Admins.Any(k => k.Tc == admin.Tc))
            {
                ModelState.AddModelError("Tc", "Bu Tc Zaten Mevcut");
                return View(admin);
            }
            if (_db.Admins.Any(c => c.Email == admin.Email))
            {
                ModelState.AddModelError("Email", "Bu Mail Adresi zaten Mevcut");
                return View(admin);
            }

			HttpClient client = new HttpClient();
			var Json = JsonConvert.SerializeObject(admin);
			var content = new StringContent(Json, Encoding.UTF8, "application/json");
			var response = await client.PostAsync("https://localhost:7100/api/AdminApi",content);
			if(response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");


			// API request logic


			// If the API request fails or ModelState is not valid, return to the view with the original model

		}

		public IActionResult Edit(int? id)
		{
			if (id == 0 || id == null)
			{
				return NotFound();
			}
			var admin = _db.Admins.Find(id);
			if (admin == null)
			{
				return NotFound();
			}
			return View(admin);
		}
		[HttpPost]
        public async Task<IActionResult> Edit(Admin p)
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

        public IActionResult Delete(int? id)
		{

			if (id == null || id == 0)
			{
				return NotFound();
			}

			var deleted = _db.Admins.Find(id);

			if (deleted == null)
			{
				return NotFound();
			}

			return View(deleted);
		}
		[HttpPost]
        public async Task<IActionResult> Deleted(Admin kullanci)
        {

			
			if (kullanci == null)
			{
				return NotFound();
			}
			HttpClient client = new HttpClient();
			var Json = JsonConvert.SerializeObject(kullanci);
			var response = await client.DeleteAsync($"https://localhost:7100/api/AdminApi/{kullanci.Id}");
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}

      



    }
}
