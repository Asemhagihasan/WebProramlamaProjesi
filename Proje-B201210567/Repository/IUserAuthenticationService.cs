using Proje_B201210567.Models.Authentication;

namespace Proje_B201210567.Repository
{
	public interface IUserAuthenticationService
	{
		Task<Status> LoginAsync(LoginModel model);
		Task<Status> RegistratiopnAsync(RegistrationModel model);
		Task LogoutAsync();
	}
}
