using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirst.Web.Migrations
{
    public partial class AddingcommentsDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvertPostsComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdvertPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertPostsComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertPostsComment_AdvertPosts_AdvertPostId",
                        column: x => x.AdvertPostId,
                        principalTable: "AdvertPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertPostsComment_AdvertPostId",
                table: "AdvertPostsComment",
                column: "AdvertPostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertPostsComment");
        }
    }
}
