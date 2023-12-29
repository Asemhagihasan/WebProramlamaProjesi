﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Proje_B201210567.Data;

#nullable disable

namespace Proje_B201210567.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231226115206_AddedtheTable")]
    partial class AddedtheTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("Soyad")
                        .HasColumnType("integer");

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

                    b.Property<int>("DoktorId")
                        .HasColumnType("integer");

                    b.Property<int>("Gun")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DoktorId");

                    b.ToTable("CalismaSaati");
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

                    b.Property<DateTime>("BaslangicTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("BitisTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("DoktorId")
                        .HasColumnType("integer");

                    b.Property<string>("Durum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("KullaniciId")
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