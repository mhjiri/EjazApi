using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V121 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banners_Media_Md_ID",
                table: "Banners");

            migrationBuilder.DropIndex(
                name: "IX_Banners_Md_ID",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Bn_Name",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Bn_Name_Ar",
                table: "Banners");

            migrationBuilder.AddColumn<Guid>(
                name: "Bl_ID",
                table: "Banners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Gr_ID",
                table: "Banners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Bl_Ratio",
                table: "BannerLocations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Banners_Bl_ID",
                table: "Banners",
                column: "Bl_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Banners_Gr_ID",
                table: "Banners",
                column: "Gr_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_BannerLocations_Bl_ID",
                table: "Banners",
                column: "Bl_ID",
                principalTable: "BannerLocations",
                principalColumn: "Bl_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_Groups_Gr_ID",
                table: "Banners",
                column: "Gr_ID",
                principalTable: "Groups",
                principalColumn: "Gr_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banners_BannerLocations_Bl_ID",
                table: "Banners");

            migrationBuilder.DropForeignKey(
                name: "FK_Banners_Groups_Gr_ID",
                table: "Banners");

            migrationBuilder.DropIndex(
                name: "IX_Banners_Bl_ID",
                table: "Banners");

            migrationBuilder.DropIndex(
                name: "IX_Banners_Gr_ID",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Bl_ID",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Gr_ID",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Bl_Ratio",
                table: "BannerLocations");

            migrationBuilder.AddColumn<string>(
                name: "Bn_Name",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bn_Name_Ar",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Banners_Md_ID",
                table: "Banners",
                column: "Md_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_Media_Md_ID",
                table: "Banners",
                column: "Md_ID",
                principalTable: "Media",
                principalColumn: "Md_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
