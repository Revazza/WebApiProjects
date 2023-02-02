using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonusSystem.Migrations
{
    /// <inheritdoc />
    public partial class Initial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_RecommendatorId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_RecommendatorId",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employees_RecommendatorId",
                table: "Employees",
                column: "RecommendatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_RecommendatorId",
                table: "Employees",
                column: "RecommendatorId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
