using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetChill.Persistence.Migrations.NetChillDb
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUpcoming",
                table: "MovieBaseInfoes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUpcoming",
                table: "MovieBaseInfoes");
        }
    }
}
