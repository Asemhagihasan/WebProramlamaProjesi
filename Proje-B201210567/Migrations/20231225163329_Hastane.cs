﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Proje_B201210567.Migrations
{
    public partial class Hastane : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kullancilar",
                columns: table => new
                {
                    KullaniciId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tc = table.Column<string>(type: "text", nullable: false),
                    Kullanci_Adi = table.Column<string>(type: "text", nullable: false),
                    Kullanci_Soyad = table.Column<string>(type: "text", nullable: false),
                    TelefonNumarasi = table.Column<string>(type: "text", nullable: false),
                    Cinsel = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Sifre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullancilar", x => x.KullaniciId);
                });

            migrationBuilder.CreateTable(
                name: "Poliklinikler",
                columns: table => new
                {
                    Bolum_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Bolum_Adi = table.Column<string>(type: "text", nullable: false),
                    DoktorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poliklinikler", x => x.Bolum_Id);
                });

            migrationBuilder.CreateTable(
                name: "Doktorlar",
                columns: table => new
                {
                    DoktorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tc = table.Column<string>(type: "text", nullable: false),
                    Doktor_Adi = table.Column<string>(type: "text", nullable: false),
                    Doktor_Soyad = table.Column<string>(type: "text", nullable: false),
                    TelefonNumarasi = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    poliklinikBolum_Id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doktorlar", x => x.DoktorId);
                    table.ForeignKey(
                        name: "FK_Doktorlar_Poliklinikler_poliklinikBolum_Id",
                        column: x => x.poliklinikBolum_Id,
                        principalTable: "Poliklinikler",
                        principalColumn: "Bolum_Id");
                });

            migrationBuilder.CreateTable(
                name: "CalismaSaati",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoktorId = table.Column<int>(type: "integer", nullable: false),
                    Gun = table.Column<int>(type: "integer", nullable: false),
                    BaslangicSaati = table.Column<TimeSpan>(type: "interval", nullable: false),
                    BitisSaati = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Tarih = table.Column<string>(type: "text", nullable: true),
                    DayOfWeeks = table.Column<List<TimeSpan>>(type: "interval[]", nullable: true),
                    RandevuSaatlari = table.Column<List<TimeSpan>>(type: "interval[]", nullable: true),
                    IsAvailable = table.Column<bool[]>(type: "boolean[]", nullable: false),

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalismaSaati", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalismaSaati_Doktorlar_DoktorId",
                        column: x => x.DoktorId,
                        principalTable: "Doktorlar",
                        principalColumn: "DoktorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rendevuler",
                columns: table => new
                {
                    RandevuId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoktorId = table.Column<int>(type: "integer", nullable: true),
                    KullaniciId = table.Column<int>(type: "integer", nullable: true),
                    kullanciKullaniciId = table.Column<int>(type: "integer", nullable: true),
                    RandevuOlasanTarih = table.Column<string>(type:"text", nullable: false),
                    Durum = table.Column<string>(type: "text", nullable: false),
                    BolumId = table.Column<int>(type: "integer", nullable:true ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rendevuler", x => x.RandevuId);
                    table.ForeignKey(
                        name: "FK_Rendevuler_Doktorlar_DoktorId",
                        column: x => x.DoktorId,
                        principalTable: "Doktorlar",
                        principalColumn: "DoktorId");
                    table.ForeignKey(
                        name: "FK_Rendevuler_Kullancilar_kullanciKullaniciId",
                        column: x => x.kullanciKullaniciId,
                        principalTable: "Kullancilar",
                        principalColumn: "KullaniciId");
                });

            migrationBuilder.CreateTable(
                name: "calismaVePolikliniks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    calsismaId = table.Column<int>(type: "integer", nullable: true),
                    poliklinikId = table.Column<int>(type: "integer", nullable: true),
                    DoktorId = table.Column<int>(type: "integer", nullable: true),
                    KullanciId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_calismaVePolikliniks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_calismaVePolikliniks_CalismaSaati_calsismaId",
                        column: x => x.calsismaId,
                        principalTable: "CalismaSaati",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_calismaVePolikliniks_Doktorlar_DoktorId",
                        column: x => x.DoktorId,
                        principalTable: "Doktorlar",
                        principalColumn: "DoktorId");
                    table.ForeignKey(
                        name: "FK_calismaVePolikliniks_Kullancilar_KullanciId",
                        column: x => x.KullanciId,
                        principalTable: "Kullancilar",
                        principalColumn: "KullaniciId");
                    table.ForeignKey(
                        name: "FK_calismaVePolikliniks_Poliklinikler_poliklinikId",
                        column: x => x.poliklinikId,
                        principalTable: "Poliklinikler",
                        principalColumn: "Bolum_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalismaSaati_DoktorId",
                table: "CalismaSaati",
                column: "DoktorId");

            migrationBuilder.CreateIndex(
                name: "IX_calismaVePolikliniks_calsismaId",
                table: "calismaVePolikliniks",
                column: "calsismaId");

            migrationBuilder.CreateIndex(
                name: "IX_calismaVePolikliniks_DoktorId",
                table: "calismaVePolikliniks",
                column: "DoktorId");

            migrationBuilder.CreateIndex(
                name: "IX_calismaVePolikliniks_KullanciId",
                table: "calismaVePolikliniks",
                column: "KullanciId");

            migrationBuilder.CreateIndex(
                name: "IX_calismaVePolikliniks_poliklinikId",
                table: "calismaVePolikliniks",
                column: "poliklinikId");

            migrationBuilder.CreateIndex(
                name: "IX_Doktorlar_poliklinikBolum_Id",
                table: "Doktorlar",
                column: "poliklinikBolum_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rendevuler_DoktorId",
                table: "Rendevuler",
                column: "DoktorId");

            migrationBuilder.CreateIndex(
                name: "IX_Rendevuler_kullanciKullaniciId",
                table: "Rendevuler",
                column: "kullanciKullaniciId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calismaVePolikliniks");

            migrationBuilder.DropTable(
                name: "Rendevuler");

            migrationBuilder.DropTable(
                name: "CalismaSaati");

            migrationBuilder.DropTable(
                name: "Kullancilar");

            migrationBuilder.DropTable(
                name: "Doktorlar");

            migrationBuilder.DropTable(
                name: "Poliklinikler");
        }
    }
}