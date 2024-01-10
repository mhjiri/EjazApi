using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SuggestBookV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SugggestBook",
                columns: table => new
                {
                    Bk_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bk_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bk_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bk_Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bk_Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bk_Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bk_Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bk_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bk_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SugggestBook", x => x.Bk_ID);
                    table.ForeignKey(
                        name: "FK_SugggestBook_AspNetUsers_Bk_Creator",
                        column: x => x.Bk_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SugggestBook_Bk_Creator",
                table: "SugggestBook",
                column: "Bk_Creator");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SugggestBook");
        }
    }
}
