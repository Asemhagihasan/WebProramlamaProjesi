using Proje_B201210567.Models;
using System.Collections.Generic;

namespace Proje_B201210567.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
           using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                //Kullancilar
                if(!context.Kullancilar.Any())
                {
                    context.Kullancilar.AddRange(new List<Kullanci>()
                    {
                        new Kullanci()
                        {
                            KullaniciId = 1234,
                            Kullanci_Adi = "Asem",
                            Kullanci_Soyad = "Hagi hasan",
                            Cinsel = "Erkek",
                            Email = "asemhagihasan12345@gmail.com",
                            Sifre = "9924"
                        },
                          new Kullanci()
                        {
                            KullaniciId = 1345,
                            Kullanci_Adi = "omar",
                            Kullanci_Soyad = "shamiye",
                            Cinsel = "kadin",
                            Email = "omarshamiye@gmail.com",
                            Sifre = "9924"
                        },
                          new Kullanci()
                          {
                            KullaniciId = 1246,
                            Kullanci_Adi = "qusai",
                            Kullanci_Soyad = "kalmon",
                            Cinsel = "Erkek",
                            Email = "qusai@gmail.com",
                            Sifre = "9924"
                          },
                           new Kullanci()
                          {
                            KullaniciId = 2314,
                            Kullanci_Adi = "suhaib",
                            Kullanci_Soyad = "hagosman",
                            Cinsel = "Erkek",
                            Email = "suhaib@gmail.com",
                            Sifre = "9924"
                          }
                    });
                    context.SaveChanges();
                }
                //Doktorlar
                if (!context.Doktorlar.Any())
                {
                    context.Doktorlar.AddRange(new List<Doktor>()
                    {
                        new Doktor()
                        {
                            DoktorId = 0001,
                            Doktor_Adi = "ahmet",
                            Doktor_Soyad = "nor",
                            Uzmanlik_Alani = "Çocuk Cerrahisi",
                            CalismaSaatleri = new List<CalismaSaati>()
                            {
                                new CalismaSaati()
                                {
                                    Gun = new DateTime(2023,12,5).DayOfWeek,
                                    BaslangicSaati = new TimeSpan(9,0,0),
                                    BitisSaati = new TimeSpan(17,0,0),
                                },
                                new CalismaSaati()
                                {
                                    Gun = new DateTime(2023,12,7).DayOfWeek,
                                    BaslangicSaati = new TimeSpan(9,0,0),
                                    BitisSaati = new TimeSpan(17,0,0),
                                }
                            }
                        },
                        new Doktor()
                        {
                            DoktorId = 0002,
                            Doktor_Adi = "Hasan",
                            Doktor_Soyad = "hamil",
                            Uzmanlik_Alani = "El Cerrahisi",
                             CalismaSaatleri = new List<CalismaSaati>()
                            {
                                new CalismaSaati()
                                {
                                    Gun = new DateTime(2023,12,4).DayOfWeek,
                                    BaslangicSaati = new TimeSpan(13,0,0),
                                    BitisSaati = new TimeSpan(17,0,0),
                                },
                                new CalismaSaati()
                                {
                                    Gun = new DateTime(2023,12,8).DayOfWeek,
                                    BaslangicSaati = new TimeSpan(9,0,0),
                                    BitisSaati = new TimeSpan(15,0,0),
                                }
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Poliklinikler
                if (!context.Poliklinikler.Any())
                {
                    context.Poliklinikler.AddRange(new List<Poliklinik>()
                    {
                        new Poliklinik()
                        {
                            Bolum_Id = 10,
                            Bolum_Adi = "Dahiliye"
                        },

                        new Poliklinik()
                        {
                            Bolum_Id = 11,
                            Bolum_Adi = "Çocuk Polikliniği"
                        }
                    });
                    context.SaveChanges();

                }
            }
        }
    }
}

