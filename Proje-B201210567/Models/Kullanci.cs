using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proje_B201210567.Models
{
	public class Kullanci : IdentityUser
	{
        

		[Required]
        public string Tc { get; set; }

        [Required(ErrorMessage = "Kullanıcı Adı boş bırakılamaz.")]
		[DisplayName("Adı")]
        public string Kullanci_Adi { get; set; }

		[Required(ErrorMessage = "Kullanıcı Soyadı boş bırakılamaz.")]
		[DisplayName("Soyad")]
        public string Kullanci_Soyad { get; set; }

		[Required(ErrorMessage = "Telefon Numarası boş bırakılamaz.")]
		[DisplayName("Telefon Numarası")]
        public string TelefonNumarasi { get; set; }

		[Required(ErrorMessage = "Cinsel seçimi yapmalısınız.")]
		public string Cinsel { get; set; }
	}
}
