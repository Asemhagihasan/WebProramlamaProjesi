using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje_B201210567.Data;
using Proje_B201210567.Models;

namespace Proje_B201210567.Controllers
{
    public class PoliklinikController : Controller
    {
        private readonly AppDbContext _db;
        public PoliklinikController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult PoliklinikGet()
        {
            IEnumerable<Poliklinik> PoliklinkListe = _db.Poliklinikler.ToList();

			return View(PoliklinkListe);
        }
        [HttpGet]
        public JsonResult jsonData(int ? BolumId)
        {
            if(BolumId.HasValue) 
            {
                var data = _db.Poliklinikler.Find(BolumId);
                // Return data in JSON format
                return Json(data);
            }
            return Json(null);
        }
    }
}
