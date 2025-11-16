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
                name: "tbl_batch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BatchNo = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_batch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_birdInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BatchId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BoxCount = table.Column<int>(type: "integer", nullable: false),
                    BirdsPerBoxCount = table.Column<int>(type: "integer", nullable: false),
                    TotalBirdCount = table.Column<int>(type: "integer", nullable: false),
                    BirdsArrivedCount = table.Column<int>(type: "integer", nullable: false),
                    BoxMortalityCount = table.Column<int>(type: "integer", nullable: false),
                    DisabledBirdCount = table.Column<int>(type: "integer", nullable: false),
                    WeakBirdCount = table.Column<int>(type: "integer", nullable: false),
                    ShortBirdCount = table.Column<int>(type: "integer", nullable: false),
                    ExcessBirdCount = table.Column<int>(type: "integer", nullable: false),
                    HousedBirdCount = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_birdInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_birdInventory_tbl_batch_BatchId",
                        column: x => x.BatchId,
                        principalTable: "tbl_batch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_dailyRegister",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BatchId = table.Column<int>(type: "integer", nullable: false),
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
                        name: "FK_tbl_dailyRegister_tbl_batch_BatchId",
                        column: x => x.BatchId,
                        principalTable: "tbl_batch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_feedInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BatchId = table.Column<int>(type: "integer", nullable: false),
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
                        name: "FK_tbl_feedInventory_tbl_batch_BatchId",
                        column: x => x.BatchId,
                        principalTable: "tbl_batch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_birdInventory_BatchId",
                table: "tbl_birdInventory",
                column: "BatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_dailyRegister_BatchId",
                table: "tbl_dailyRegister",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_feedInventory_BatchId",
                table: "tbl_feedInventory",
                column: "BatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_birdInventory");

            migrationBuilder.DropTable(
                name: "tbl_dailyRegister");

            migrationBuilder.DropTable(
                name: "tbl_feedInventory");

            migrationBuilder.DropTable(
                name: "tbl_batch");
        }
    }
}
