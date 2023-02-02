using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonusSystem.Migrations
{
    /// <inheritdoc />
    public partial class BonusEntityAndEmployeeEntityChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "RecommendatorId",
                table: "Bonuses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RecommendatorId",
                table: "Employees",
                column: "RecommendatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonuses_RecommendatorId",
                table: "Bonuses",
                column: "RecommendatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonuses_Employees_RecommendatorId",
                table: "Bonuses",
                column: "RecommendatorId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_RecommendatorId",
                table: "Employees",
                column: "RecommendatorId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonuses_Employees_RecommendatorId",
                table: "Bonuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_RecommendatorId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_RecommendatorId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Bonuses_RecommendatorId",
                table: "Bonuses");

            migrationBuilder.AlterColumn<Guid>(
                name: "RecommendatorId",
                table: "Bonuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
