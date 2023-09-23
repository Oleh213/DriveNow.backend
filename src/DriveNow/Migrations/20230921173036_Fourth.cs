using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveNow.Migrations
{
    /// <inheritdoc />
    public partial class Fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "cars");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "cars",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "cars",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "cars");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
