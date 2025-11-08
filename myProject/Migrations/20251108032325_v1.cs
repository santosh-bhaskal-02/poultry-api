using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_birdInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoxCount = table.Column<int>(type: "int", nullable: false),
                    BirdsPerBoxCount = table.Column<int>(type: "int", nullable: false),
                    TotalBirdCount = table.Column<int>(type: "int", nullable: false),
                    BirdsArrivedCount = table.Column<int>(type: "int", nullable: false),
                    BoxMortalityCount = table.Column<int>(type: "int", nullable: false),
                    DisabledBirdCount = table.Column<int>(type: "int", nullable: false),
                    WeakBirdCount = table.Column<int>(type: "int", nullable: false),
                    ExcessBirdCount = table.Column<int>(type: "int", nullable: false),
                    HousedBirdCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_birdInventory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_dailyRegister",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    BirdInventoryId = table.Column<int>(type: "int", nullable: true),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BirdAgeInDays = table.Column<int>(type: "int", nullable: false),
                    FeedConsumedBags = table.Column<int>(type: "int", nullable: false),
                    MortalityCount = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_dailyRegister", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_dailyRegister_tbl_birdInventory_BirdInventoryId",
                        column: x => x.BirdInventoryId,
                        principalTable: "tbl_birdInventory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_feedInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    BirdInventoryId = table.Column<int>(type: "int", nullable: true),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FeedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BagsArrivedCount = table.Column<int>(type: "int", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_feedInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_feedInventory_tbl_birdInventory_BirdInventoryId",
                        column: x => x.BirdInventoryId,
                        principalTable: "tbl_birdInventory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_dailyRegister_BirdInventoryId",
                table: "tbl_dailyRegister",
                column: "BirdInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_feedInventory_BirdInventoryId",
                table: "tbl_feedInventory",
                column: "BirdInventoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_dailyRegister");

            migrationBuilder.DropTable(
                name: "tbl_feedInventory");

            migrationBuilder.DropTable(
                name: "tbl_birdInventory");
        }
    }
}
