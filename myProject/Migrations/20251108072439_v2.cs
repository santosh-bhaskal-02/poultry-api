using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecordDate",
                table: "tbl_feedInventory",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "RecordDate",
                table: "tbl_dailyRegister",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "RecordDate",
                table: "tbl_birdInventory",
                newName: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "tbl_feedInventory",
                newName: "RecordDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "tbl_dailyRegister",
                newName: "RecordDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "tbl_birdInventory",
                newName: "RecordDate");
        }
    }
}
