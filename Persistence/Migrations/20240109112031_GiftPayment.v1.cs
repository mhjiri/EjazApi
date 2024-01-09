using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GiftPaymentv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiftPayments_AspNetUsers_Pm_Subscriber",
                table: "GiftPayments");

            migrationBuilder.DropIndex(
                name: "IX_GiftPayments_Pm_Subscriber",
                table: "GiftPayments");

            migrationBuilder.DropColumn(
                name: "Pm_PayedBy",
                table: "GiftPayments");

            migrationBuilder.DropColumn(
                name: "Pm_Payer",
                table: "GiftPayments");

            migrationBuilder.DropColumn(
                name: "Pm_Subscriber",
                table: "GiftPayments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pm_PayedBy",
                table: "GiftPayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pm_Payer",
                table: "GiftPayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pm_Subscriber",
                table: "GiftPayments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiftPayments_Pm_Subscriber",
                table: "GiftPayments",
                column: "Pm_Subscriber");

            migrationBuilder.AddForeignKey(
                name: "FK_GiftPayments_AspNetUsers_Pm_Subscriber",
                table: "GiftPayments",
                column: "Pm_Subscriber",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
