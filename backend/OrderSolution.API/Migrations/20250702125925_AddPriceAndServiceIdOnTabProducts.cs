using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderSolution.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceAndServiceIdOnTabProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "TabProducts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "TabProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TabProducts_ServiceId",
                table: "TabProducts",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TabProducts_Services_ServiceId",
                table: "TabProducts",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabProducts_Services_ServiceId",
                table: "TabProducts");

            migrationBuilder.DropIndex(
                name: "IX_TabProducts_ServiceId",
                table: "TabProducts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TabProducts");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "TabProducts");
        }
    }
}
