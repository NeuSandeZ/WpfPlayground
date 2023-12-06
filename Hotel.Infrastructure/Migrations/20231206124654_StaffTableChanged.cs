using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StaffTableChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staves_Addresses_AddressId",
                table: "Staves");

            migrationBuilder.DropColumn(
                name: "BankAccountNumber",
                table: "Staves");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Staves",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Staves_Addresses_AddressId",
                table: "Staves",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staves_Addresses_AddressId",
                table: "Staves");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Staves",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BankAccountNumber",
                table: "Staves",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Staves_Addresses_AddressId",
                table: "Staves",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
