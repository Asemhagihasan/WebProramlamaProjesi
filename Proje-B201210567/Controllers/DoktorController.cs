using Microsoft.AspNetCore.Mvc;
using Proje_B201210567.Data;

namespace Proje_B201210567.Controllers
{
	public class DoktorController : Controller
	{
        private readonly AppDbContext _db;
        public DoktorController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult DoktorListesi(int ?id)
		{
			if(id == 0 && id == null) { return NotFound(); }
			
			//var doktorlar = (from d in _db.Doktorlar where d.Bolum_Id == id select d).ToList();

			//if(doktorlar == null) {
			//	return NotFound(); 
			//}

			return View();
		}

	}
}
