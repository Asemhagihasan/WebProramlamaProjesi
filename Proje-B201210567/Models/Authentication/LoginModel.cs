using System.ComponentModel.DataAnnotations;

namespace Proje_B201210567.Models.Authentication
{
	public class LoginModel
	{
		[Required]
		public string Email { get; set; }
		[Required]
		public string Sifre { get; set; }
	}
}
