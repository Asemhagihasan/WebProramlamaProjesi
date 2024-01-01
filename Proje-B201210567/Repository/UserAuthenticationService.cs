using Microsoft.AspNetCore.Identity;
using Proje_B201210567.Models;
using Proje_B201210567.Models.Authentication;
using System.Security.Claims;

namespace Proje_B201210567.Repository
{
	public class UserAuthenticationService : IUserAuthenticationService
	{
		private readonly SignInManager<Kullanci> signInManager;
		private readonly UserManager<Kullanci> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public UserAuthenticationService(SignInManager<Kullanci> signInManager, UserManager<Kullanci> userManager, RoleManager<IdentityRole> roleManager)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.roleManager = roleManager;

		}

		public async Task<Status> LoginAsync(LoginModel model)
		{
			var status = new Status();
			var user = await userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				status.StatusCode = 0;
				status.Message = "Invalid user";
				return status;
			}

			if (!await userManager.CheckPasswordAsync(user, model.Sifre))
			{
				status.StatusCode = 0;
				status.Message = "Invalid password";
				return status;
			}

			var signInResult = await signInManager.PasswordSignInAsync(user, model.Sifre, false, true);
			if (signInResult.Succeeded)
			{
				var userRoles = await userManager.GetRolesAsync(user);
				var authClaims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.UserName),
				};
				foreach (var claim in userRoles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, claim));
				}
				status.StatusCode = 1;
				status.Message = "Logged In Successfully";
				return status;
			}
			else if (signInResult.IsLockedOut)
			{
				status.StatusCode = 0;
				status.Message = "User is locked out";
				return status;
			}
			{
				status.StatusCode = 0;
				status.Message = "Error when logging the user in";
				return status;
			}
		}

		public async Task LogoutAsync()
		{
			await signInManager.SignOutAsync();
		}

		public async Task<Status> RegistratiopnAsync(RegistrationModel model)
		{
			var status = new Status();
			var userExists = await userManager.FindByEmailAsync(model.Email);
			if (userExists != null)
			{
				status.StatusCode = 0;
				status.Message = "User already exists";
				return status;
			}

			Kullanci user = new Kullanci
			{
				SecurityStamp = Guid.NewGuid().ToString(),
				Kullanci_Adi = model.Adi,
				Kullanci_Soyad = model.Soyadi,
				Email = model.Email,
				UserName = model.Adi,
				Tc = model.TC,
				TelefonNumarasi = model.TelefonNumarasi,
				EmailConfirmed = true,
				Cinsel = model.Cinsiyet,
			};



			var result = await userManager.CreateAsync(user, model.Sifre);
			if (!result.Succeeded)
			{
				status.StatusCode = 0;
				status.Message = "User Creation Failed";
				return status;
			}



			if (!await roleManager.RoleExistsAsync(model.Role))
				await roleManager.CreateAsync(new IdentityRole(model.Role));

			await userManager.AddToRoleAsync(user, model.Role);

			status.StatusCode = 1;
			status.Message = "User Created Successfully";
			return status;
		}
	}

}

