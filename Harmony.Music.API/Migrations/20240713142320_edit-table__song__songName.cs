using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmony.Music.API.Migrations
{
    /// <inheritdoc />
    public partial class edittable__song__songName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "music",
                table: "songs",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "music",
                table: "songs");
        }
    }
}
