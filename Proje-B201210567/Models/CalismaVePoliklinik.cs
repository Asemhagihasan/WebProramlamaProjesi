using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje_B201210567.Models
{
    public class CalismaVePoliklinik
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CalismaSaati")]
        public int ?calsismaId { get; set; }
        public CalismaSaati ?CalismaSaati { get; set; }

        [ForeignKey("Poliklinik")]
        public int ?poliklinikId { get; set; }
        public Poliklinik ?Poliklinik { get; set; }

        [ForeignKey("Doktor")]

        public int ?DoktorId { get; set; }
        public Doktor ?Doktor { get; set; }
        [ForeignKey("Kullanci")]
        public string? KullanciId { get; set; }
        public Kullanci ?Kullanci { get; set; }
    }
}
