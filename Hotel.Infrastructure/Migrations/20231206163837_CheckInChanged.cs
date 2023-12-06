using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CheckInChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CheckOuts_CheckInsId",
                table: "CheckOuts");

            migrationBuilder.AddColumn<int>(
                name: "CheckInsId",
                table: "CheckIns",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckOuts_CheckInsId",
                table: "CheckOuts",
                column: "CheckInsId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CheckOuts_CheckInsId",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "CheckInsId",
                table: "CheckIns");

            migrationBuilder.CreateIndex(
                name: "IX_CheckOuts_CheckInsId",
                table: "CheckOuts",
                column: "CheckInsId");
        }
    }
}
