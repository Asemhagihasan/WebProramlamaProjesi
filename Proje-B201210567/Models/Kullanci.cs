using System.ComponentModel.DataAnnotations;

namespace Proje_B201210567.Models
{
	public class Kullanci
	{
        [Key]
        public int KullaniciId { get; set; }
        [Required]
        public string Kullanci_Adi { get; set; }
		[Required]
		public string Kullanci_Soyad { get; set; }
        public int TelefonNumarasi { get; set; }
        public string Cinsel { get; set; }
        [Required]
		public string Email { get; set; }
		[Required]
		public string Sifre { get; set; }
    }
}
