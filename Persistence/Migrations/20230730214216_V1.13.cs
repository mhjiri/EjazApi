using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V113 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sb_Days",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Sb_DiscountDesc",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Sb_DisplayPrice",
                table: "Subscriptions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Pm_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Py_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sb_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pm_RefernceID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pm_DisplayPrice = table.Column<double>(type: "float", nullable: false),
                    Pm_Days = table.Column<int>(type: "int", nullable: false),
                    Pm_Price = table.Column<double>(type: "float", nullable: false),
                    Pm_Result = table.Column<int>(type: "int", nullable: false),
                    Pm_Ordinal = table.Column<int>(type: "int", nullable: false),
                    Pm_Active = table.Column<bool>(type: "bit", nullable: false),
                    Pm_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pm_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Pm_ID);
                    table.ForeignKey(
                        name: "FK_Payment_AspNetUsers_Pm_Creator",
                        column: x => x.Pm_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payment_PaymentMethods_Py_ID",
                        column: x => x.Py_ID,
                        principalTable: "PaymentMethods",
                        principalColumn: "Py_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_Subscriptions_Sb_ID",
                        column: x => x.Sb_ID,
                        principalTable: "Subscriptions",
                        principalColumn: "Sb_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Pm_Creator",
                table: "Payment",
                column: "Pm_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Py_ID",
                table: "Payment",
                column: "Py_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Sb_ID",
                table: "Payment",
                column: "Sb_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropColumn(
                name: "Sb_Days",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Sb_DiscountDesc",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Sb_DisplayPrice",
                table: "Subscriptions");
        }
    }
}
