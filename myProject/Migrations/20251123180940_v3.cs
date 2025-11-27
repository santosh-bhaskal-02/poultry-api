using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyProject.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_stockOut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BatchId = table.Column<int>(type: "integer", nullable: false),
                    StockOutNo = table.Column<int>(type: "integer", nullable: false),
                    TotalBirds = table.Column<int>(type: "integer", nullable: false),
                    TotalWeight = table.Column<decimal>(type: "numeric", nullable: false),
                    AvgWeight = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_stockOut", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_stockOut_tbl_batch_BatchId",
                        column: x => x.BatchId,
                        principalTable: "tbl_batch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_stockOutEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SrNo = table.Column<int>(type: "integer", nullable: false),
                    Birds = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false),
                    StockOutId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_stockOutEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_stockOutEntry_tbl_stockOut_StockOutId",
                        column: x => x.StockOutId,
                        principalTable: "tbl_stockOut",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_stockOut_BatchId",
                table: "tbl_stockOut",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_stockOutEntry_StockOutId",
                table: "tbl_stockOutEntry",
                column: "StockOutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_stockOutEntry");

            migrationBuilder.DropTable(
                name: "tbl_stockOut");
        }
    }
}
