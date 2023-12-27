using System.ComponentModel.DataAnnotations.Schema;

namespace Proje_B201210567.Models
{
    public class CalismaSaati
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Doktor))]
        public int DoktorId { get; set; }
        public DayOfWeek Gun { get; set; } // Haftanın günü  
        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }
        public string ?Tarih {  get; set; }
        public List<TimeSpan> ?DayOfWeeks { get; set; }
        public List<TimeSpan>? RandevuSaatlari { get; set; }
        public bool[] ?IsAvailable { get; set; }
    }
}
