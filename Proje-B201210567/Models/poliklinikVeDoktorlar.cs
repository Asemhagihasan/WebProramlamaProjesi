namespace Proje_B201210567.Models
{
    public class poliklinikVeDoktorlar
    {
        public List<Doktor> doktors { get; set; } = new List<Doktor>();
        public Poliklinik Poliklinik { get; set; }
    }
}
