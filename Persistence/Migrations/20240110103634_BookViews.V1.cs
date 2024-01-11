using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BookViewsV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Bk_LastViewedOn",
                table: "Books",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bk_ViewCount",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bk_LastViewedOn",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Bk_ViewCount",
                table: "Books");
        }
    }
}
