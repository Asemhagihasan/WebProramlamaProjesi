using System.ComponentModel.DataAnnotations;

namespace Proje_B201210567.Models
{
    public class RandevuModelleri
    {
        public List <Poliklinik> poliklinikler { get; set; }
		public Poliklinik Poliklinik { get; set; }
		 public int SelectedBolumId { get; set; }

		public List<Doktor> doktorlar { get; set; }
		public Doktor Doktor { get; set; }

		public Kullanci Kullanci { get; set; }

		[Required(ErrorMessage = "Başlangıç tarihi zorunludur.")]
		[DataType(DataType.Date)]
		public DateTime BaslangicTarihi { get; set; }

		[Required(ErrorMessage = "Bitiş tarihi zorunludur.")]
		[DataType(DataType.Date)]
		public DateTime BitisTarihi { get; set; }

	}
}
