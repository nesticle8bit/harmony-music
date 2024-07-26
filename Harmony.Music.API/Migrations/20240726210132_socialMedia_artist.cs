using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmony.Music.API.Migrations
{
    /// <inheritdoc />
    public partial class socialMedia_artist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastfmProfile",
                schema: "music",
                table: "artists",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpotifyProfile",
                schema: "music",
                table: "artists",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastfmProfile",
                schema: "music",
                table: "artists");

            migrationBuilder.DropColumn(
                name: "SpotifyProfile",
                schema: "music",
                table: "artists");
        }
    }
}
