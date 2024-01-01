using System.ComponentModel.DataAnnotations.Schema;

namespace Proje_B201210567.Models
{
    public class RandevuSaatlari
    {
        public int Id { get; set; }
        public int DoktorId { get; set; }
        public DayOfWeek Gun { get; set; } // Haftanın günü 
        public TimeSpan BaslangicSaati { get; set; } // Çalışma başlangıç saati
        public TimeSpan BitisSaati { get; set; } // Çalışma bitiş saati d
        public List<CalismaSaati> CalismaSaatlari { get;}
    }
}
