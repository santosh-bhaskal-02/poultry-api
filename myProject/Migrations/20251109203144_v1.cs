using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BatchNo = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BoxCount = table.Column<int>(type: "integer", nullable: false),
                    BirdsPerBoxCount = table.Column<int>(type: "integer", nullable: false),
                    TotalBirdCount = table.Column<int>(type: "integer", nullable: false),
                    BirdsArrivedCount = table.Column<int>(type: "integer", nullable: false),
                    BoxMortalityCount = table.Column<int>(type: "integer", nullable: false),
                    DisabledBirdCount = table.Column<int>(type: "integer", nullable: false),
                    WeakBirdCount = table.Column<int>(type: "integer", nullable: false),
                    ExcessBirdCount = table.Column<int>(type: "integer", nullable: false),
                    HousedBirdCount = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_birdInventory", x => x.Id);
                    table.UniqueConstraint("AK_tbl_birdInventory_BatchNo", x => x.BatchNo);
                });

            migrationBuilder.CreateTable(
                name: "tbl_dailyRegister",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BatchNo = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BirdAgeInDays = table.Column<int>(type: "integer", nullable: false),
                    FeedConsumedBags = table.Column<int>(type: "integer", nullable: false),
                    MortalityCount = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_dailyRegister", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_dailyRegister_tbl_birdInventory_BatchNo",
                        column: x => x.BatchNo,
                        principalTable: "tbl_birdInventory",
                        principalColumn: "BatchNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_feedInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BatchNo = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FeedName = table.Column<string>(type: "text", nullable: false),
                    BagsArrivedCount = table.Column<int>(type: "integer", nullable: false),
                    DriverName = table.Column<string>(type: "text", nullable: false),
                    DriverPhoneNumber = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_feedInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_feedInventory_tbl_birdInventory_BatchNo",
                        column: x => x.BatchNo,
                        principalTable: "tbl_birdInventory",
                        principalColumn: "BatchNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_birdInventory_BatchNo",
                table: "tbl_birdInventory",
                column: "BatchNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_dailyRegister_BatchNo",
                table: "tbl_dailyRegister",
                column: "BatchNo");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_feedInventory_BatchNo",
                table: "tbl_feedInventory",
                column: "BatchNo");
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
