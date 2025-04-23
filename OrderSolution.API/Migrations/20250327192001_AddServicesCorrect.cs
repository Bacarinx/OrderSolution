using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderSolution.API.Migrations
{
    /// <inheritdoc />
    public partial class AddServicesCorrect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartService = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndService = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceClients_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceClients_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceClientProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ServiceClientId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_ServiceClients_ClientId",
                table: "ServiceClients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceClients_ServiceId",
                table: "ServiceClients",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UserId",
                table: "Services",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceClientProducts");

            migrationBuilder.DropTable(
                name: "ServiceClients");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
