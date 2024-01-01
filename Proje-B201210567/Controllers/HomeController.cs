using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Proje_B201210567.Data;
using Proje_B201210567.Models;
using System.Diagnostics;

namespace Proje_B201210567.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;
        
        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
            
        }

        public IActionResult Index()
		{
            
            //var Doktorlar = _db.Doktorlar.ToList();
            //var poliklinikLer = _db.Poliklinikler.ToList();

            //List<poliklinikVeDoktorlar> list = new List<poliklinikVeDoktorlar>();
            //foreach (var p in poliklinikLer)
            //{
            //    poliklinikVeDoktorlar model = new poliklinikVeDoktorlar();
            //    model.Poliklinik = p;

            //    // Create a copy of Doktorlar before iterating
            //    var doktorlarCopy = Doktorlar.ToList();

            //    foreach (var d in doktorlarCopy)
            //    {
            //        if (d.poliklinikBolum_Id == p.Bolum_Id)
            //        {
            //            model.doktors.Add(d);
            //            Doktorlar.Remove(d);
            //        }
            //    }

            //    list.Add(model);
            //}
			return View();
		}


		[Authorize(Roles = "admin")]
		public IActionResult AdminIndex()
		{
			return View();
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}