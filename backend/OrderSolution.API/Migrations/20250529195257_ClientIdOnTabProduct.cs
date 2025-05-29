using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderSolution.API.Migrations
{
    /// <inheritdoc />
    public partial class ClientIdOnTabProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "TabProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TabProducts_ClientId",
                table: "TabProducts",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_TabProducts_Clients_ClientId",
                table: "TabProducts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabProducts_Clients_ClientId",
                table: "TabProducts");

            migrationBuilder.DropIndex(
                name: "IX_TabProducts_ClientId",
                table: "TabProducts");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "TabProducts");
        }
    }
}
