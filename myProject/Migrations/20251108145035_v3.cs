using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_dailyRegister_tbl_birdInventory_BirdInventoryId",
                table: "tbl_dailyRegister");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_feedInventory_tbl_birdInventory_BirdInventoryId",
                table: "tbl_feedInventory");

            migrationBuilder.DropIndex(
                name: "IX_tbl_feedInventory_BirdInventoryId",
                table: "tbl_feedInventory");

            migrationBuilder.DropIndex(
                name: "IX_tbl_dailyRegister_BirdInventoryId",
                table: "tbl_dailyRegister");

            migrationBuilder.DropColumn(
                name: "BirdInventoryId",
                table: "tbl_feedInventory");

            migrationBuilder.DropColumn(
                name: "BirdInventoryId",
                table: "tbl_dailyRegister");

            migrationBuilder.RenameColumn(
                name: "BatchId",
                table: "tbl_feedInventory",
                newName: "BatchNo");

            migrationBuilder.RenameColumn(
                name: "BatchId",
                table: "tbl_dailyRegister",
                newName: "BatchNo");

            migrationBuilder.AddColumn<int>(
                name: "BatchNo",
                table: "tbl_birdInventory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_tbl_birdInventory_BatchNo",
                table: "tbl_birdInventory",
                column: "BatchNo");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_feedInventory_BatchNo",
                table: "tbl_feedInventory",
                column: "BatchNo");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_dailyRegister_BatchNo",
                table: "tbl_dailyRegister",
                column: "BatchNo");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_birdInventory_BatchNo",
                table: "tbl_birdInventory",
                column: "BatchNo",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_dailyRegister_tbl_birdInventory_BatchNo",
                table: "tbl_dailyRegister",
                column: "BatchNo",
                principalTable: "tbl_birdInventory",
                principalColumn: "BatchNo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_feedInventory_tbl_birdInventory_BatchNo",
                table: "tbl_feedInventory",
                column: "BatchNo",
                principalTable: "tbl_birdInventory",
                principalColumn: "BatchNo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_dailyRegister_tbl_birdInventory_BatchNo",
                table: "tbl_dailyRegister");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_feedInventory_tbl_birdInventory_BatchNo",
                table: "tbl_feedInventory");

            migrationBuilder.DropIndex(
                name: "IX_tbl_feedInventory_BatchNo",
                table: "tbl_feedInventory");

            migrationBuilder.DropIndex(
                name: "IX_tbl_dailyRegister_BatchNo",
                table: "tbl_dailyRegister");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_tbl_birdInventory_BatchNo",
                table: "tbl_birdInventory");

            migrationBuilder.DropIndex(
                name: "IX_tbl_birdInventory_BatchNo",
                table: "tbl_birdInventory");

            migrationBuilder.DropColumn(
                name: "BatchNo",
                table: "tbl_birdInventory");

            migrationBuilder.RenameColumn(
                name: "BatchNo",
                table: "tbl_feedInventory",
                newName: "BatchId");

            migrationBuilder.RenameColumn(
                name: "BatchNo",
                table: "tbl_dailyRegister",
                newName: "BatchId");

            migrationBuilder.AddColumn<int>(
                name: "BirdInventoryId",
                table: "tbl_feedInventory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BirdInventoryId",
                table: "tbl_dailyRegister",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_feedInventory_BirdInventoryId",
                table: "tbl_feedInventory",
                column: "BirdInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_dailyRegister_BirdInventoryId",
                table: "tbl_dailyRegister",
                column: "BirdInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_dailyRegister_tbl_birdInventory_BirdInventoryId",
                table: "tbl_dailyRegister",
                column: "BirdInventoryId",
                principalTable: "tbl_birdInventory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_feedInventory_tbl_birdInventory_BirdInventoryId",
                table: "tbl_feedInventory",
                column: "BirdInventoryId",
                principalTable: "tbl_birdInventory",
                principalColumn: "Id");
        }
    }
}
