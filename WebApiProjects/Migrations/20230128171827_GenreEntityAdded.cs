using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesDatabase.Api.Migrations
{
    /// <inheritdoc />
    public partial class GenreEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectorMovies_Directors_DirectorsDirectorId",
                table: "DirectorMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectorMovies_Movies_MoviesMovieId",
                table: "DirectorMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Genres_GenresGenreId",
                table: "MovieGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Movies_MoviesMovieId",
                table: "MovieGenres");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Movies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MoviesMovieId",
                table: "MovieGenres",
                newName: "MoviesId");

            migrationBuilder.RenameColumn(
                name: "GenresGenreId",
                table: "MovieGenres",
                newName: "GenresId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenres_MoviesMovieId",
                table: "MovieGenres",
                newName: "IX_MovieGenres_MoviesId");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Genres",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DirectorId",
                table: "Directors",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MoviesMovieId",
                table: "DirectorMovies",
                newName: "MoviesId");

            migrationBuilder.RenameColumn(
                name: "DirectorsDirectorId",
                table: "DirectorMovies",
                newName: "DirectorsId");

            migrationBuilder.RenameIndex(
                name: "IX_DirectorMovies_MoviesMovieId",
                table: "DirectorMovies",
                newName: "IX_DirectorMovies_MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorMovies_Directors_DirectorsId",
                table: "DirectorMovies",
                column: "DirectorsId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorMovies_Movies_MoviesId",
                table: "DirectorMovies",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Genres_GenresId",
                table: "MovieGenres",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Movies_MoviesId",
                table: "MovieGenres",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectorMovies_Directors_DirectorsId",
                table: "DirectorMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectorMovies_Movies_MoviesId",
                table: "DirectorMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Genres_GenresId",
                table: "MovieGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Movies_MoviesId",
                table: "MovieGenres");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Movies",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "MovieGenres",
                newName: "MoviesMovieId");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "MovieGenres",
                newName: "GenresGenreId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenres_MoviesId",
                table: "MovieGenres",
                newName: "IX_MovieGenres_MoviesMovieId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Genres",
                newName: "GenreId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Directors",
                newName: "DirectorId");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "DirectorMovies",
                newName: "MoviesMovieId");

            migrationBuilder.RenameColumn(
                name: "DirectorsId",
                table: "DirectorMovies",
                newName: "DirectorsDirectorId");

            migrationBuilder.RenameIndex(
                name: "IX_DirectorMovies_MoviesId",
                table: "DirectorMovies",
                newName: "IX_DirectorMovies_MoviesMovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorMovies_Directors_DirectorsDirectorId",
                table: "DirectorMovies",
                column: "DirectorsDirectorId",
                principalTable: "Directors",
                principalColumn: "DirectorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorMovies_Movies_MoviesMovieId",
                table: "DirectorMovies",
                column: "MoviesMovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Genres_GenresGenreId",
                table: "MovieGenres",
                column: "GenresGenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Movies_MoviesMovieId",
                table: "MovieGenres",
                column: "MoviesMovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
