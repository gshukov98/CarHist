using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarHist.Blazor.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4ebfd78c-c5fa-433d-b447-d66a3b68e315", "c1b8b090-c284-4ed8-b40d-b55f6284fa8d", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "70f06478-a7b8-4c6f-8fbc-badb3f130059", "a8c90bd4-b314-4932-81c3-3a524bd35ca4", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ebfd78c-c5fa-433d-b447-d66a3b68e315");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70f06478-a7b8-4c6f-8fbc-badb3f130059");
        }
    }
}
