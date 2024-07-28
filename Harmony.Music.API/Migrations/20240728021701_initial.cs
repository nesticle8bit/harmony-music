using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Harmony.Music.API.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
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
                    Artists = table.Column<List<long>>(type: "jsonb", nullable: true),
                    Hash = table.Column<string>(type: "text", nullable: false),
                    Artwork = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Title = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    Disc = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Genres = table.Column<string>(type: "jsonb", nullable: true),
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
                    Hash = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    FoundedIn = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Tags = table.Column<List<string>>(type: "jsonb", nullable: true),
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
                    HasBeenProcessed = table.Column<bool>(type: "boolean", nullable: false)
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
                    LibraryId = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    AlbumId = table.Column<long>(type: "bigint", nullable: true),
                    Artists = table.Column<List<long>>(type: "jsonb", nullable: true),
                    Track = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Mimetype = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    PossiblyCorrupt = table.Column<bool>(type: "boolean", nullable: false),
                    Lyrics = table.Column<string>(type: "text", nullable: true),
                    MediaProperties = table.Column<string>(type: "jsonb", nullable: true),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_songs_library_LibraryId",
                        column: x => x.LibraryId,
                        principalSchema: "music",
                        principalTable: "library",
                        principalColumn: "Id");
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

            migrationBuilder.CreateIndex(
                name: "IX_songs_LibraryId",
                schema: "music",
                table: "songs",
                column: "LibraryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artists",
                schema: "music");

            migrationBuilder.DropTable(
                name: "songs",
                schema: "music");

            migrationBuilder.DropTable(
                name: "albums",
                schema: "music");

            migrationBuilder.DropTable(
                name: "library",
                schema: "music");
        }
    }
}
