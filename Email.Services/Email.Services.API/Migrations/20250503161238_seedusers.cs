using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Email.Services.API.Migrations
{
    /// <inheritdoc />
    public partial class seedusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "UserName" },
                values: new object[,]
                {
                    { 1, "srikanthg@technoflair.com", "srikanthg" },
                    { 2, "revanthv@technoflair.com", "revanthv" },
                    { 3, "sreedhars@technoflair.com", "sreedhars" },
                    { 4, "mohsinm@technoflair.com", "mohsinm" },
                    { 5, "akhilas@technoflair.com", "akhilas" },
                    { 6, "ravichandrav@technoflair.com", "ravichandrav" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
