﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Proje_B201210567.Data;

#nullable disable

namespace Proje_B201210567.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Proje_B201210567.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Tc")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TelefonNumarasi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Proje_B201210567.Models.CalismaSaati", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("BaslangicSaati")
                        .HasColumnType("interval");

                    b.Property<TimeSpan>("BitisSaati")
                        .HasColumnType("interval");

                    b.Property<List<TimeSpan>>("DayOfWeeks")
                        .HasColumnType("interval[]");

                    b.Property<bool[]>("IsAvailable")
                        .HasColumnType("bool[]");
                    

                    b.Property<int>("DoktorId")
                        .HasColumnType("integer");

                    b.Property<int>("Gun")
                        .HasColumnType("integer");

                    b.Property<List<TimeSpan>>("RandevuSaatlari")
                        .HasColumnType("interval[]");

                    b.Property<string>("Tarih")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DoktorId");

                    b.ToTable("CalismaSaati");
                });

            modelBuilder.Entity("Proje_B201210567.Models.CalismaVePoliklinik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("DoktorId")
                        .HasColumnType("integer");

                    b.Property<int?>("KullanciId")
                        .HasColumnType("integer");

                    b.Property<int?>("calsismaId")
                        .HasColumnType("integer");

                    b.Property<int?>("poliklinikId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DoktorId");

                    b.HasIndex("KullanciId");

                    b.HasIndex("calsismaId");

                    b.HasIndex("poliklinikId");

                    b.ToTable("calismaVePolikliniks");
                });

            modelBuilder.Entity("Proje_B201210567.Models.Doktor", b =>
                {
                    b.Property<int>("DoktorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DoktorId"));

                    b.Property<string>("Doktor_Adi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Doktor_Soyad")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Tc")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TelefonNumarasi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("poliklinikBolum_Id")
                        .HasColumnType("integer");

                    b.HasKey("DoktorId");

                    b.HasIndex("poliklinikBolum_Id");

                    b.ToTable("Doktorlar");
                });

            modelBuilder.Entity("Proje_B201210567.Models.Kullanci", b =>
                {
                    b.Property<int>("KullaniciId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("KullaniciId"));

                    b.Property<string>("Cinsel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Kullanci_Adi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Kullanci_Soyad")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Tc")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TelefonNumarasi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("KullaniciId");

                    b.ToTable("Kullancilar");
                });

            modelBuilder.Entity("Proje_B201210567.Models.Poliklinik", b =>
                {
                    b.Property<int>("Bolum_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Bolum_Id"));

                    b.Property<TimeSpan>("AcilisSaati")
                        .HasColumnType("interval");

                    b.Property<string>("Bolum_Adi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("DoktorId")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("KapanisSaati")
                        .HasColumnType("interval");

                    b.HasKey("Bolum_Id");

                    b.ToTable("Poliklinikler");
                });

            modelBuilder.Entity("Proje_B201210567.Models.Randevu", b =>
                {
                    b.Property<int>("RandevuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RandevuId"));

                    b.Property<int?>("DoktorId")
                        .HasColumnType("integer");

                    b.Property<string>("Durum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RandevuOlasanTarih")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("KullaniciId")
                        .HasColumnType("integer");

                    b.Property<int?>("BolumId")
                       .HasColumnType("integer");

                    b.Property<int?>("kullanciKullaniciId")
                        .HasColumnType("integer");

                    b.HasKey("RandevuId");

                    b.HasIndex("DoktorId");

                    b.HasIndex("kullanciKullaniciId");

                    b.ToTable("Rendevuler");
                });

            modelBuilder.Entity("Proje_B201210567.Models.CalismaSaati", b =>
                {
                    b.HasOne("Proje_B201210567.Models.Doktor", null)
                        .WithMany("CalismaSaatleri")
                        .HasForeignKey("DoktorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Proje_B201210567.Models.CalismaVePoliklinik", b =>
                {
                    b.HasOne("Proje_B201210567.Models.Doktor", "Doktor")
                        .WithMany()
                        .HasForeignKey("DoktorId");

                    b.HasOne("Proje_B201210567.Models.Kullanci", "Kullanci")
                        .WithMany()
                        .HasForeignKey("KullanciId");

                    b.HasOne("Proje_B201210567.Models.CalismaSaati", "CalismaSaati")
                        .WithMany()
                        .HasForeignKey("calsismaId");

                    b.HasOne("Proje_B201210567.Models.Poliklinik", "Poliklinik")
                        .WithMany()
                        .HasForeignKey("poliklinikId");

                    b.Navigation("CalismaSaati");

                    b.Navigation("Doktor");

                    b.Navigation("Kullanci");

                    b.Navigation("Poliklinik");
                });

            modelBuilder.Entity("Proje_B201210567.Models.Doktor", b =>
                {
                    b.HasOne("Proje_B201210567.Models.Poliklinik", "poliklinik")
                        .WithMany("DoktorList")
                        .HasForeignKey("poliklinikBolum_Id");

                    b.Navigation("poliklinik");
                });

            modelBuilder.Entity("Proje_B201210567.Models.Randevu", b =>
                {
                    b.HasOne("Proje_B201210567.Models.Doktor", "doktor")
                        .WithMany()
                        .HasForeignKey("DoktorId");

                    b.HasOne("Proje_B201210567.Models.Kullanci", "kullanci")
                        .WithMany()
                        .HasForeignKey("kullanciKullaniciId");

                    b.Navigation("doktor");

                    b.Navigation("kullanci");
                });

            modelBuilder.Entity("Proje_B201210567.Models.Doktor", b =>
                {
                    b.Navigation("CalismaSaatleri");
                });

            modelBuilder.Entity("Proje_B201210567.Models.Poliklinik", b =>
                {
                    b.Navigation("DoktorList");
                });
#pragma warning restore 612, 618
        }
    }
}
