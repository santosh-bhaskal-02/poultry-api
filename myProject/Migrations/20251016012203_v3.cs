using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myProject.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
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
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfBox = table.Column<int>(type: "int", nullable: false),
                    NumberOfBirds = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    NumberOfBirdsArrived = table.Column<int>(type: "int", nullable: false),
                    NumberOfBoxMortality = table.Column<int>(type: "int", nullable: false),
                    NumberOfRuns = table.Column<int>(type: "int", nullable: false),
                    NumberOfWeaks = table.Column<int>(type: "int", nullable: false),
                    NumberOfExcess = table.Column<int>(type: "int", nullable: false),
                    NumberOfBirdsHoused = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_birdInventory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_dailyReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DailyFeedConsume = table.Column<int>(type: "int", nullable: false),
                    DailyMortality = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_dailyReport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_feedInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FeedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfBagsArrived = table.Column<int>(type: "int", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverPhoneNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_feedInventory", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_birdInventory");

            migrationBuilder.DropTable(
                name: "tbl_dailyReport");

            migrationBuilder.DropTable(
                name: "tbl_feedInventory");
        }
    }
}
