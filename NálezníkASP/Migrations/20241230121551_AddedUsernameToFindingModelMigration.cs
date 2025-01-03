using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NálezníkASP.Migrations
{
    /// <inheritdoc />
    public partial class AddedUsernameToFindingModelMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "findings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "findings");
        }
    }
}
