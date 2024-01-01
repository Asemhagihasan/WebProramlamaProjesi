using System.ComponentModel.DataAnnotations.Schema;

namespace Proje_B201210567.Models
{
    public class RandevuYazdir
    {
        public string KullanciAdi { get; set; }
        public string Tc {  get; set; }

        public string DoktorAdi { get; set; }

        public string BolumAdi { get; set; }

        [ForeignKey(nameof(Kullanci))]
        public string Id { get; set; }
        public int BolumId { get; set; }
        public int DokrorId { get; set; }
    }
}
