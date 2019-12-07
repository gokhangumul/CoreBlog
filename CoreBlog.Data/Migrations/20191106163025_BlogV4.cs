using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreBlog.Data.Migrations
{
    public partial class BlogV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostDetailHeaderImage",
                table: "Posts",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UniqKey",
                table: "Categories",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostDetailHeaderImage",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "UniqKey",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
