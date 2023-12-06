using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CheckInsChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "CheckIns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_ReservationId",
                table: "CheckIns",
                column: "ReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Reservations_ReservationId",
                table: "CheckIns",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Reservations_ReservationId",
                table: "CheckIns");

            migrationBuilder.DropIndex(
                name: "IX_CheckIns_ReservationId",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "CheckIns");
        }
    }
}
