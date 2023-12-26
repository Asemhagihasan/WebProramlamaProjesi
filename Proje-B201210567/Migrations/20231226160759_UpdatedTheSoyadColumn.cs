using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje_B201210567.Migrations
{
    public partial class UpdatedTheSoyadColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Soyad",
                table: "Admins",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Soyad",
                table: "Admins",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
