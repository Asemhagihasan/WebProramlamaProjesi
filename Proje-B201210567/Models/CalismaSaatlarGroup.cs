namespace Proje_B201210567.Models
{
	public class CalismaSaatlarGroup
	{
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoktorSoyad { get; set; }
        public string BolumAdi { get; set; } = "";
        public int KullanciId { get; set; }
        public List<CalismaSaati> CalismaSaatiListesi { get; set; } = new List<CalismaSaati>() { };
        public DateTime BaslangisTarih { get; set; }
        public DateTime BitisTarih { get; set; }
    }
}
