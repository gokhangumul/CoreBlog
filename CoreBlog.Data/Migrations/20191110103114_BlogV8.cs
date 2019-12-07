using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreBlog.Data.Migrations
{
    public partial class BlogV8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlogName",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogName",
                table: "Settings");
        }
    }
}
