using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Harmony.Music.API.Migrations
{
    /// <inheritdoc />
    public partial class new_relations__artistSong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_songs_albums_AlbumId",
                schema: "music",
                table: "songs");

            migrationBuilder.DropTable(
                name: "artistsAlbums",
                schema: "music");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "music",
                table: "songs",
                type: "character varying(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Mimetype",
                schema: "music",
                table: "songs",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "music",
                table: "songs",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AlbumId",
                schema: "music",
                table: "songs",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ArtistId",
                schema: "music",
                table: "songs",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_songs_ArtistId",
                schema: "music",
                table: "songs",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_songs_albums_AlbumId",
                schema: "music",
                table: "songs",
                column: "AlbumId",
                principalSchema: "music",
                principalTable: "albums",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_songs_artists_ArtistId",
                schema: "music",
                table: "songs",
                column: "ArtistId",
                principalSchema: "music",
                principalTable: "artists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_songs_albums_AlbumId",
                schema: "music",
                table: "songs");

            migrationBuilder.DropForeignKey(
                name: "FK_songs_artists_ArtistId",
                schema: "music",
                table: "songs");

            migrationBuilder.DropIndex(
                name: "IX_songs_ArtistId",
                schema: "music",
                table: "songs");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                schema: "music",
                table: "songs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "music",
                table: "songs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Mimetype",
                schema: "music",
                table: "songs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "music",
                table: "songs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AlbumId",
                schema: "music",
                table: "songs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "artistsAlbums",
                schema: "music",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlbumId = table.Column<long>(type: "bigint", nullable: false),
                    ArtistId = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artistsAlbums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_artistsAlbums_albums_AlbumId",
                        column: x => x.AlbumId,
                        principalSchema: "music",
                        principalTable: "albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_artistsAlbums_artists_ArtistId",
                        column: x => x.ArtistId,
                        principalSchema: "music",
                        principalTable: "artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_artistsAlbums_AlbumId",
                schema: "music",
                table: "artistsAlbums",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_artistsAlbums_ArtistId",
                schema: "music",
                table: "artistsAlbums",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_artistsAlbums_Id",
                schema: "music",
                table: "artistsAlbums",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_songs_albums_AlbumId",
                schema: "music",
                table: "songs",
                column: "AlbumId",
                principalSchema: "music",
                principalTable: "albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
