using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje_B201210567.Models
{
	public class Poliklinik
	{
        [Key]
        public int Bolum_Id { get; set; }
        public string Bolum_Adi { get; set; }

        [ForeignKey("Doktor")]
        public int ?DoktorId { get; set; }
        public List<Doktor> DoktorList { get; set;}
    }
}
