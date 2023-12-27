using Proje_B201210567.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje_B201210567.Models
{
	public class Randevu
	{
		[Key]
		public int RandevuId { get; set; }
		[ForeignKey("Doktor")]
		public int ?DoktorId { get; set; } // Randevunun hangi doktora ait olduğunu belirtmek için
		public Doktor ?doktor {  get; set; }

		[ForeignKey("Kullanci")]
		public int ?KullaniciId { get; set; } // Randevuyu alan kullanıcının Id'si
		public Kullanci ?kullanci { get; set; }
		 public string ?RandevuOlasanTarih {  get; set; }
		public string Durum { get; set; } // Randevu durumu: Onaylanmış, Beklemede, İptal Edilmiş vb.
                                          // Diğer randevu bilgileri

		public int ?BolumId { get; set; }
    }
}
