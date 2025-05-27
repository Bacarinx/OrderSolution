using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderSolution.API.Migrations
{
    /// <inheritdoc />
    public partial class T : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tab_Users_UserId",
                table: "Tab");

            migrationBuilder.AddForeignKey(
                name: "FK_Tab_Users_UserId",
                table: "Tab",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tab_Users_UserId",
                table: "Tab");

            migrationBuilder.AddForeignKey(
                name: "FK_Tab_Users_UserId",
                table: "Tab",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
