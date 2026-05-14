using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyProject.Migrations
{
    /// <inheritdoc />
    public partial class _10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "tbl_batch",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tbl_user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_user", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_batch_UserId",
                table: "tbl_batch",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_batch_tbl_user_UserId",
                table: "tbl_batch",
                column: "UserId",
                principalTable: "tbl_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_batch_tbl_user_UserId",
                table: "tbl_batch");

            migrationBuilder.DropTable(
                name: "tbl_user");

            migrationBuilder.DropIndex(
                name: "IX_tbl_batch_UserId",
                table: "tbl_batch");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tbl_batch");
        }
    }
}
