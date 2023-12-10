using Microsoft.AspNetCore.Mvc;

namespace Proje_B201210567.Controllers
{
	public class DoktorController : Controller
	{
		public IActionResult DoktorListesi()
		{
			return View();
		}

	}
}
