using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V116 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerThematicAreas_AspNetUsers_CsTh_Creator",
                table: "CustomerThematicAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerThematicAreas_AspNetUsers_CsTh_Modifier",
                table: "CustomerThematicAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerThematicAreas_AspNetUsers_Cs_ID",
                table: "CustomerThematicAreas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerThematicAreas",
                table: "CustomerThematicAreas");

            migrationBuilder.DropIndex(
                name: "IX_CustomerThematicAreas_CsTh_Creator",
                table: "CustomerThematicAreas");

            migrationBuilder.DropIndex(
                name: "IX_CustomerThematicAreas_CsTh_Modifier",
                table: "CustomerThematicAreas");

            migrationBuilder.AlterColumn<string>(
                name: "CsTh_Modifier",
                table: "CustomerThematicAreas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsTh_Creator",
                table: "CustomerThematicAreas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cs_ID",
                table: "CustomerThematicAreas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "CsTh_ID",
                table: "CustomerThematicAreas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerThematicAreas",
                table: "CustomerThematicAreas",
                column: "CsTh_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerThematicAreas_Cs_ID",
                table: "CustomerThematicAreas",
                column: "Cs_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerThematicAreas_AspNetUsers_Cs_ID",
                table: "CustomerThematicAreas",
                column: "Cs_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerThematicAreas_AspNetUsers_Cs_ID",
                table: "CustomerThematicAreas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerThematicAreas",
                table: "CustomerThematicAreas");

            migrationBuilder.DropIndex(
                name: "IX_CustomerThematicAreas_Cs_ID",
                table: "CustomerThematicAreas");

            migrationBuilder.DropColumn(
                name: "CsTh_ID",
                table: "CustomerThematicAreas");

            migrationBuilder.AlterColumn<string>(
                name: "Cs_ID",
                table: "CustomerThematicAreas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsTh_Modifier",
                table: "CustomerThematicAreas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsTh_Creator",
                table: "CustomerThematicAreas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerThematicAreas",
                table: "CustomerThematicAreas",
                columns: new[] { "Cs_ID", "Th_ID" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerThematicAreas_CsTh_Creator",
                table: "CustomerThematicAreas",
                column: "CsTh_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerThematicAreas_CsTh_Modifier",
                table: "CustomerThematicAreas",
                column: "CsTh_Modifier");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerThematicAreas_AspNetUsers_CsTh_Creator",
                table: "CustomerThematicAreas",
                column: "CsTh_Creator",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerThematicAreas_AspNetUsers_CsTh_Modifier",
                table: "CustomerThematicAreas",
                column: "CsTh_Modifier",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerThematicAreas_AspNetUsers_Cs_ID",
                table: "CustomerThematicAreas",
                column: "Cs_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
