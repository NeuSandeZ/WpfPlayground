using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoomPromotionEntityChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomPromotions_RoomPromotionsId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomPromotionsId",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "RoomPromotions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RoomPromotions_RoomId",
                table: "RoomPromotions",
                column: "RoomId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomPromotions_Rooms_RoomId",
                table: "RoomPromotions",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomPromotions_Rooms_RoomId",
                table: "RoomPromotions");

            migrationBuilder.DropIndex(
                name: "IX_RoomPromotions_RoomId",
                table: "RoomPromotions");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "RoomPromotions");

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
    }
}
