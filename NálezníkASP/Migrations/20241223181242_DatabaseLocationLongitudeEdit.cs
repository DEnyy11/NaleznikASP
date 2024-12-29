using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NálezníkASP.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseLocationLongitudeEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationLangtitude",
                table: "findings",
                newName: "LocationLongitude");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationLongitude",
                table: "findings",
                newName: "LocationLangtitude");
        }
    }
}
