using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V115 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sb_DiscountDesc_Ar",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pm_Subscriber",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Pm_Subscriber",
                table: "Payments",
                column: "Pm_Subscriber");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_Pm_Subscriber",
                table: "Payments",
                column: "Pm_Subscriber",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_Pm_Subscriber",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_Pm_Subscriber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Sb_DiscountDesc_Ar",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Pm_Subscriber",
                table: "Payments");
        }
    }
}
