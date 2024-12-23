using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NálezníkASP.Migrations
{
    /// <inheritdoc />
    public partial class AddedFindingMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "findings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FindingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Depth = table.Column<double>(type: "float", nullable: false),
                    Coin = table.Column<bool>(type: "bit", nullable: false),
                    LocationLatitude = table.Column<int>(type: "int", nullable: false),
                    LocationLangtitude = table.Column<int>(type: "int", nullable: false),
                    MintingYear = table.Column<int>(type: "int", nullable: false),
                    Nominal = table.Column<int>(type: "int", nullable: false),
                    FindingPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    AfterCleanPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_findings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "findings");
        }
    }
}
