using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TABP.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RoomType",
                table: "RoomClasses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_RoomClasses_AdultsCapacity_ChildrenCapacity",
                table: "RoomClasses",
                columns: new[] { "AdultsCapacity", "ChildrenCapacity" });

            migrationBuilder.CreateIndex(
                name: "IX_RoomClasses_PricePerNight",
                table: "RoomClasses",
                column: "PricePerNight");

            migrationBuilder.CreateIndex(
                name: "IX_RoomClasses_RoomType",
                table: "RoomClasses",
                column: "RoomType");

            migrationBuilder.CreateIndex(
                name: "IX_Images_EntityId",
                table: "Images",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_StarRating",
                table: "Hotels",
                column: "StarRating");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_StartDateUtc_EndDateUtc",
                table: "Discounts",
                columns: new[] { "StartDateUtc", "EndDateUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CheckInDateUtc_CheckOutDateUtc",
                table: "Bookings",
                columns: new[] { "CheckInDateUtc", "CheckOutDateUtc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoomClasses_AdultsCapacity_ChildrenCapacity",
                table: "RoomClasses");

            migrationBuilder.DropIndex(
                name: "IX_RoomClasses_PricePerNight",
                table: "RoomClasses");

            migrationBuilder.DropIndex(
                name: "IX_RoomClasses_RoomType",
                table: "RoomClasses");

            migrationBuilder.DropIndex(
                name: "IX_Images_EntityId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_StarRating",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_StartDateUtc_EndDateUtc",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CheckInDateUtc_CheckOutDateUtc",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "RoomType",
                table: "RoomClasses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
