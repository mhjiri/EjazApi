using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V120 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gr_AgeFrom",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gr_AgeTill",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Gr_Gender",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gr_Language",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupCategory",
                columns: table => new
                {
                    Gr_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ct_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrCt_Ordinal = table.Column<int>(type: "int", nullable: false),
                    GrCt_Active = table.Column<bool>(type: "bit", nullable: false),
                    GrCt_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GrCt_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GrCt_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GrCt_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupCategory", x => new { x.Gr_ID, x.Ct_ID });
                    table.ForeignKey(
                        name: "FK_GroupCategory_AspNetUsers_GrCt_Creator",
                        column: x => x.GrCt_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupCategory_AspNetUsers_GrCt_Modifier",
                        column: x => x.GrCt_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupCategory_Categories_Ct_ID",
                        column: x => x.Ct_ID,
                        principalTable: "Categories",
                        principalColumn: "Ct_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupCategory_Groups_Gr_ID",
                        column: x => x.Gr_ID,
                        principalTable: "Groups",
                        principalColumn: "Gr_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupGenre",
                columns: table => new
                {
                    Gr_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gn_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrGn_Ordinal = table.Column<int>(type: "int", nullable: false),
                    GrGn_Active = table.Column<bool>(type: "bit", nullable: false),
                    GrGn_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GrGn_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GrGn_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GrGn_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupGenre", x => new { x.Gr_ID, x.Gn_ID });
                    table.ForeignKey(
                        name: "FK_GroupGenre_AspNetUsers_GrGn_Creator",
                        column: x => x.GrGn_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupGenre_AspNetUsers_GrGn_Modifier",
                        column: x => x.GrGn_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupGenre_Genres_Gn_ID",
                        column: x => x.Gn_ID,
                        principalTable: "Genres",
                        principalColumn: "Gn_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupGenre_Groups_Gr_ID",
                        column: x => x.Gr_ID,
                        principalTable: "Groups",
                        principalColumn: "Gr_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupTag",
                columns: table => new
                {
                    Gr_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tg_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrTg_Ordinal = table.Column<int>(type: "int", nullable: false),
                    GrTg_Active = table.Column<bool>(type: "bit", nullable: false),
                    GrTg_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GrTg_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GrTg_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GrTg_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTag", x => new { x.Gr_ID, x.Tg_ID });
                    table.ForeignKey(
                        name: "FK_GroupTag_AspNetUsers_GrTg_Creator",
                        column: x => x.GrTg_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupTag_AspNetUsers_GrTg_Modifier",
                        column: x => x.GrTg_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupTag_Groups_Gr_ID",
                        column: x => x.Gr_ID,
                        principalTable: "Groups",
                        principalColumn: "Gr_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupTag_Tags_Tg_ID",
                        column: x => x.Tg_ID,
                        principalTable: "Tags",
                        principalColumn: "Tg_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupThematicArea",
                columns: table => new
                {
                    Gr_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Th_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrTh_Ordinal = table.Column<int>(type: "int", nullable: false),
                    GrTh_Active = table.Column<bool>(type: "bit", nullable: false),
                    GrTh_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GrTh_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GrTh_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GrTh_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupThematicArea", x => new { x.Gr_ID, x.Th_ID });
                    table.ForeignKey(
                        name: "FK_GroupThematicArea_AspNetUsers_GrTh_Creator",
                        column: x => x.GrTh_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupThematicArea_AspNetUsers_GrTh_Modifier",
                        column: x => x.GrTh_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupThematicArea_Groups_Gr_ID",
                        column: x => x.Gr_ID,
                        principalTable: "Groups",
                        principalColumn: "Gr_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupThematicArea_ThematicAreas_Th_ID",
                        column: x => x.Th_ID,
                        principalTable: "ThematicAreas",
                        principalColumn: "Th_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupCategory_Ct_ID",
                table: "GroupCategory",
                column: "Ct_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCategory_GrCt_Creator",
                table: "GroupCategory",
                column: "GrCt_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCategory_GrCt_Modifier",
                table: "GroupCategory",
                column: "GrCt_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_GroupGenre_Gn_ID",
                table: "GroupGenre",
                column: "Gn_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupGenre_GrGn_Creator",
                table: "GroupGenre",
                column: "GrGn_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_GroupGenre_GrGn_Modifier",
                table: "GroupGenre",
                column: "GrGn_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTag_GrTg_Creator",
                table: "GroupTag",
                column: "GrTg_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTag_GrTg_Modifier",
                table: "GroupTag",
                column: "GrTg_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTag_Tg_ID",
                table: "GroupTag",
                column: "Tg_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupThematicArea_GrTh_Creator",
                table: "GroupThematicArea",
                column: "GrTh_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_GroupThematicArea_GrTh_Modifier",
                table: "GroupThematicArea",
                column: "GrTh_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_GroupThematicArea_Th_ID",
                table: "GroupThematicArea",
                column: "Th_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupCategory");

            migrationBuilder.DropTable(
                name: "GroupGenre");

            migrationBuilder.DropTable(
                name: "GroupTag");

            migrationBuilder.DropTable(
                name: "GroupThematicArea");

            migrationBuilder.DropColumn(
                name: "Gr_AgeFrom",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Gr_AgeTill",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Gr_Gender",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Gr_Language",
                table: "Groups");
        }
    }
}
