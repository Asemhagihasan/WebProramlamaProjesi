using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje_B201210567.Migrations
{
    public partial class AddedNewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "AcilisSaati",
                table: "Poliklinikler",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "KapanisSaati",
                table: "Poliklinikler",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcilisSaati",
                table: "Poliklinikler");

            migrationBuilder.DropColumn(
                name: "KapanisSaati",
                table: "Poliklinikler");
        }
    }
}
