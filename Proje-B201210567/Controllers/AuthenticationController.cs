using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje_B201210567.Data;
using Proje_B201210567.Models.Authentication;
using Proje_B201210567.Repository;

namespace Proje_B201210567.Controllers
{
	public class AuthenticationController : Controller
	{
		private readonly IUserAuthenticationService _authService;
		private readonly AppDbContext _db;
		public AuthenticationController(IUserAuthenticationService authService, AppDbContext db)
		{
			_authService = authService;
			_db = db;
		}

		[HttpGet]
		public IActionResult Registration()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Registration(RegistrationModel model)
		{
			if (model == null)
			{
				return View(model);
			}

			model.Role = "user";
			var result = await _authService.RegistratiopnAsync(model);

			if (result.StatusCode == 0)
			{
				TempData["msg"] = result.Message;
				return RedirectToAction(nameof(Registration));
			}
			else
			{
				if (User.IsInRole("admin"))
				{
					return RedirectToAction("Kullancilar", "Kullanci");
				}
				return RedirectToAction(nameof(Login));
			}
		}


		[HttpGet]
		public IActionResult Login()
		{

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = await _authService.LoginAsync(model);
			if (result.StatusCode == 1)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				TempData["msg"] = result.Message;
				return RedirectToAction(nameof(Login));
			}
		}

		[Authorize]
		public async Task<IActionResult> Logout()
		{

			await _authService.LogoutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}

