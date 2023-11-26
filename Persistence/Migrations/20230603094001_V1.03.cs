using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V103 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_AspNetUsers_Gn_Modifier",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_Gn_Modifier",
                table: "Genres");

            migrationBuilder.AlterColumn<string>(
                name: "Gn_Modifier",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifierId",
                table: "Genres",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_ModifierId",
                table: "Genres",
                column: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_AspNetUsers_ModifierId",
                table: "Genres",
                column: "ModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_AspNetUsers_ModifierId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_ModifierId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "Genres");

            migrationBuilder.AlterColumn<string>(
                name: "Gn_Modifier",
                table: "Genres",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Gn_Modifier",
                table: "Genres",
                column: "Gn_Modifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_AspNetUsers_Gn_Modifier",
                table: "Genres",
                column: "Gn_Modifier",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
