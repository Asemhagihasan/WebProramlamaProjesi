using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proje_B201210567.Models;

namespace Proje_B201210567.Data
{
	public class AppDbContext: IdentityDbContext<Kullanci>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) 
		{ 
			
		}

        public DbSet<Kullanci> Kullancilar { get; set; }
        public DbSet<Doktor> Doktorlar { get; set; }
        public DbSet<Randevu> Rendevuler { get; set; }
        public DbSet<Poliklinik> Poliklinikler { get; set; }
        public DbSet<CalismaSaati> CalismaSaati { get; set; }
        public DbSet<CalismaVePoliklinik> calismaVePolikliniks { get; set; }


    }
}
