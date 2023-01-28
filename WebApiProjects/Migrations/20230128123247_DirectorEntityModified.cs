using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesDatabase.Api.Migrations
{
    /// <inheritdoc />
    public partial class DirectorEntityModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectorMovies_Directors_DirectorsProducerId",
                table: "DirectorMovies");

            migrationBuilder.RenameColumn(
                name: "ProducerId",
                table: "Directors",
                newName: "DirectorId");

            migrationBuilder.RenameColumn(
                name: "DirectorsProducerId",
                table: "DirectorMovies",
                newName: "DirectorsDirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorMovies_Directors_DirectorsDirectorId",
                table: "DirectorMovies",
                column: "DirectorsDirectorId",
                principalTable: "Directors",
                principalColumn: "DirectorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectorMovies_Directors_DirectorsDirectorId",
                table: "DirectorMovies");

            migrationBuilder.RenameColumn(
                name: "DirectorId",
                table: "Directors",
                newName: "ProducerId");

            migrationBuilder.RenameColumn(
                name: "DirectorsDirectorId",
                table: "DirectorMovies",
                newName: "DirectorsProducerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorMovies_Directors_DirectorsProducerId",
                table: "DirectorMovies",
                column: "DirectorsProducerId",
                principalTable: "Directors",
                principalColumn: "ProducerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
