using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V126cs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bk_LastViewedBy",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Bk_LastViewedBy",
                table: "Books",
                column: "Bk_LastViewedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_Bk_LastViewedBy",
                table: "Books",
                column: "Bk_LastViewedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_Bk_LastViewedBy",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_Bk_LastViewedBy",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Bk_LastViewedBy",
                table: "Books");
        }
    }
}
