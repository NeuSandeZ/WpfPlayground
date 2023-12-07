using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoomPromotionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomPromotionsId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomPromotionsId",
                table: "Rooms",
                column: "RoomPromotionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomPromotions_RoomPromotionsId",
                table: "Rooms",
                column: "RoomPromotionsId",
                principalTable: "RoomPromotions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomPromotions_RoomPromotionsId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomPromotionsId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomPromotionsId",
                table: "Rooms");
        }
    }
}
