using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class RoleConfigurationSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "512ef45d-9cef-4fad-9fc1-bb96261067f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b64004d-3db3-4860-8a16-f97b7343f698");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f225a586-6e03-4394-b7be-49fe052161cc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0eb0848b-25cb-4939-bd9f-428c90d02ee6", "11c2ae1e-8cad-42de-b498-7f22340a5f38", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1c5fedbf-c90b-4db6-9e97-6298964d318c", "823caafa-1a0e-4a06-81f9-a5b1e67182cb", "Antreneör", "ANTRENEÖR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bd9458a1-5439-468b-a04b-4699c00345ad", "7b194242-f4e3-4388-af33-407ca4156da8", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0eb0848b-25cb-4939-bd9f-428c90d02ee6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c5fedbf-c90b-4db6-9e97-6298964d318c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd9458a1-5439-468b-a04b-4699c00345ad");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "512ef45d-9cef-4fad-9fc1-bb96261067f9", "a15f0bab-9568-4e1c-973b-4481950e6f9b", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5b64004d-3db3-4860-8a16-f97b7343f698", "079a9c2f-545d-4547-8a3c-3c4eb24b2e94", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f225a586-6e03-4394-b7be-49fe052161cc", "106f5477-3ce4-4fbf-9a61-ae7f419a54bd", "Antreneör", "ANTRENEÖR" });
        }
    }
}
