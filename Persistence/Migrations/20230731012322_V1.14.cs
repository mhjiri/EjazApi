using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V114 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_AspNetUsers_Pm_Creator",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_PaymentMethods_Py_ID",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Subscriptions_Sb_ID",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_Sb_ID",
                table: "Payments",
                newName: "IX_Payments_Sb_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_Py_ID",
                table: "Payments",
                newName: "IX_Payments_Py_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_Pm_Creator",
                table: "Payments",
                newName: "IX_Payments_Pm_Creator");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Pm_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_Pm_Creator",
                table: "Payments",
                column: "Pm_Creator",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentMethods_Py_ID",
                table: "Payments",
                column: "Py_ID",
                principalTable: "PaymentMethods",
                principalColumn: "Py_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Subscriptions_Sb_ID",
                table: "Payments",
                column: "Sb_ID",
                principalTable: "Subscriptions",
                principalColumn: "Sb_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_Pm_Creator",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentMethods_Py_ID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Subscriptions_Sb_ID",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_Sb_ID",
                table: "Payment",
                newName: "IX_Payment_Sb_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_Py_ID",
                table: "Payment",
                newName: "IX_Payment_Py_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_Pm_Creator",
                table: "Payment",
                newName: "IX_Payment_Pm_Creator");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "Pm_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_AspNetUsers_Pm_Creator",
                table: "Payment",
                column: "Pm_Creator",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_PaymentMethods_Py_ID",
                table: "Payment",
                column: "Py_ID",
                principalTable: "PaymentMethods",
                principalColumn: "Py_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Subscriptions_Sb_ID",
                table: "Payment",
                column: "Sb_ID",
                principalTable: "Subscriptions",
                principalColumn: "Sb_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
