using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V122 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BannerBannerLocations_BannerLocations_Bl_ID",
                table: "BannerBannerLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_BannerBannerLocations_Banners_Bn_Id",
                table: "BannerBannerLocations");

            migrationBuilder.DropIndex(
                name: "IX_BannerBannerLocations_Bl_ID",
                table: "BannerBannerLocations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BannerBannerLocations_Bl_ID",
                table: "BannerBannerLocations",
                column: "Bl_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BannerBannerLocations_BannerLocations_Bl_ID",
                table: "BannerBannerLocations",
                column: "Bl_ID",
                principalTable: "BannerLocations",
                principalColumn: "Bl_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BannerBannerLocations_Banners_Bn_Id",
                table: "BannerBannerLocations",
                column: "Bn_Id",
                principalTable: "Banners",
                principalColumn: "Bn_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
