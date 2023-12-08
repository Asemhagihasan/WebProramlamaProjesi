using Proje_B201210567.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje_B201210567.Models
{
	public class Doktor
	{
		[Key]
		public int DoktorId { get; set; }
		[Required]
        public string Doktor_Adi { get; set; }
		[Required]
        public string Doktor_Soyad { get; set; }
		[Required]
        public string Uzmanlik_Alani { get; set; }

		[ForeignKey("Poliklinik")]
        public string ?Bolum_Id { get; set; }
		public Poliklinik ?poliklinik {  get; set; }

        // Doktorun çalışma tarihleri ve saatleri
        public List<CalismaSaati> CalismaSaatleri { get; set; }
	}

}
