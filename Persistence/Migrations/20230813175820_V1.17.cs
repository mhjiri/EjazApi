using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V117 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCategories_AspNetUsers_CsCt_Creator",
                table: "CustomerCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCategories_AspNetUsers_CsCt_Modifier",
                table: "CustomerCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGenres_AspNetUsers_CsGn_Creator",
                table: "CustomerGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGenres_AspNetUsers_CsGn_Modifier",
                table: "CustomerGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroups_AspNetUsers_CsGr_Creator",
                table: "CustomerGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroups_AspNetUsers_CsGr_Modifier",
                table: "CustomerGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerRewards_AspNetUsers_CsRw_Creator",
                table: "CustomerRewards");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerRewards_AspNetUsers_CsRw_Modifier",
                table: "CustomerRewards");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTags_AspNetUsers_CsTg_Creator",
                table: "CustomerTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTags_AspNetUsers_CsTg_Modifier",
                table: "CustomerTags");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTags_CsTg_Creator",
                table: "CustomerTags");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTags_CsTg_Modifier",
                table: "CustomerTags");

            migrationBuilder.DropIndex(
                name: "IX_CustomerRewards_CsRw_Creator",
                table: "CustomerRewards");

            migrationBuilder.DropIndex(
                name: "IX_CustomerRewards_CsRw_Modifier",
                table: "CustomerRewards");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroups_CsGr_Creator",
                table: "CustomerGroups");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroups_CsGr_Modifier",
                table: "CustomerGroups");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGenres_CsGn_Creator",
                table: "CustomerGenres");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGenres_CsGn_Modifier",
                table: "CustomerGenres");

            migrationBuilder.DropIndex(
                name: "IX_CustomerCategories_CsCt_Creator",
                table: "CustomerCategories");

            migrationBuilder.DropIndex(
                name: "IX_CustomerCategories_CsCt_Modifier",
                table: "CustomerCategories");

            migrationBuilder.AlterColumn<string>(
                name: "CsTg_Modifier",
                table: "CustomerTags",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsTg_Creator",
                table: "CustomerTags",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsRw_Modifier",
                table: "CustomerRewards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsRw_Creator",
                table: "CustomerRewards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsGr_Modifier",
                table: "CustomerGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsGr_Creator",
                table: "CustomerGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsGn_Modifier",
                table: "CustomerGenres",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsGn_Creator",
                table: "CustomerGenres",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsCt_Modifier",
                table: "CustomerCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsCt_Creator",
                table: "CustomerCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CsTg_Modifier",
                table: "CustomerTags",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsTg_Creator",
                table: "CustomerTags",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsRw_Modifier",
                table: "CustomerRewards",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsRw_Creator",
                table: "CustomerRewards",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsGr_Modifier",
                table: "CustomerGroups",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsGr_Creator",
                table: "CustomerGroups",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsGn_Modifier",
                table: "CustomerGenres",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsGn_Creator",
                table: "CustomerGenres",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsCt_Modifier",
                table: "CustomerCategories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CsCt_Creator",
                table: "CustomerCategories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTags_CsTg_Creator",
                table: "CustomerTags",
                column: "CsTg_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTags_CsTg_Modifier",
                table: "CustomerTags",
                column: "CsTg_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRewards_CsRw_Creator",
                table: "CustomerRewards",
                column: "CsRw_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRewards_CsRw_Modifier",
                table: "CustomerRewards",
                column: "CsRw_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroups_CsGr_Creator",
                table: "CustomerGroups",
                column: "CsGr_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroups_CsGr_Modifier",
                table: "CustomerGroups",
                column: "CsGr_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGenres_CsGn_Creator",
                table: "CustomerGenres",
                column: "CsGn_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGenres_CsGn_Modifier",
                table: "CustomerGenres",
                column: "CsGn_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCategories_CsCt_Creator",
                table: "CustomerCategories",
                column: "CsCt_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCategories_CsCt_Modifier",
                table: "CustomerCategories",
                column: "CsCt_Modifier");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCategories_AspNetUsers_CsCt_Creator",
                table: "CustomerCategories",
                column: "CsCt_Creator",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCategories_AspNetUsers_CsCt_Modifier",
                table: "CustomerCategories",
                column: "CsCt_Modifier",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGenres_AspNetUsers_CsGn_Creator",
                table: "CustomerGenres",
                column: "CsGn_Creator",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGenres_AspNetUsers_CsGn_Modifier",
                table: "CustomerGenres",
                column: "CsGn_Modifier",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroups_AspNetUsers_CsGr_Creator",
                table: "CustomerGroups",
                column: "CsGr_Creator",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroups_AspNetUsers_CsGr_Modifier",
                table: "CustomerGroups",
                column: "CsGr_Modifier",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerRewards_AspNetUsers_CsRw_Creator",
                table: "CustomerRewards",
                column: "CsRw_Creator",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerRewards_AspNetUsers_CsRw_Modifier",
                table: "CustomerRewards",
                column: "CsRw_Modifier",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTags_AspNetUsers_CsTg_Creator",
                table: "CustomerTags",
                column: "CsTg_Creator",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTags_AspNetUsers_CsTg_Modifier",
                table: "CustomerTags",
                column: "CsTg_Modifier",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
