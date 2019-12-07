using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreBlog.Data.Migrations
{
    public partial class BlogV7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainHeader",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainSubHeader",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainHeader",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "MainSubHeader",
                table: "Settings");
        }
    }
}
