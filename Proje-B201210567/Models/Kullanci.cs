using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proje_B201210567.Models
{
	public class Kullanci
	{
        [Key]
        public int KullaniciId { get; set; }

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

		[Required(ErrorMessage = "Email boş bırakılamaz.")]
		[EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
		[DisplayName("Email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Şifre boş bırakılamaz.")]
		[DisplayName("Sifre")]
        public string Sifre { get; set; }
    }
}
