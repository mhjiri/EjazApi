using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GiftPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Us_SubscriptionDays",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "GiftPayments",
                columns: table => new
                {
                    Pm_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Py_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sb_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pm_Payer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pm_PayedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pm_RefernceID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pm_DisplayPrice = table.Column<double>(type: "float", nullable: false),
                    Pm_Days = table.Column<int>(type: "int", nullable: false),
                    Pm_Price = table.Column<double>(type: "float", nullable: false),
                    Pm_Result = table.Column<int>(type: "int", nullable: false),
                    Pm_Ordinal = table.Column<int>(type: "int", nullable: false),
                    PM_Recipient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PM_CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PM_GiftedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pm_Active = table.Column<bool>(type: "bit", nullable: false),
                    PM_Used = table.Column<bool>(type: "bit", nullable: false),
                    Pm_Subscriber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Pm_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftPayments", x => x.Pm_ID);
                    table.ForeignKey(
                        name: "FK_GiftPayments_AspNetUsers_Pm_Creator",
                        column: x => x.Pm_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GiftPayments_AspNetUsers_Pm_Subscriber",
                        column: x => x.Pm_Subscriber,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GiftPayments_PaymentMethods_Py_ID",
                        column: x => x.Py_ID,
                        principalTable: "PaymentMethods",
                        principalColumn: "Py_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GiftPayments_Subscriptions_Sb_ID",
                        column: x => x.Sb_ID,
                        principalTable: "Subscriptions",
                        principalColumn: "Sb_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GiftPayments_Pm_Creator",
                table: "GiftPayments",
                column: "Pm_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_GiftPayments_Pm_Subscriber",
                table: "GiftPayments",
                column: "Pm_Subscriber");

            migrationBuilder.CreateIndex(
                name: "IX_GiftPayments_Py_ID",
                table: "GiftPayments",
                column: "Py_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GiftPayments_Sb_ID",
                table: "GiftPayments",
                column: "Sb_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiftPayments");

            migrationBuilder.AlterColumn<int>(
                name: "Us_SubscriptionDays",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
