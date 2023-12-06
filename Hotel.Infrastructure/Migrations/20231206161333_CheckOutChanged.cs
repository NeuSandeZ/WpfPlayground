using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CheckOutChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_Guests_GuestId",
                table: "CheckOuts");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_Rooms_RoomId",
                table: "CheckOuts");

            migrationBuilder.DropIndex(
                name: "IX_CheckOuts_GuestId",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "CheckOuts");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "CheckOuts",
                newName: "CheckInsId");

            migrationBuilder.RenameIndex(
                name: "IX_CheckOuts_RoomId",
                table: "CheckOuts",
                newName: "IX_CheckOuts_CheckInsId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "CheckIns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_CheckIns_CheckInsId",
                table: "CheckOuts",
                column: "CheckInsId",
                principalTable: "CheckIns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_CheckIns_CheckInsId",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "CheckIns");

            migrationBuilder.RenameColumn(
                name: "CheckInsId",
                table: "CheckOuts",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_CheckOuts_CheckInsId",
                table: "CheckOuts",
                newName: "IX_CheckOuts_RoomId");

            migrationBuilder.AddColumn<int>(
                name: "GuestId",
                table: "CheckOuts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CheckOuts_GuestId",
                table: "CheckOuts",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_Guests_GuestId",
                table: "CheckOuts",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_Rooms_RoomId",
                table: "CheckOuts",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
