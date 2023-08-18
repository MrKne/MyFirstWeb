using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirst.Web.Migrations.AuthDb
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9dfa7209-45b7-4f56-abd4-82a6d54efaac",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45e903a8-f711-4d0d-92d6-39c955b37b5a", "AQAAAAEAACcQAAAAEKgCuw7vos7iJIwHJlMyrMoEidQ5JKrL3z+8++7ZhZiaM2ty2lfGne7mdbKs3O4cog==", "760a9b6e-9ebc-44a2-a72b-4f5168a8fabb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9dfa7209-45b7-4f56-abd4-82a6d54efaac",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72285076-a7a9-4c01-aef5-70d42455b118", "AQAAAAEAACcQAAAAEGZAAmTASBuhG7L2bkeeGRFWqywwJ4PdDdUn9AASTtxTsUCKB6bjbeAHb3HP0fekhg==", "5d65120a-9e8c-435a-9f6c-196477e157d2" });
        }
    }
}
