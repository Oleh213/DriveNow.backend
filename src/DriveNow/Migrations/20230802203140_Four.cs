using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveNow.Migrations
{
    /// <inheritdoc />
    public partial class Four : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Free",
                table: "cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Free",
                table: "cars");
        }
    }
}
