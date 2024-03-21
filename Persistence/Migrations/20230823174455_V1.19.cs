using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V119 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCategories_Categories_Th_ID",
                table: "CustomerCategories");

            migrationBuilder.DropIndex(
                name: "IX_CustomerCategories_Th_ID",
                table: "CustomerCategories");

            migrationBuilder.DropColumn(
                name: "Th_ID",
                table: "CustomerCategories");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCategories_Ct_ID",
                table: "CustomerCategories",
                column: "Ct_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCategories_Categories_Ct_ID",
                table: "CustomerCategories",
                column: "Ct_ID",
                principalTable: "Categories",
                principalColumn: "Ct_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCategories_Categories_Ct_ID",
                table: "CustomerCategories");

            migrationBuilder.DropIndex(
                name: "IX_CustomerCategories_Ct_ID",
                table: "CustomerCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "Th_ID",
                table: "CustomerCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCategories_Th_ID",
                table: "CustomerCategories",
                column: "Th_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCategories_Categories_Th_ID",
                table: "CustomerCategories",
                column: "Th_ID",
                principalTable: "Categories",
                principalColumn: "Ct_ID");
        }
    }
}
