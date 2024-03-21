using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V109 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookCollections",
                columns: table => new
                {
                    Bc_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Md_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bc_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bc_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bc_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bc_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bc_Active = table.Column<bool>(type: "bit", nullable: false),
                    Bc_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bc_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Bc_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Bc_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCollections", x => x.Bc_ID);
                    table.ForeignKey(
                        name: "FK_BookCollections_AspNetUsers_Bc_Creator",
                        column: x => x.Bc_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookCollections_AspNetUsers_Bc_Modifier",
                        column: x => x.Bc_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookBookCollections",
                columns: table => new
                {
                    Bk_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bc_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BkBc_Ordinal = table.Column<int>(type: "int", nullable: false),
                    BkBc_Active = table.Column<bool>(type: "bit", nullable: false),
                    BkBc_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BkBc_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BkBc_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BkBc_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBookCollections", x => new { x.Bk_ID, x.Bc_ID });
                    table.ForeignKey(
                        name: "FK_BookBookCollections_AspNetUsers_BkBc_Creator",
                        column: x => x.BkBc_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookBookCollections_AspNetUsers_BkBc_Modifier",
                        column: x => x.BkBc_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookBookCollections_BookCollections_Bc_ID",
                        column: x => x.Bc_ID,
                        principalTable: "BookCollections",
                        principalColumn: "Bc_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookBookCollections_Books_Bk_ID",
                        column: x => x.Bk_ID,
                        principalTable: "Books",
                        principalColumn: "Bk_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookBookCollections_Bc_ID",
                table: "BookBookCollections",
                column: "Bc_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BookBookCollections_BkBc_Creator",
                table: "BookBookCollections",
                column: "BkBc_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_BookBookCollections_BkBc_Modifier",
                table: "BookBookCollections",
                column: "BkBc_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_BookCollections_Bc_Creator",
                table: "BookCollections",
                column: "Bc_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_BookCollections_Bc_Modifier",
                table: "BookCollections",
                column: "Bc_Modifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookBookCollections");

            migrationBuilder.DropTable(
                name: "BookCollections");
        }
    }
}
