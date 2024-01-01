using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proje_B201210567.Data;
using Proje_B201210567.Models;
using Proje_B201210567.Models.Authentication;
using Proje_B201210567.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Proje_B201210567.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize(Roles = "admin")]
	public class AdminApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly UserManager<Kullanci> _userManager;
        private readonly SignInManager<Kullanci> _signInManager;
        private readonly IUserAuthenticationService _authService;
        public AdminApiController(AppDbContext db, UserManager<Kullanci> userManager, SignInManager<Kullanci> signInManager, IUserAuthenticationService authService)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
        }
        //api/products
        [HttpGet]
        public async Task<List<Kullanci>> GetAsync()
        {
            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            return adminUsers.ToList(); ;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrationModel admin)
        {
            admin.Role = "admin";
            var result = await _authService.RegistratiopnAsync(admin);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Kullanci>> GetAsync(string? id)
        {
            if (id is null)
            {
                return NotFound();
            }
			var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            var admin = adminUsers.FirstOrDefault(k => k.Id == id);
			if (admin == null)
            {
                return NotFound();
            }
            return admin;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string? id, [FromBody] Kullanci y)
        {
            if (id is null)
            {
                return NotFound();
            }
			var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            var user = adminUsers.SingleOrDefault(k => k.Id == id);
	

			if (user == null)
			{
                return NotFound();
            }
            user.Tc = y.Tc;
			user.Kullanci_Adi = y.Kullanci_Adi;
			user.Kullanci_Soyad = y.Kullanci_Soyad;
			user.TelefonNumarasi = y.TelefonNumarasi;
            user.Cinsel = y.Cinsel;
			var result = await _userManager.UpdateAsync(user);
			return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if (id is null)
            {
                return NotFound();
            }
			var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            var DeletedAdmin = adminUsers.SingleOrDefault(k => k.Id == id);
            if (DeletedAdmin == null)
            {
                return NotFound();
            }
			var result = await _userManager.DeleteAsync(DeletedAdmin);

			return Ok(DeletedAdmin);
        }
    }
}

