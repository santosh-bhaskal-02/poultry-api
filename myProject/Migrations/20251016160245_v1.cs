using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myProject.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_dailyReport",
                table: "tbl_dailyReport");

            migrationBuilder.RenameTable(
                name: "tbl_dailyReport",
                newName: "tbl_dailyRegister");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_dailyRegister",
                table: "tbl_dailyRegister",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_dailyRegister",
                table: "tbl_dailyRegister");

            migrationBuilder.RenameTable(
                name: "tbl_dailyRegister",
                newName: "tbl_dailyReport");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_dailyReport",
                table: "tbl_dailyReport",
                column: "Id");
        }
    }
}
