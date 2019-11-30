using Microsoft.EntityFrameworkCore.Migrations;

namespace Iraqi.Heros.Migrations
{
    public partial class CreateReports_addStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Reports",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reports");
        }
    }
}
