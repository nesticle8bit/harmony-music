using System;
using System.Collections.Generic;
using Harmony.Music.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Harmony.Music.API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "music");

            migrationBuilder.CreateTable(
                name: "albums",
                schema: "music",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Artwork = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Title = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    Disc = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    MusicBrainzDiscId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Genres = table.Column<List<string>>(type: "jsonb", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_albums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "artists",
                schema: "music",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    FoundedIn = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<List<string>>(type: "jsonb", nullable: true),
                    MetalArchivesUrl = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "library",
                schema: "music",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: true),
                    HasBeenProcessed = table.Column<bool>(type: "boolean", nullable: false),
                    ReadingFiles = table.Column<bool>(type: "boolean", nullable: false),
                    ArtistImages = table.Column<bool>(type: "boolean", nullable: false),
                    AlbumImages = table.Column<bool>(type: "boolean", nullable: false),
                    AlbumColors = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_library", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "songs",
                schema: "music",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlbumId = table.Column<long>(type: "bigint", nullable: false),
                    Track = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Mimetype = table.Column<string>(type: "text", nullable: false),
                    PossiblyCorrupt = table.Column<bool>(type: "boolean", nullable: false),
                    Lyrics = table.Column<string>(type: "text", nullable: true),
                    MediaProperties = table.Column<MediaPropertyDto>(type: "jsonb", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_songs_albums_AlbumId",
                        column: x => x.AlbumId,
                        principalSchema: "music",
                        principalTable: "albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artistsAlbums",
                schema: "music",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArtistId = table.Column<long>(type: "bigint", nullable: false),
                    AlbumId = table.Column<long>(type: "bigint", nullable: false),
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
                name: "IX_albums_Id",
                schema: "music",
                table: "albums",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_artists_Id",
                schema: "music",
                table: "artists",
                column: "Id",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_library_Id",
                schema: "music",
                table: "library",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_songs_AlbumId",
                schema: "music",
                table: "songs",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_songs_Id",
                schema: "music",
                table: "songs",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artistsAlbums",
                schema: "music");

            migrationBuilder.DropTable(
                name: "library",
                schema: "music");

            migrationBuilder.DropTable(
                name: "songs",
                schema: "music");

            migrationBuilder.DropTable(
                name: "artists",
                schema: "music");

            migrationBuilder.DropTable(
                name: "albums",
                schema: "music");
        }
    }
}
