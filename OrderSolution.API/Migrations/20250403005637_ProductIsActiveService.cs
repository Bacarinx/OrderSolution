using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderSolution.API.Migrations
{
    /// <inheritdoc />
    public partial class ProductIsActiveService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ServiceClientProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ServiceClientProducts");
        }
    }
}
