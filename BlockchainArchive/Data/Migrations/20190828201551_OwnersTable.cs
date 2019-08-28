using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlockchainArchive.Data.Migrations
{
    public partial class OwnersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "Files",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileOwners",
                columns: table => new
                {
                    FileGuid = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileOwners", x => new { x.FileGuid, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_FileOwners_Files_FileGuid",
                        column: x => x.FileGuid,
                        principalTable: "Files",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileOwners_OwnerId",
                table: "FileOwners",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileOwners");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "Files");
        }
    }
}
