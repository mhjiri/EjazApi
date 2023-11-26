using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Cn_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Md_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Us_DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Us_DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Us_Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Us_language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Us_FirebaseUID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Us_Active = table.Column<bool>(type: "bit", nullable: false),
                    Us_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Us_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Us_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Us_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_Us_Creator",
                        column: x => x.Us_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_Us_Modifier",
                        column: x => x.Us_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementChannels",
                columns: table => new
                {
                    AnCh_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnCh_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnCh_Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnCh_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnCh_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnCh_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnCh_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnCh_Active = table.Column<bool>(type: "bit", nullable: false),
                    AnCh_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnCh_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AnCh_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnCh_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementChannels", x => x.AnCh_ID);
                    table.ForeignKey(
                        name: "FK_AnnouncementChannels_AspNetUsers_AnCh_Creator",
                        column: x => x.AnCh_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnnouncementChannels_AspNetUsers_AnCh_Modifier",
                        column: x => x.AnCh_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementPriorities",
                columns: table => new
                {
                    AnPr_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnPr_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnPr_Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnPr_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnPr_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnPr_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnPr_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnPr_Weight = table.Column<int>(type: "int", nullable: false),
                    AnPr_Active = table.Column<bool>(type: "bit", nullable: false),
                    AnPr_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnPr_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AnPr_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnPr_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementPriorities", x => x.AnPr_ID);
                    table.ForeignKey(
                        name: "FK_AnnouncementPriorities_AspNetUsers_AnPr_Creator",
                        column: x => x.AnPr_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnnouncementPriorities_AspNetUsers_AnPr_Modifier",
                        column: x => x.AnPr_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Av_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Md_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Av_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Av_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Av_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Av_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Av_Active = table.Column<bool>(type: "bit", nullable: false),
                    Av_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Av_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Av_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Av_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Av_ID);
                    table.ForeignKey(
                        name: "FK_Avatars_AspNetUsers_Av_Creator",
                        column: x => x.Av_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Avatars_AspNetUsers_Av_Modifier",
                        column: x => x.Av_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BannerLocations",
                columns: table => new
                {
                    Bl_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bl_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bl_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bl_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bl_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bl_Active = table.Column<bool>(type: "bit", nullable: false),
                    Bl_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bl_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Bl_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Bl_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerLocations", x => x.Bl_ID);
                    table.ForeignKey(
                        name: "FK_BannerLocations_AspNetUsers_Bl_Creator",
                        column: x => x.Bl_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BannerLocations_AspNetUsers_Bl_Modifier",
                        column: x => x.Bl_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Bk_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Md_AudioEn_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Md_AudioAr_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Bk_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bk_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bk_Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bk_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bk_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bk_Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bk_Summary_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bk_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bk_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bk_Active = table.Column<bool>(type: "bit", nullable: false),
                    Bk_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bk_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Bk_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Bk_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Bk_ID);
                    table.ForeignKey(
                        name: "FK_Books_AspNetUsers_Bk_Creator",
                        column: x => x.Bk_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Books_AspNetUsers_Bk_Modifier",
                        column: x => x.Bk_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Gn_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gn_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gn_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gn_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gn_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gn_Active = table.Column<bool>(type: "bit", nullable: false),
                    Gn_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gn_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Gn_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gn_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Gn_ID);
                    table.ForeignKey(
                        name: "FK_Genres_AspNetUsers_Gn_Creator",
                        column: x => x.Gn_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Genres_AspNetUsers_Gn_Modifier",
                        column: x => x.Gn_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Gr_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gr_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gr_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gr_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gr_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gr_Active = table.Column<bool>(type: "bit", nullable: false),
                    Gr_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gr_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Gr_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gr_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Gr_ID);
                    table.ForeignKey(
                        name: "FK_Groups_AspNetUsers_Gr_Creator",
                        column: x => x.Gr_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Groups_AspNetUsers_Gr_Modifier",
                        column: x => x.Gr_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Py_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Py_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Py_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Py_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Py_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Py_Active = table.Column<bool>(type: "bit", nullable: false),
                    Py_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Py_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Py_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Py_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Py_ID);
                    table.ForeignKey(
                        name: "FK_PaymentMethods_AspNetUsers_Py_Creator",
                        column: x => x.Py_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentMethods_AspNetUsers_Py_Modifier",
                        column: x => x.Py_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Tg_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tg_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tg_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tg_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tg_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tg_Active = table.Column<bool>(type: "bit", nullable: false),
                    Tg_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tg_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Tg_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tg_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Tg_ID);
                    table.ForeignKey(
                        name: "FK_Tags_AspNetUsers_Tg_Creator",
                        column: x => x.Tg_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tags_AspNetUsers_Tg_Modifier",
                        column: x => x.Tg_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ThematicAreas",
                columns: table => new
                {
                    Th_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Th_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Th_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Th_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Th_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Th_Active = table.Column<bool>(type: "bit", nullable: false),
                    Th_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Th_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Th_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Th_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThematicAreas", x => x.Th_ID);
                    table.ForeignKey(
                        name: "FK_ThematicAreas_AspNetUsers_Th_Creator",
                        column: x => x.Th_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ThematicAreas_AspNetUsers_Th_Modifier",
                        column: x => x.Th_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Md_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bk_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Md_Medium = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Md_FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Md_FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Md_Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Md_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Md_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Md_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Md_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Md_Ordinal = table.Column<int>(type: "int", nullable: false),
                    Md_Active = table.Column<bool>(type: "bit", nullable: false),
                    Md_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Md_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Md_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Md_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Md_ID);
                    table.ForeignKey(
                        name: "FK_Media_AspNetUsers_Md_Creator",
                        column: x => x.Md_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Media_AspNetUsers_Md_Modifier",
                        column: x => x.Md_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Media_Books_Bk_ID",
                        column: x => x.Bk_ID,
                        principalTable: "Books",
                        principalColumn: "Bk_ID");
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    Bk_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gn_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BkGn_Ordinal = table.Column<int>(type: "int", nullable: false),
                    BkGn_Active = table.Column<bool>(type: "bit", nullable: false),
                    BkGn_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BkGn_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BkGn_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BkGn_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => new { x.Bk_ID, x.Gn_ID });
                    table.ForeignKey(
                        name: "FK_BookGenres_AspNetUsers_BkGn_Creator",
                        column: x => x.BkGn_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookGenres_AspNetUsers_BkGn_Modifier",
                        column: x => x.BkGn_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookGenres_Books_Bk_ID",
                        column: x => x.Bk_ID,
                        principalTable: "Books",
                        principalColumn: "Bk_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookGenres_Genres_Gn_ID",
                        column: x => x.Gn_ID,
                        principalTable: "Genres",
                        principalColumn: "Gn_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerGenres",
                columns: table => new
                {
                    Cs_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gn_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CsGn_Ordinal = table.Column<int>(type: "int", nullable: false),
                    CsGn_Active = table.Column<bool>(type: "bit", nullable: false),
                    CsGn_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CsGn_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CsGn_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CsGn_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGenres", x => new { x.Cs_ID, x.Gn_ID });
                    table.ForeignKey(
                        name: "FK_CustomerGenres_AspNetUsers_CsGn_Creator",
                        column: x => x.CsGn_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGenres_AspNetUsers_CsGn_Modifier",
                        column: x => x.CsGn_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGenres_AspNetUsers_Cs_ID",
                        column: x => x.Cs_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerGenres_Genres_Gn_ID",
                        column: x => x.Gn_ID,
                        principalTable: "Genres",
                        principalColumn: "Gn_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerGroups",
                columns: table => new
                {
                    Cs_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gr_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CsGr_Ordinal = table.Column<int>(type: "int", nullable: false),
                    CsGr_Active = table.Column<bool>(type: "bit", nullable: false),
                    CsGr_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CsGr_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CsGr_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CsGr_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGroups", x => new { x.Cs_ID, x.Gr_ID });
                    table.ForeignKey(
                        name: "FK_CustomerGroups_AspNetUsers_CsGr_Creator",
                        column: x => x.CsGr_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroups_AspNetUsers_CsGr_Modifier",
                        column: x => x.CsGr_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroups_AspNetUsers_Cs_ID",
                        column: x => x.Cs_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerGroups_Groups_Gr_ID",
                        column: x => x.Gr_ID,
                        principalTable: "Groups",
                        principalColumn: "Gr_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookTags",
                columns: table => new
                {
                    Bk_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tg_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BkTg_Ordinal = table.Column<int>(type: "int", nullable: false),
                    BkTg_Active = table.Column<bool>(type: "bit", nullable: false),
                    BkTg_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BkTg_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BkTg_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BkTg_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTags", x => new { x.Bk_ID, x.Tg_ID });
                    table.ForeignKey(
                        name: "FK_BookTags_AspNetUsers_BkTg_Creator",
                        column: x => x.BkTg_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookTags_AspNetUsers_BkTg_Modifier",
                        column: x => x.BkTg_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookTags_Books_Bk_ID",
                        column: x => x.Bk_ID,
                        principalTable: "Books",
                        principalColumn: "Bk_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookTags_Tags_Tg_ID",
                        column: x => x.Tg_ID,
                        principalTable: "Tags",
                        principalColumn: "Tg_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTags",
                columns: table => new
                {
                    Cs_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tg_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CsTg_Ordinal = table.Column<int>(type: "int", nullable: false),
                    CsTg_Active = table.Column<bool>(type: "bit", nullable: false),
                    CsTg_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CsTg_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CsTg_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CsTg_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTags", x => new { x.Cs_ID, x.Tg_ID });
                    table.ForeignKey(
                        name: "FK_CustomerTags_AspNetUsers_CsTg_Creator",
                        column: x => x.CsTg_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerTags_AspNetUsers_CsTg_Modifier",
                        column: x => x.CsTg_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerTags_AspNetUsers_Cs_ID",
                        column: x => x.Cs_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerTags_Tags_Tg_ID",
                        column: x => x.Tg_ID,
                        principalTable: "Tags",
                        principalColumn: "Tg_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookThematicAreas",
                columns: table => new
                {
                    Bk_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Th_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BkTh_Ordinal = table.Column<int>(type: "int", nullable: false),
                    BkTh_Active = table.Column<bool>(type: "bit", nullable: false),
                    BkTh_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BkTh_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BkTh_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BkTh_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookThematicAreas", x => new { x.Bk_ID, x.Th_ID });
                    table.ForeignKey(
                        name: "FK_BookThematicAreas_AspNetUsers_BkTh_Creator",
                        column: x => x.BkTh_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookThematicAreas_AspNetUsers_BkTh_Modifier",
                        column: x => x.BkTh_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookThematicAreas_Books_Bk_ID",
                        column: x => x.Bk_ID,
                        principalTable: "Books",
                        principalColumn: "Bk_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookThematicAreas_ThematicAreas_Th_ID",
                        column: x => x.Th_ID,
                        principalTable: "ThematicAreas",
                        principalColumn: "Th_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerThematicAreas",
                columns: table => new
                {
                    Cs_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Th_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CsTh_Ordinal = table.Column<int>(type: "int", nullable: false),
                    CsTh_Active = table.Column<bool>(type: "bit", nullable: false),
                    CsTh_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CsTh_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CsTh_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CsTh_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerThematicAreas", x => new { x.Cs_ID, x.Th_ID });
                    table.ForeignKey(
                        name: "FK_CustomerThematicAreas_AspNetUsers_CsTh_Creator",
                        column: x => x.CsTh_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerThematicAreas_AspNetUsers_CsTh_Modifier",
                        column: x => x.CsTh_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerThematicAreas_AspNetUsers_Cs_ID",
                        column: x => x.Cs_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerThematicAreas_ThematicAreas_Th_ID",
                        column: x => x.Th_ID,
                        principalTable: "ThematicAreas",
                        principalColumn: "Th_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    An_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Md_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    An_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    An_Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    An_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    An_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    An_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    An_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    An_SendFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    An_SendTill = table.Column<DateTime>(type: "datetime2", nullable: true),
                    An_Active = table.Column<bool>(type: "bit", nullable: false),
                    An_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    An_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    An_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    An_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.An_ID);
                    table.ForeignKey(
                        name: "FK_Announcements_AspNetUsers_An_Creator",
                        column: x => x.An_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Announcements_AspNetUsers_An_Modifier",
                        column: x => x.An_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Announcements_Media_Md_ID",
                        column: x => x.Md_ID,
                        principalTable: "Media",
                        principalColumn: "Md_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    At_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Md_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    At_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    At_Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    At_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    At_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    At_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    At_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    At_Active = table.Column<bool>(type: "bit", nullable: false),
                    At_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    At_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    At_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    At_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.At_ID);
                    table.ForeignKey(
                        name: "FK_Authors_AspNetUsers_At_Creator",
                        column: x => x.At_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Authors_AspNetUsers_At_Modifier",
                        column: x => x.At_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Authors_Media_Md_ID",
                        column: x => x.Md_ID,
                        principalTable: "Media",
                        principalColumn: "Md_ID");
                });

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Bn_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Md_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bn_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bn_Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bn_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bn_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bn_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bn_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bn_PublishFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Bn_PublishTill = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Bn_Active = table.Column<bool>(type: "bit", nullable: false),
                    Bn_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bn_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Bn_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Bn_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Bn_ID);
                    table.ForeignKey(
                        name: "FK_Banners_AspNetUsers_Bn_Creator",
                        column: x => x.Bn_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Banners_AspNetUsers_Bn_Modifier",
                        column: x => x.Bn_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Banners_Media_Md_ID",
                        column: x => x.Md_ID,
                        principalTable: "Media",
                        principalColumn: "Md_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Ct_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ct_ClassificationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Md_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ct_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ct_Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ct_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ct_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ct_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ct_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ct_Active = table.Column<bool>(type: "bit", nullable: false),
                    Ct_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ct_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Ct_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ct_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Ct_ID);
                    table.ForeignKey(
                        name: "FK_Categories_AspNetUsers_Ct_Creator",
                        column: x => x.Ct_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_AspNetUsers_Ct_Modifier",
                        column: x => x.Ct_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_Categories_Ct_ClassificationID",
                        column: x => x.Ct_ClassificationID,
                        principalTable: "Categories",
                        principalColumn: "Ct_ID");
                    table.ForeignKey(
                        name: "FK_Categories_Media_Md_ID",
                        column: x => x.Md_ID,
                        principalTable: "Media",
                        principalColumn: "Md_ID");
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Cn_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Md_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cn_Code = table.Column<int>(type: "int", nullable: false),
                    Cn_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cn_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cn_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cn_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cn_Active = table.Column<bool>(type: "bit", nullable: false),
                    Cn_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cn_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Cn_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cn_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Cn_ID);
                    table.ForeignKey(
                        name: "FK_Countries_AspNetUsers_Cn_Creator",
                        column: x => x.Cn_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Countries_AspNetUsers_Cn_Modifier",
                        column: x => x.Cn_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Countries_Media_Md_ID",
                        column: x => x.Md_ID,
                        principalTable: "Media",
                        principalColumn: "Md_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    Rw_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Md_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rw_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rw_Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rw_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rw_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rw_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rw_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rw_Coins = table.Column<int>(type: "int", nullable: false),
                    Rw_Duration = table.Column<int>(type: "int", nullable: false),
                    Rw_Active = table.Column<bool>(type: "bit", nullable: false),
                    Rw_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rw_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Rw_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rw_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.Rw_ID);
                    table.ForeignKey(
                        name: "FK_Rewards_AspNetUsers_Rw_Creator",
                        column: x => x.Rw_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rewards_AspNetUsers_Rw_Modifier",
                        column: x => x.Rw_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rewards_Media_Md_ID",
                        column: x => x.Md_ID,
                        principalTable: "Media",
                        principalColumn: "Md_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Sb_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Md_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Sb_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sb_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sb_Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sb_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sb_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sb_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sb_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sb_Price = table.Column<double>(type: "float", nullable: false),
                    Sb_Active = table.Column<bool>(type: "bit", nullable: false),
                    Sb_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sb_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Sb_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sb_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Sb_ID);
                    table.ForeignKey(
                        name: "FK_Subscriptions_AspNetUsers_Sb_Creator",
                        column: x => x.Sb_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subscriptions_AspNetUsers_Sb_Modifier",
                        column: x => x.Sb_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subscriptions_Media_Md_ID",
                        column: x => x.Md_ID,
                        principalTable: "Media",
                        principalColumn: "Md_ID");
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementConditions",
                columns: table => new
                {
                    AnCn_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    An_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnPr_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gr_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnCn_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnCn_Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnCn_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnCn_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnCn_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnCn_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnCn_Active = table.Column<bool>(type: "bit", nullable: false),
                    AnCn_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnCn_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AnCn_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnCn_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementConditions", x => x.AnCn_ID);
                    table.ForeignKey(
                        name: "FK_AnnouncementConditions_AnnouncementPriorities_AnPr_ID",
                        column: x => x.AnPr_ID,
                        principalTable: "AnnouncementPriorities",
                        principalColumn: "AnPr_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementConditions_Announcements_An_ID",
                        column: x => x.An_ID,
                        principalTable: "Announcements",
                        principalColumn: "An_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementConditions_AspNetUsers_AnCn_Creator",
                        column: x => x.AnCn_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnnouncementConditions_AspNetUsers_AnCn_Modifier",
                        column: x => x.AnCn_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementMessages",
                columns: table => new
                {
                    AnMs_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    An_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnPr_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnCh_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cs_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AnMs_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnMs_Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnMs_Active = table.Column<bool>(type: "bit", nullable: false),
                    AnMs_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnMs_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AnMs_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnMs_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementMessages", x => x.AnMs_ID);
                    table.ForeignKey(
                        name: "FK_AnnouncementMessages_AnnouncementChannels_AnCh_ID",
                        column: x => x.AnCh_ID,
                        principalTable: "AnnouncementChannels",
                        principalColumn: "AnCh_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementMessages_AnnouncementPriorities_AnPr_ID",
                        column: x => x.AnPr_ID,
                        principalTable: "AnnouncementPriorities",
                        principalColumn: "AnPr_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementMessages_Announcements_An_ID",
                        column: x => x.An_ID,
                        principalTable: "Announcements",
                        principalColumn: "An_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementMessages_AspNetUsers_AnMs_Creator",
                        column: x => x.AnMs_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnnouncementMessages_AspNetUsers_AnMs_Modifier",
                        column: x => x.AnMs_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnnouncementMessages_AspNetUsers_Cs_ID",
                        column: x => x.Cs_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuthorGenres",
                columns: table => new
                {
                    At_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gn_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AtGn_Ordinal = table.Column<int>(type: "int", nullable: false),
                    AtGn_Active = table.Column<bool>(type: "bit", nullable: false),
                    AtGn_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AtGn_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AtGn_ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AtGn_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorGenres", x => new { x.At_ID, x.Gn_ID });
                    table.ForeignKey(
                        name: "FK_AuthorGenres_AspNetUsers_AtGn_Creator",
                        column: x => x.AtGn_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuthorGenres_AspNetUsers_AtGn_Modifier",
                        column: x => x.AtGn_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuthorGenres_Authors_At_ID",
                        column: x => x.At_ID,
                        principalTable: "Authors",
                        principalColumn: "At_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthorGenres_Genres_Gn_ID",
                        column: x => x.Gn_ID,
                        principalTable: "Genres",
                        principalColumn: "Gn_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    Bk_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    At_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BkAt_Ordinal = table.Column<int>(type: "int", nullable: false),
                    BkAt_Active = table.Column<bool>(type: "bit", nullable: false),
                    BkAt_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BkAt_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BkAt_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BkAt_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.Bk_ID, x.At_ID });
                    table.ForeignKey(
                        name: "FK_BookAuthors_AspNetUsers_BkAt_Creator",
                        column: x => x.BkAt_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookAuthors_AspNetUsers_BkAt_Modifier",
                        column: x => x.BkAt_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_At_ID",
                        column: x => x.At_ID,
                        principalTable: "Authors",
                        principalColumn: "At_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_Bk_ID",
                        column: x => x.Bk_ID,
                        principalTable: "Books",
                        principalColumn: "Bk_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BannerBannerLocations",
                columns: table => new
                {
                    Bn_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bl_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BnBl_Ordinal = table.Column<int>(type: "int", nullable: false),
                    BnBl_Active = table.Column<bool>(type: "bit", nullable: false),
                    BnBl_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BnBl_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BnBl_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BnBl_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerBannerLocations", x => new { x.Bn_Id, x.Bl_ID });
                    table.ForeignKey(
                        name: "FK_BannerBannerLocations_AspNetUsers_BnBl_Creator",
                        column: x => x.BnBl_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BannerBannerLocations_AspNetUsers_BnBl_Modifier",
                        column: x => x.BnBl_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BannerBannerLocations_BannerLocations_Bl_ID",
                        column: x => x.Bl_ID,
                        principalTable: "BannerLocations",
                        principalColumn: "Bl_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BannerBannerLocations_Banners_Bn_Id",
                        column: x => x.Bn_Id,
                        principalTable: "Banners",
                        principalColumn: "Bn_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BannerGroups",
                columns: table => new
                {
                    Bn_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gr_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BnGr_Ordinal = table.Column<int>(type: "int", nullable: false),
                    BnGr_Active = table.Column<bool>(type: "bit", nullable: false),
                    BnGr_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BnGr_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BnGr_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BnGr_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerGroups", x => new { x.Bn_Id, x.Gr_ID });
                    table.ForeignKey(
                        name: "FK_BannerGroups_AspNetUsers_BnGr_Creator",
                        column: x => x.BnGr_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BannerGroups_AspNetUsers_BnGr_Modifier",
                        column: x => x.BnGr_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BannerGroups_Banners_Bn_Id",
                        column: x => x.Bn_Id,
                        principalTable: "Banners",
                        principalColumn: "Bn_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BannerGroups_Groups_Gr_ID",
                        column: x => x.Gr_ID,
                        principalTable: "Groups",
                        principalColumn: "Gr_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookCategories",
                columns: table => new
                {
                    Bk_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ct_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BkCt_Ordinal = table.Column<int>(type: "int", nullable: false),
                    BkCt_Active = table.Column<bool>(type: "bit", nullable: false),
                    BkCt_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BkCt_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BkCt_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BkCt_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategories", x => new { x.Bk_ID, x.Ct_ID });
                    table.ForeignKey(
                        name: "FK_BookCategories_AspNetUsers_BkCt_Creator",
                        column: x => x.BkCt_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookCategories_AspNetUsers_BkCt_Modifier",
                        column: x => x.BkCt_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookCategories_Books_Bk_ID",
                        column: x => x.Bk_ID,
                        principalTable: "Books",
                        principalColumn: "Bk_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookCategories_Categories_Ct_ID",
                        column: x => x.Ct_ID,
                        principalTable: "Categories",
                        principalColumn: "Ct_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTags",
                columns: table => new
                {
                    Ct_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tg_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CtTg_Ordinal = table.Column<int>(type: "int", nullable: false),
                    CtTg_Active = table.Column<bool>(type: "bit", nullable: false),
                    CtTg_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CtTg_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CtTg_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CtTg_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTags", x => new { x.Ct_ID, x.Tg_ID });
                    table.ForeignKey(
                        name: "FK_CategoryTags_AspNetUsers_CtTg_Creator",
                        column: x => x.CtTg_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CategoryTags_AspNetUsers_CtTg_Modifier",
                        column: x => x.CtTg_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CategoryTags_Categories_Ct_ID",
                        column: x => x.Ct_ID,
                        principalTable: "Categories",
                        principalColumn: "Ct_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryTags_Tags_Tg_ID",
                        column: x => x.Tg_ID,
                        principalTable: "Tags",
                        principalColumn: "Tg_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCategories",
                columns: table => new
                {
                    Cs_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ct_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CsCt_Ordinal = table.Column<int>(type: "int", nullable: false),
                    CsCt_Active = table.Column<bool>(type: "bit", nullable: false),
                    CsCt_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CsCt_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CsCt_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CsCt_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Th_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCategories", x => new { x.Cs_ID, x.Ct_ID });
                    table.ForeignKey(
                        name: "FK_CustomerCategories_AspNetUsers_CsCt_Creator",
                        column: x => x.CsCt_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerCategories_AspNetUsers_CsCt_Modifier",
                        column: x => x.CsCt_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerCategories_AspNetUsers_Cs_ID",
                        column: x => x.Cs_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerCategories_Categories_Th_ID",
                        column: x => x.Th_ID,
                        principalTable: "Categories",
                        principalColumn: "Ct_ID");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Ad_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Us_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Cn_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ad_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad_IsBilling = table.Column<bool>(type: "bit", nullable: false),
                    Ad_IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Ad_Building = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad_Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad_Zone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad_Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad_Active = table.Column<bool>(type: "bit", nullable: false),
                    Ad_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ad_Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ad_Modifier = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Ad_ID);
                    table.ForeignKey(
                        name: "FK_Addresses_AspNetUsers_Us_ID",
                        column: x => x.Us_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_Cn_ID",
                        column: x => x.Cn_ID,
                        principalTable: "Countries",
                        principalColumn: "Cn_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Pb_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Md_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Cn_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pb_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pb_Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pb_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pb_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pb_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pb_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pb_Active = table.Column<bool>(type: "bit", nullable: false),
                    Pb_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pb_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Pb_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pb_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Pb_ID);
                    table.ForeignKey(
                        name: "FK_Publishers_AspNetUsers_Pb_Creator",
                        column: x => x.Pb_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Publishers_AspNetUsers_Pb_Modifier",
                        column: x => x.Pb_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Publishers_Countries_Cn_ID",
                        column: x => x.Cn_ID,
                        principalTable: "Countries",
                        principalColumn: "Cn_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Publishers_Media_Md_ID",
                        column: x => x.Md_ID,
                        principalTable: "Media",
                        principalColumn: "Md_ID");
                });

            migrationBuilder.CreateTable(
                name: "CustomerRewards",
                columns: table => new
                {
                    Cs_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rw_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CsRw_Coins = table.Column<int>(type: "int", nullable: false),
                    CsRw_Duration = table.Column<int>(type: "int", nullable: false),
                    CsRw_CoinsLeft = table.Column<int>(type: "int", nullable: false),
                    CsRw_ExpiredOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CsRw_Ordinal = table.Column<int>(type: "int", nullable: false),
                    CsRw_Active = table.Column<bool>(type: "bit", nullable: false),
                    CsRw_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CsRw_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CsRw_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CsRw_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRewards", x => new { x.Cs_ID, x.Rw_ID });
                    table.ForeignKey(
                        name: "FK_CustomerRewards_AspNetUsers_CsRw_Creator",
                        column: x => x.CsRw_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerRewards_AspNetUsers_CsRw_Modifier",
                        column: x => x.CsRw_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerRewards_AspNetUsers_Cs_ID",
                        column: x => x.Cs_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerRewards_Rewards_Rw_ID",
                        column: x => x.Rw_ID,
                        principalTable: "Rewards",
                        principalColumn: "Rw_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RewardGroups",
                columns: table => new
                {
                    Rw_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gr_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RwGr_Ordinal = table.Column<int>(type: "int", nullable: false),
                    RwGr_Active = table.Column<bool>(type: "bit", nullable: false),
                    RwGr_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RwGr_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RwGr_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RwGr_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardGroups", x => new { x.Rw_Id, x.Gr_ID });
                    table.ForeignKey(
                        name: "FK_RewardGroups_AspNetUsers_RwGr_Creator",
                        column: x => x.RwGr_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RewardGroups_AspNetUsers_RwGr_Modifier",
                        column: x => x.RwGr_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RewardGroups_Groups_Gr_ID",
                        column: x => x.Gr_ID,
                        principalTable: "Groups",
                        principalColumn: "Gr_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RewardGroups_Rewards_Rw_Id",
                        column: x => x.Rw_Id,
                        principalTable: "Rewards",
                        principalColumn: "Rw_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementConditionAnnouncementChannels",
                columns: table => new
                {
                    AnCn_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnCh_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnChGr_Ordinal = table.Column<int>(type: "int", nullable: false),
                    AnChGr_Active = table.Column<bool>(type: "bit", nullable: false),
                    AnChGr_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnChGr_Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnChGr_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnChGr_Modifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnCnGr_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AnCnGr_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementConditionAnnouncementChannels", x => new { x.AnCn_ID, x.AnCh_ID });
                    table.ForeignKey(
                        name: "FK_AnnouncementConditionAnnouncementChannels_AnnouncementChannels_AnCh_ID",
                        column: x => x.AnCh_ID,
                        principalTable: "AnnouncementChannels",
                        principalColumn: "AnCh_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementConditionAnnouncementChannels_AnnouncementConditions_AnCn_ID",
                        column: x => x.AnCn_ID,
                        principalTable: "AnnouncementConditions",
                        principalColumn: "AnCn_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementConditionAnnouncementChannels_AspNetUsers_AnCnGr_Creator",
                        column: x => x.AnCnGr_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnnouncementConditionAnnouncementChannels_AspNetUsers_AnCnGr_Modifier",
                        column: x => x.AnCnGr_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementConditionGroups",
                columns: table => new
                {
                    AnCn_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gr_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnCnGr_Ordinal = table.Column<int>(type: "int", nullable: false),
                    AnCnGr_Active = table.Column<bool>(type: "bit", nullable: false),
                    AnCnGr_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnCnGr_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AnCnGr_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnCnGr_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementConditionGroups", x => new { x.AnCn_Id, x.Gr_ID });
                    table.ForeignKey(
                        name: "FK_AnnouncementConditionGroups_AnnouncementConditions_AnCn_Id",
                        column: x => x.AnCn_Id,
                        principalTable: "AnnouncementConditions",
                        principalColumn: "AnCn_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementConditionGroups_AspNetUsers_AnCnGr_Creator",
                        column: x => x.AnCnGr_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnnouncementConditionGroups_AspNetUsers_AnCnGr_Modifier",
                        column: x => x.AnCnGr_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnnouncementConditionGroups_Groups_Gr_ID",
                        column: x => x.Gr_ID,
                        principalTable: "Groups",
                        principalColumn: "Gr_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Or_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Py_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Cs_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Or_AddressID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Or_BillingAddressID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Or_CustomerNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Or_StaffNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Or_PromotionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Or_SubTotal = table.Column<double>(type: "float", nullable: false),
                    Or_TotalDiscount = table.Column<double>(type: "float", nullable: false),
                    Or_Vat = table.Column<double>(type: "float", nullable: false),
                    Or_Total = table.Column<double>(type: "float", nullable: false),
                    Or_PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Or_PaymentNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Or_PaymentDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Or_PaymentReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Or_Active = table.Column<bool>(type: "bit", nullable: false),
                    Or_IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Or_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Or_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Or_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Or_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Or_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Or_ID);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_Or_AddressID",
                        column: x => x.Or_AddressID,
                        principalTable: "Addresses",
                        principalColumn: "Ad_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_Or_BillingAddressID",
                        column: x => x.Or_BillingAddressID,
                        principalTable: "Addresses",
                        principalColumn: "Ad_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_Cs_ID",
                        column: x => x.Cs_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_Or_Creator",
                        column: x => x.Or_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_Or_Modifier",
                        column: x => x.Or_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_PaymentMethods_Py_ID",
                        column: x => x.Py_ID,
                        principalTable: "PaymentMethods",
                        principalColumn: "Py_ID");
                });

            migrationBuilder.CreateTable(
                name: "BookPublishers",
                columns: table => new
                {
                    Bk_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pb_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BkPb_Ordinal = table.Column<int>(type: "int", nullable: false),
                    BkPb_Active = table.Column<bool>(type: "bit", nullable: false),
                    BkPb_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BkPb_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BkPb_ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BkPb_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPublishers", x => new { x.Bk_ID, x.Pb_ID });
                    table.ForeignKey(
                        name: "FK_BookPublishers_AspNetUsers_BkPb_Creator",
                        column: x => x.BkPb_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookPublishers_AspNetUsers_BkPb_Modifier",
                        column: x => x.BkPb_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookPublishers_Books_Bk_ID",
                        column: x => x.Bk_ID,
                        principalTable: "Books",
                        principalColumn: "Bk_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookPublishers_Publishers_Pb_ID",
                        column: x => x.Pb_ID,
                        principalTable: "Publishers",
                        principalColumn: "Pb_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PublisherGenres",
                columns: table => new
                {
                    Pb_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gn_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PbGn_Ordinal = table.Column<int>(type: "int", nullable: false),
                    PbGn_Active = table.Column<bool>(type: "bit", nullable: false),
                    PbGn_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PbGn_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PbGn_ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PbGn_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherGenres", x => new { x.Pb_ID, x.Gn_ID });
                    table.ForeignKey(
                        name: "FK_PublisherGenres_AspNetUsers_PbGn_Creator",
                        column: x => x.PbGn_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublisherGenres_AspNetUsers_PbGn_Modifier",
                        column: x => x.PbGn_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublisherGenres_Genres_Gn_ID",
                        column: x => x.Gn_ID,
                        principalTable: "Genres",
                        principalColumn: "Gn_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublisherGenres_Publishers_Pb_ID",
                        column: x => x.Pb_ID,
                        principalTable: "Publishers",
                        principalColumn: "Pb_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderSubscriptions",
                columns: table => new
                {
                    Or_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sb_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrSb_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrSb_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrSb_Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrSb_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrSb_Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrSb_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrSb_Desc_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrSb_Price = table.Column<double>(type: "float", nullable: false),
                    OrSb_Quantity = table.Column<int>(type: "int", nullable: false),
                    OrSb_Discount = table.Column<double>(type: "float", nullable: false),
                    OrSb_SubTotal = table.Column<double>(type: "float", nullable: false),
                    OrSb_Ordinal = table.Column<int>(type: "int", nullable: false),
                    OrSb_Active = table.Column<bool>(type: "bit", nullable: false),
                    OrSb_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrSb_Creator = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrSb_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrSb_Modifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSubscriptions", x => new { x.Or_ID, x.Sb_ID });
                    table.ForeignKey(
                        name: "FK_OrderSubscriptions_AspNetUsers_OrSb_Creator",
                        column: x => x.OrSb_Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderSubscriptions_AspNetUsers_OrSb_Modifier",
                        column: x => x.OrSb_Modifier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderSubscriptions_Orders_Or_ID",
                        column: x => x.Or_ID,
                        principalTable: "Orders",
                        principalColumn: "Or_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderSubscriptions_Subscriptions_Sb_ID",
                        column: x => x.Sb_ID,
                        principalTable: "Subscriptions",
                        principalColumn: "Sb_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Cn_ID",
                table: "Addresses",
                column: "Cn_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Us_ID",
                table: "Addresses",
                column: "Us_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementChannels_AnCh_Creator",
                table: "AnnouncementChannels",
                column: "AnCh_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementChannels_AnCh_Modifier",
                table: "AnnouncementChannels",
                column: "AnCh_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementConditionAnnouncementChannels_AnCh_ID",
                table: "AnnouncementConditionAnnouncementChannels",
                column: "AnCh_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementConditionAnnouncementChannels_AnCnGr_Creator",
                table: "AnnouncementConditionAnnouncementChannels",
                column: "AnCnGr_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementConditionAnnouncementChannels_AnCnGr_Modifier",
                table: "AnnouncementConditionAnnouncementChannels",
                column: "AnCnGr_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementConditionGroups_AnCnGr_Creator",
                table: "AnnouncementConditionGroups",
                column: "AnCnGr_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementConditionGroups_AnCnGr_Modifier",
                table: "AnnouncementConditionGroups",
                column: "AnCnGr_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementConditionGroups_Gr_ID",
                table: "AnnouncementConditionGroups",
                column: "Gr_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementConditions_An_ID",
                table: "AnnouncementConditions",
                column: "An_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementConditions_AnCn_Creator",
                table: "AnnouncementConditions",
                column: "AnCn_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementConditions_AnCn_Modifier",
                table: "AnnouncementConditions",
                column: "AnCn_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementConditions_AnPr_ID",
                table: "AnnouncementConditions",
                column: "AnPr_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementMessages_An_ID",
                table: "AnnouncementMessages",
                column: "An_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementMessages_AnCh_ID",
                table: "AnnouncementMessages",
                column: "AnCh_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementMessages_AnMs_Creator",
                table: "AnnouncementMessages",
                column: "AnMs_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementMessages_AnMs_Modifier",
                table: "AnnouncementMessages",
                column: "AnMs_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementMessages_AnPr_ID",
                table: "AnnouncementMessages",
                column: "AnPr_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementMessages_Cs_ID",
                table: "AnnouncementMessages",
                column: "Cs_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementPriorities_AnPr_Creator",
                table: "AnnouncementPriorities",
                column: "AnPr_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementPriorities_AnPr_Modifier",
                table: "AnnouncementPriorities",
                column: "AnPr_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_An_Creator",
                table: "Announcements",
                column: "An_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_An_Modifier",
                table: "Announcements",
                column: "An_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_Md_ID",
                table: "Announcements",
                column: "Md_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Us_Creator",
                table: "AspNetUsers",
                column: "Us_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Us_Modifier",
                table: "AspNetUsers",
                column: "Us_Modifier");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorGenres_AtGn_Creator",
                table: "AuthorGenres",
                column: "AtGn_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorGenres_AtGn_Modifier",
                table: "AuthorGenres",
                column: "AtGn_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorGenres_Gn_ID",
                table: "AuthorGenres",
                column: "Gn_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_At_Creator",
                table: "Authors",
                column: "At_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_At_Modifier",
                table: "Authors",
                column: "At_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Md_ID",
                table: "Authors",
                column: "Md_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_Av_Creator",
                table: "Avatars",
                column: "Av_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_Av_Modifier",
                table: "Avatars",
                column: "Av_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_BannerBannerLocations_Bl_ID",
                table: "BannerBannerLocations",
                column: "Bl_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BannerBannerLocations_BnBl_Creator",
                table: "BannerBannerLocations",
                column: "BnBl_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_BannerBannerLocations_BnBl_Modifier",
                table: "BannerBannerLocations",
                column: "BnBl_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_BannerGroups_BnGr_Creator",
                table: "BannerGroups",
                column: "BnGr_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_BannerGroups_BnGr_Modifier",
                table: "BannerGroups",
                column: "BnGr_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_BannerGroups_Gr_ID",
                table: "BannerGroups",
                column: "Gr_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BannerLocations_Bl_Creator",
                table: "BannerLocations",
                column: "Bl_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_BannerLocations_Bl_Modifier",
                table: "BannerLocations",
                column: "Bl_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_Banners_Bn_Creator",
                table: "Banners",
                column: "Bn_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Banners_Bn_Modifier",
                table: "Banners",
                column: "Bn_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_Banners_Md_ID",
                table: "Banners",
                column: "Md_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_At_ID",
                table: "BookAuthors",
                column: "At_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_BkAt_Creator",
                table: "BookAuthors",
                column: "BkAt_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_BkAt_Modifier",
                table: "BookAuthors",
                column: "BkAt_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_BkCt_Creator",
                table: "BookCategories",
                column: "BkCt_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_BkCt_Modifier",
                table: "BookCategories",
                column: "BkCt_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_Ct_ID",
                table: "BookCategories",
                column: "Ct_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_BkGn_Creator",
                table: "BookGenres",
                column: "BkGn_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_BkGn_Modifier",
                table: "BookGenres",
                column: "BkGn_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_Gn_ID",
                table: "BookGenres",
                column: "Gn_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BookPublishers_BkPb_Creator",
                table: "BookPublishers",
                column: "BkPb_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_BookPublishers_BkPb_Modifier",
                table: "BookPublishers",
                column: "BkPb_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_BookPublishers_Pb_ID",
                table: "BookPublishers",
                column: "Pb_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Bk_Creator",
                table: "Books",
                column: "Bk_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Bk_Modifier",
                table: "Books",
                column: "Bk_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_BookTags_BkTg_Creator",
                table: "BookTags",
                column: "BkTg_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_BookTags_BkTg_Modifier",
                table: "BookTags",
                column: "BkTg_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_BookTags_Tg_ID",
                table: "BookTags",
                column: "Tg_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BookThematicAreas_BkTh_Creator",
                table: "BookThematicAreas",
                column: "BkTh_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_BookThematicAreas_BkTh_Modifier",
                table: "BookThematicAreas",
                column: "BkTh_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_BookThematicAreas_Th_ID",
                table: "BookThematicAreas",
                column: "Th_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Ct_ClassificationID",
                table: "Categories",
                column: "Ct_ClassificationID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Ct_Creator",
                table: "Categories",
                column: "Ct_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Ct_Modifier",
                table: "Categories",
                column: "Ct_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Md_ID",
                table: "Categories",
                column: "Md_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTags_CtTg_Creator",
                table: "CategoryTags",
                column: "CtTg_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTags_CtTg_Modifier",
                table: "CategoryTags",
                column: "CtTg_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTags_Tg_ID",
                table: "CategoryTags",
                column: "Tg_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Cn_Creator",
                table: "Countries",
                column: "Cn_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Cn_Modifier",
                table: "Countries",
                column: "Cn_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Md_ID",
                table: "Countries",
                column: "Md_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCategories_CsCt_Creator",
                table: "CustomerCategories",
                column: "CsCt_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCategories_CsCt_Modifier",
                table: "CustomerCategories",
                column: "CsCt_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCategories_Th_ID",
                table: "CustomerCategories",
                column: "Th_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGenres_CsGn_Creator",
                table: "CustomerGenres",
                column: "CsGn_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGenres_CsGn_Modifier",
                table: "CustomerGenres",
                column: "CsGn_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGenres_Gn_ID",
                table: "CustomerGenres",
                column: "Gn_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroups_CsGr_Creator",
                table: "CustomerGroups",
                column: "CsGr_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroups_CsGr_Modifier",
                table: "CustomerGroups",
                column: "CsGr_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroups_Gr_ID",
                table: "CustomerGroups",
                column: "Gr_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRewards_CsRw_Creator",
                table: "CustomerRewards",
                column: "CsRw_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRewards_CsRw_Modifier",
                table: "CustomerRewards",
                column: "CsRw_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRewards_Rw_ID",
                table: "CustomerRewards",
                column: "Rw_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTags_CsTg_Creator",
                table: "CustomerTags",
                column: "CsTg_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTags_CsTg_Modifier",
                table: "CustomerTags",
                column: "CsTg_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTags_Tg_ID",
                table: "CustomerTags",
                column: "Tg_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerThematicAreas_CsTh_Creator",
                table: "CustomerThematicAreas",
                column: "CsTh_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerThematicAreas_CsTh_Modifier",
                table: "CustomerThematicAreas",
                column: "CsTh_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerThematicAreas_Th_ID",
                table: "CustomerThematicAreas",
                column: "Th_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Gn_Creator",
                table: "Genres",
                column: "Gn_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Gn_Modifier",
                table: "Genres",
                column: "Gn_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Gr_Creator",
                table: "Groups",
                column: "Gr_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Gr_Modifier",
                table: "Groups",
                column: "Gr_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_Media_Bk_ID",
                table: "Media",
                column: "Bk_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Media_Md_Creator",
                table: "Media",
                column: "Md_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Media_Md_Modifier",
                table: "Media",
                column: "Md_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Cs_ID",
                table: "Orders",
                column: "Cs_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Or_AddressID",
                table: "Orders",
                column: "Or_AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Or_BillingAddressID",
                table: "Orders",
                column: "Or_BillingAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Or_Creator",
                table: "Orders",
                column: "Or_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Or_Modifier",
                table: "Orders",
                column: "Or_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Py_ID",
                table: "Orders",
                column: "Py_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSubscriptions_OrSb_Creator",
                table: "OrderSubscriptions",
                column: "OrSb_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSubscriptions_OrSb_Modifier",
                table: "OrderSubscriptions",
                column: "OrSb_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSubscriptions_Sb_ID",
                table: "OrderSubscriptions",
                column: "Sb_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_Py_Creator",
                table: "PaymentMethods",
                column: "Py_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_Py_Modifier",
                table: "PaymentMethods",
                column: "Py_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_PublisherGenres_Gn_ID",
                table: "PublisherGenres",
                column: "Gn_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PublisherGenres_PbGn_Creator",
                table: "PublisherGenres",
                column: "PbGn_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_PublisherGenres_PbGn_Modifier",
                table: "PublisherGenres",
                column: "PbGn_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_Cn_ID",
                table: "Publishers",
                column: "Cn_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_Md_ID",
                table: "Publishers",
                column: "Md_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_Pb_Creator",
                table: "Publishers",
                column: "Pb_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_Pb_Modifier",
                table: "Publishers",
                column: "Pb_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_RewardGroups_Gr_ID",
                table: "RewardGroups",
                column: "Gr_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RewardGroups_RwGr_Creator",
                table: "RewardGroups",
                column: "RwGr_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_RewardGroups_RwGr_Modifier",
                table: "RewardGroups",
                column: "RwGr_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_Rewards_Md_ID",
                table: "Rewards",
                column: "Md_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rewards_Rw_Creator",
                table: "Rewards",
                column: "Rw_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Rewards_Rw_Modifier",
                table: "Rewards",
                column: "Rw_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Md_ID",
                table: "Subscriptions",
                column: "Md_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Sb_Creator",
                table: "Subscriptions",
                column: "Sb_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Sb_Modifier",
                table: "Subscriptions",
                column: "Sb_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Tg_Creator",
                table: "Tags",
                column: "Tg_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Tg_Modifier",
                table: "Tags",
                column: "Tg_Modifier");

            migrationBuilder.CreateIndex(
                name: "IX_ThematicAreas_Th_Creator",
                table: "ThematicAreas",
                column: "Th_Creator");

            migrationBuilder.CreateIndex(
                name: "IX_ThematicAreas_Th_Modifier",
                table: "ThematicAreas",
                column: "Th_Modifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnouncementConditionAnnouncementChannels");

            migrationBuilder.DropTable(
                name: "AnnouncementConditionGroups");

            migrationBuilder.DropTable(
                name: "AnnouncementMessages");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuthorGenres");

            migrationBuilder.DropTable(
                name: "Avatars");

            migrationBuilder.DropTable(
                name: "BannerBannerLocations");

            migrationBuilder.DropTable(
                name: "BannerGroups");

            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "BookCategories");

            migrationBuilder.DropTable(
                name: "BookGenres");

            migrationBuilder.DropTable(
                name: "BookPublishers");

            migrationBuilder.DropTable(
                name: "BookTags");

            migrationBuilder.DropTable(
                name: "BookThematicAreas");

            migrationBuilder.DropTable(
                name: "CategoryTags");

            migrationBuilder.DropTable(
                name: "CustomerCategories");

            migrationBuilder.DropTable(
                name: "CustomerGenres");

            migrationBuilder.DropTable(
                name: "CustomerGroups");

            migrationBuilder.DropTable(
                name: "CustomerRewards");

            migrationBuilder.DropTable(
                name: "CustomerTags");

            migrationBuilder.DropTable(
                name: "CustomerThematicAreas");

            migrationBuilder.DropTable(
                name: "OrderSubscriptions");

            migrationBuilder.DropTable(
                name: "PublisherGenres");

            migrationBuilder.DropTable(
                name: "RewardGroups");

            migrationBuilder.DropTable(
                name: "AnnouncementConditions");

            migrationBuilder.DropTable(
                name: "AnnouncementChannels");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BannerLocations");

            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "ThematicAreas");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Rewards");

            migrationBuilder.DropTable(
                name: "AnnouncementPriorities");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
