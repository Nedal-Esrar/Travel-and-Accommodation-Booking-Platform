using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TABP.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RolesSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3d736b28-cf80-4dc1-8e49-453e3760f0be"), "Guest" },
                    { new Guid("d2e7d2bb-bc77-4a6d-a43a-763716c6df8b"), "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3d736b28-cf80-4dc1-8e49-453e3760f0be"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d2e7d2bb-bc77-4a6d-a43a-763716c6df8b"));
        }
    }
}
