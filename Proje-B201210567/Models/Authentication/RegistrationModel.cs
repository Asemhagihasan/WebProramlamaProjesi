using System.ComponentModel.DataAnnotations;

namespace Proje_B201210567.Models.Authentication
{
	public class RegistrationModel
	{
		[Required]
		public string Adi { get; set; }
		[Required]
		public string Soyadi { get; set; }
		[Required]
		public string TC { get; set; }
		[Required]
		public string TelefonNumarasi { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minimum length 6 and must contain  1 Uppercase,1 lowercase, 1 special character and 1 digit")]
		public string Sifre { get; set; }
		[Required]
		[Compare("Sifre")]
		public string SifreTekrar { get; set; }
		
		[Required]
		
		public string Cinsiyet { get; set; }
		public string? Role { get; set; }
	}
}
