using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreBlog.Data.Migrations
{
    public partial class BlogV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Posts",
                newName: "PostContent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostContent",
                table: "Posts",
                newName: "Content");
        }
    }
}
