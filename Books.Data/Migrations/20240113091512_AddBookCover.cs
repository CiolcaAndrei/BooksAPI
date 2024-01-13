using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Books.Data.Migrations
{
    public partial class AddBookCover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverPath",
                table: "Books",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverPath",
                table: "Books");
        }
    }
}
