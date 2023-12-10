using Microsoft.AspNetCore.Mvc;
using Proje_B201210567.Data;
using Proje_B201210567.Models;

namespace Proje_B201210567.Controllers
{
	public class AdminController : Controller
	{
		private readonly AppDbContext _db;
		public AdminController(AppDbContext db)
		{
			_db = db;
		}
		
	}
}
