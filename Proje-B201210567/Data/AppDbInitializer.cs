using Proje_B201210567.Models;
using System.Collections.Generic;

namespace Proje_B201210567.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                //Kullancilar
                if (!context.Kullancilar.Any())
                {
                    context.Kullancilar.AddRange(new List<Kullanci>()
                    {
                        new Kullanci()
                        {
                            Id = "1",
                            Tc = "99241321121",
                            Kullanci_Adi = "halo",
                            Kullanci_Soyad = "Hagi hasan",
                            TelefonNumarasi = "05526492765",
                            Cinsel = "Erkek",
                            Email = "halo@gmail.com",
                            PasswordHash = "9924281328Asem*"
                        },
                          new Kullanci()
                        {
                            Id = "2",
                            Tc = "99242813277",
                            Kullanci_Adi = "omar",
                            Kullanci_Soyad = "shamiye",
                            TelefonNumarasi = "05526492981",
                            Cinsel = "kadin",
                            Email = "omarshamiye@gmail.com",
                            PasswordHash = "9924151231AsS*"
                        },
                          new Kullanci()
                          {
							 Id = "3",
							Tc = "99242813266",
                            Kullanci_Adi = "qusai",
                            Kullanci_Soyad = "kalmon",
                            TelefonNumarasi = "05525592911",
                            Cinsel = "Erkek",
                            Email = "qusai@gmail.com",
                            PasswordHash = "99241541231Qad*"
                          },
                           new Kullanci()
                          {
                            Id = "4",
                            Tc = "99242813255",
                            Kullanci_Adi = "suhaib",
                            Kullanci_Soyad = "hagosman",
                            TelefonNumarasi = "05526464911",
                            Cinsel = "Erkek",
                            Email = "suhaib@gmail.com",
                            PasswordHash = "992428165SAMI*"
                          }
                    });
                    context.SaveChanges();
                }
                //Doktorlar
                //if (!context.Doktorlar.Any())
                //{
                //    context.Doktorlar.AddRange(new List<Doktor>()
                //    {
                //        new Doktor()
                //        {
                //            DoktorId = 0001,
                //            Doktor_Adi = "ahmet",
                //            Doktor_Soyad = "nor",
                //            CalismaSaatleri = new List<CalismaSaati>()
                //            {
                //                new CalismaSaati()
                //                {
                //                    Gun = new DateTime(2023,12,5).DayOfWeek,
                //                    BaslangicSaati = new TimeSpan(9,0,0),
                //                    BitisSaati = new TimeSpan(17,0,0),
                //                },
                //                new CalismaSaati()
                //                {
                //                    Gun = new DateTime(2023,12,7).DayOfWeek,
                //                    BaslangicSaati = new TimeSpan(9,0,0),
                //                    BitisSaati = new TimeSpan(17,0,0),
                //                }
                //            }
                //        },
                //        new Doktor()
                //        {
                //            DoktorId = 0002,
                //            Doktor_Adi = "Hasan",
                //            Doktor_Soyad = "hamil",
                //             CalismaSaatleri = new List<CalismaSaati>()
                //            {
                //                new CalismaSaati()
                //                {
                //                    Gun = new DateTime(2023,12,4).DayOfWeek,
                //                    BaslangicSaati = new TimeSpan(13,0,0),
                //                    BitisSaati = new TimeSpan(17,0,0),
                //                },
                //                new CalismaSaati()
                //                {
                //                    Gun = new DateTime(2023,12,8).DayOfWeek,
                //                    BaslangicSaati = new TimeSpan(9,0,0),
                //                    BitisSaati = new TimeSpan(15,0,0),
                //                }
                //            }
                //        }
                //    });
                //    context.SaveChanges();
                //}
                //Poliklinikler
                if (!context.Poliklinikler.Any())
                {
                    context.Poliklinikler.AddRange(new List<Poliklinik>()
                    {
                        new Poliklinik()
                        {
                            Bolum_Id = 10,
                            Bolum_Adi = "Dahiliye",
                            DoktorList = new List<Doktor>()
                            {
                                new Doktor()
                                {
                                    DoktorId = 1,
                                    Tc = "99242813211",
                                    Doktor_Adi = "ahmet",
                                    Doktor_Soyad = "nor",
                                    Cinsyet ="Erkek",
                                    TelefonNumarasi = "05528728212",
                                    Email = "ahmet@gmail.com",
                                    CalismaSaatleri = new List<CalismaSaati>(){}
                                },
                                new Doktor()
                                {
                                    DoktorId = 2,
                                    Tc = "99242813211",
                                    Doktor_Adi = "Hasan",
                                    Doktor_Soyad = "hamil",
                                     Cinsyet ="Erkek",
                                    TelefonNumarasi = "5526492965",
                                    Email = "Hasan@gmail.com",
                                    CalismaSaatleri = new List<CalismaSaati>(){}
                                }
                            }
                        },

                        new Poliklinik()
                        {
                            Bolum_Id = 11,
                            Bolum_Adi = "Çocuk Polikliniği",
                            DoktorList = new List<Doktor>()
                            {
                                new Doktor()
                                {
                                    DoktorId = 3,
                                    Tc = "99242713344",
                                    Doktor_Adi = "nor",
                                    Doktor_Soyad = "hala",
                                     Cinsyet ="Kız",
                                    TelefonNumarasi = "5526492988",
                                    Email = "nor@gmail.com",
                                   CalismaSaatleri = new List<CalismaSaati>(){}
                                },
                                new Doktor()
                                {
                                    DoktorId = 4,
                                    Tc = "992427133045",
                                    Doktor_Adi = "hany",
                                    Doktor_Soyad = "san",
                                     Cinsyet ="Erkek",
                                    TelefonNumarasi = "05526482533",
                                    Email = "hany@gmail.com",
                                    CalismaSaatleri = new List<CalismaSaati>(){}
                                }
                            }
                        }
                    });
                    context.SaveChanges();

                }
            }
        }
    }
}


