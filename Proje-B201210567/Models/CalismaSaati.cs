using System.ComponentModel.DataAnnotations.Schema;

namespace Proje_B201210567.Models
{
    public class CalismaSaati
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Doktor))]
        public int DoktorId { get; set; }
        public DayOfWeek Gun { get; set; } // Haftanın günü 
        public TimeSpan BaslangicSaati { get; set; } // Çalışma başlangıç saati
        public TimeSpan BitisSaati { get; set; } // Çalışma bitiş saati d
    }
}
