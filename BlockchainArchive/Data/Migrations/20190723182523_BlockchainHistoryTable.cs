using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlockchainArchive.Data.Migrations
{
    public partial class BlockchainHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockReference",
                table: "Files");

            migrationBuilder.CreateTable(
                name: "HistoryEntries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    FileGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEntries_Files_FileGuid",
                        column: x => x.FileGuid,
                        principalTable: "Files",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_FileGuid",
                table: "HistoryEntries",
                column: "FileGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryEntries");

            migrationBuilder.AddColumn<string>(
                name: "BlockReference",
                table: "Files",
                nullable: true);
        }
    }
}
