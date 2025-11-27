using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_stockOutEntry_tbl_stockOut_StockOutId",
                table: "tbl_stockOutEntry");

            migrationBuilder.RenameColumn(
                name: "StockOutId",
                table: "tbl_stockOutEntry",
                newName: "StockOutMasterId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_stockOutEntry_StockOutId",
                table: "tbl_stockOutEntry",
                newName: "IX_tbl_stockOutEntry_StockOutMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_stockOutEntry_tbl_stockOut_StockOutMasterId",
                table: "tbl_stockOutEntry",
                column: "StockOutMasterId",
                principalTable: "tbl_stockOut",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_stockOutEntry_tbl_stockOut_StockOutMasterId",
                table: "tbl_stockOutEntry");

            migrationBuilder.RenameColumn(
                name: "StockOutMasterId",
                table: "tbl_stockOutEntry",
                newName: "StockOutId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_stockOutEntry_StockOutMasterId",
                table: "tbl_stockOutEntry",
                newName: "IX_tbl_stockOutEntry_StockOutId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_stockOutEntry_tbl_stockOut_StockOutId",
                table: "tbl_stockOutEntry",
                column: "StockOutId",
                principalTable: "tbl_stockOut",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
