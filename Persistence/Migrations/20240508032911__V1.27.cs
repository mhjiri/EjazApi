using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _V127 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "Md_URL",
            //    table: "Media",
            //    type: "nvarchar(max)",
            //    nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Md_AudioAr_ID",
                table: "Books",
                column: "Md_AudioAr_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Md_AudioEn_ID",
                table: "Books",
                column: "Md_AudioEn_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Media_Md_AudioAr_ID",
                table: "Books",
                column: "Md_AudioAr_ID",
                principalTable: "Media",
                principalColumn: "Md_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Media_Md_AudioEn_ID",
                table: "Books",
                column: "Md_AudioEn_ID",
                principalTable: "Media",
                principalColumn: "Md_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Media_Md_AudioAr_ID",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Media_Md_AudioEn_ID",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_Md_AudioAr_ID",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_Md_AudioEn_ID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Md_URL",
                table: "Media");
        }
    }
}
