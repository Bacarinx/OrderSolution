using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderSolution.API.Migrations
{
    /// <inheritdoc />
    public partial class reorganzandoEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceClientProducts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tab");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Tab",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tab_ClientId",
                table: "Tab",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tab_Clients_ClientId",
                table: "Tab",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tab_Clients_ClientId",
                table: "Tab");

            migrationBuilder.DropIndex(
                name: "IX_Tab_ClientId",
                table: "Tab");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Tab");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Tab",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ServiceClientProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ServiceClientId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceClientProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceClientProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceClientProducts_ServiceClients_ServiceClientId",
                        column: x => x.ServiceClientId,
                        principalTable: "ServiceClients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceClientProducts_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceClientProducts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceClientProducts_ProductId",
                table: "ServiceClientProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceClientProducts_ServiceClientId",
                table: "ServiceClientProducts",
                column: "ServiceClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceClientProducts_ServiceId",
                table: "ServiceClientProducts",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceClientProducts_UserId",
                table: "ServiceClientProducts",
                column: "UserId");
        }
    }
}
