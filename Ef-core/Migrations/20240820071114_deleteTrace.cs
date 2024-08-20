using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ef_core.Migrations
{
    public partial class deleteTrace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExceptionStackTrace",
                table: "Errors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExceptionStackTrace",
                table: "Errors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
