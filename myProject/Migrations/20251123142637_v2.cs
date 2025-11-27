using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyProject.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_finalReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BatchId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalBirds = table.Column<int>(type: "integer", nullable: false),
                    BirdRate = table.Column<decimal>(type: "numeric", nullable: false),
                    BirdCost = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalBags = table.Column<int>(type: "integer", nullable: false),
                    TotalFeedKg = table.Column<decimal>(type: "numeric", nullable: false),
                    FeedRate = table.Column<decimal>(type: "numeric", nullable: false),
                    FeedCost = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalSoldBirds = table.Column<int>(type: "integer", nullable: false),
                    TotalBirdsWeight = table.Column<decimal>(type: "numeric", nullable: false),
                    AvgBodyWeight = table.Column<decimal>(type: "numeric", nullable: false),
                    ChicksCost = table.Column<decimal>(type: "numeric", nullable: false),
                    MedicineCost = table.Column<decimal>(type: "numeric", nullable: false),
                    AdminCost = table.Column<decimal>(type: "numeric", nullable: false),
                    GrossProductionCost = table.Column<decimal>(type: "numeric", nullable: false),
                    NetCostPerKg = table.Column<decimal>(type: "numeric", nullable: false),
                    StdCostPerKg = table.Column<decimal>(type: "numeric", nullable: false),
                    RearingChargesStd = table.Column<decimal>(type: "numeric", nullable: false),
                    ProductionCostIncentive = table.Column<decimal>(type: "numeric", nullable: false),
                    RearingChargesPerBird = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalRearingCharges = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalAmountPayable = table.Column<decimal>(type: "numeric", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_finalReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_finalReport_tbl_batch_BatchId",
                        column: x => x.BatchId,
                        principalTable: "tbl_batch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_finalReport_BatchId",
                table: "tbl_finalReport",
                column: "BatchId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_finalReport");
        }
    }
}
