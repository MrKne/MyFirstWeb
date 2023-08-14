using Microsoft.EntityFrameworkCore.Migrations;



#nullable disable

namespace MyFirst.Web.Migrations
{
    public partial class addeddescreption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AdvertPostsComment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AdvertPostsComment");
        }
    }
}
