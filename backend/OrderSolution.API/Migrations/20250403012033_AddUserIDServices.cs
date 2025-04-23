using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderSolution.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIDServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ServiceClients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "ServiceClientProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ServiceClientProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceClients_UserId",
                table: "ServiceClients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceClientProducts_ServiceId",
                table: "ServiceClientProducts",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceClientProducts_UserId",
                table: "ServiceClientProducts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceClientProducts_Services_ServiceId",
                table: "ServiceClientProducts",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceClientProducts_Users_UserId",
                table: "ServiceClientProducts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceClients_Users_UserId",
                table: "ServiceClients",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceClientProducts_Services_ServiceId",
                table: "ServiceClientProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceClientProducts_Users_UserId",
                table: "ServiceClientProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceClients_Users_UserId",
                table: "ServiceClients");

            migrationBuilder.DropIndex(
                name: "IX_ServiceClients_UserId",
                table: "ServiceClients");

            migrationBuilder.DropIndex(
                name: "IX_ServiceClientProducts_ServiceId",
                table: "ServiceClientProducts");

            migrationBuilder.DropIndex(
                name: "IX_ServiceClientProducts_UserId",
                table: "ServiceClientProducts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ServiceClients");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "ServiceClientProducts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ServiceClientProducts");
        }
    }
}
