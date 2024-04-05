using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banga.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsblockedOnUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Isblocked",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isblocked",
                table: "AspNetUsers");
        }
    }
}
