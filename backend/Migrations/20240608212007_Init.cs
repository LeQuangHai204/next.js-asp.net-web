using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cungcap",
                columns: table => new
                {
                    CungcapID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Tendaydu = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    TenLienhe = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Diachi = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Thanhpho = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    MaBuudien = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Quocgia = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Dienthoai = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cungcap", x => x.CungcapID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "danhmuc",
                columns: table => new
                {
                    DanhmucID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TenDanhMuc = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    MoTa = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_danhmuc", x => x.DanhmucID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "giaohang",
                columns: table => new
                {
                    ShipperID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Hoten = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Sodienthoai = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giaohang", x => x.ShipperID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "khachhang",
                columns: table => new
                {
                    KhachhangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    HoTen = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    TenLienLac = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Diachi = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Thanhpho = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    MaBuudien = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    QuocGia = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khachhang", x => x.KhachhangID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "nhanvien",
                columns: table => new
                {
                    NhanviennID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Ten = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Ho = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: true),
                    Anh = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Ghichu = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nhanvien", x => x.NhanviennID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sanpham",
                columns: table => new
                {
                    SanphamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CungcapID = table.Column<int>(type: "int", nullable: false),
                    DanhmucID = table.Column<int>(type: "int", nullable: true),
                    TenSanpham = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Donvi = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Gia = table.Column<decimal>(type: "decimal(13,2)", precision: 13, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sanpham", x => x.SanphamID);
                    table.ForeignKey(
                        name: "FK_sanpham_cungcap_CungcapID",
                        column: x => x.CungcapID,
                        principalTable: "cungcap",
                        principalColumn: "CungcapID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sanpham_danhmuc_DanhmucID",
                        column: x => x.DanhmucID,
                        principalTable: "danhmuc",
                        principalColumn: "DanhmucID");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "donhang",
                columns: table => new
                {
                    DonhangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KhachhangID = table.Column<int>(type: "int", nullable: true),
                    NhanvienID = table.Column<int>(type: "int", nullable: true),
                    Ngaydathang = table.Column<DateTime>(type: "date", nullable: false),
                    ShipperID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_donhang", x => x.DonhangID);
                    table.ForeignKey(
                        name: "FK_donhang_giaohang_ShipperID",
                        column: x => x.ShipperID,
                        principalTable: "giaohang",
                        principalColumn: "ShipperID");
                    table.ForeignKey(
                        name: "FK_donhang_khachhang_KhachhangID",
                        column: x => x.KhachhangID,
                        principalTable: "khachhang",
                        principalColumn: "KhachhangID");
                    table.ForeignKey(
                        name: "FK_donhang_nhanvien_NhanvienID",
                        column: x => x.NhanvienID,
                        principalTable: "nhanvien",
                        principalColumn: "NhanviennID");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "donhangchitiet",
                columns: table => new
                {
                    DonhangChitietID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DonhangID = table.Column<int>(type: "int", nullable: false),
                    SanphamID = table.Column<int>(type: "int", nullable: false),
                    Soluong = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_donhangchitiet", x => x.DonhangChitietID);
                    table.ForeignKey(
                        name: "FK_donhangchitiet_donhang_DonhangID",
                        column: x => x.DonhangID,
                        principalTable: "donhang",
                        principalColumn: "DonhangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_donhangchitiet_sanpham_SanphamID",
                        column: x => x.SanphamID,
                        principalTable: "sanpham",
                        principalColumn: "SanphamID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "Staff", "STAFF" },
                    { "3", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_donhang_KhachhangID",
                table: "donhang",
                column: "KhachhangID");

            migrationBuilder.CreateIndex(
                name: "IX_donhang_NhanvienID",
                table: "donhang",
                column: "NhanvienID");

            migrationBuilder.CreateIndex(
                name: "IX_donhang_ShipperID",
                table: "donhang",
                column: "ShipperID");

            migrationBuilder.CreateIndex(
                name: "IX_donhangchitiet_DonhangID",
                table: "donhangchitiet",
                column: "DonhangID");

            migrationBuilder.CreateIndex(
                name: "IX_donhangchitiet_SanphamID",
                table: "donhangchitiet",
                column: "SanphamID");

            migrationBuilder.CreateIndex(
                name: "IX_sanpham_CungcapID",
                table: "sanpham",
                column: "CungcapID");

            migrationBuilder.CreateIndex(
                name: "IX_sanpham_DanhmucID",
                table: "sanpham",
                column: "DanhmucID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "donhangchitiet");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "donhang");

            migrationBuilder.DropTable(
                name: "sanpham");

            migrationBuilder.DropTable(
                name: "giaohang");

            migrationBuilder.DropTable(
                name: "khachhang");

            migrationBuilder.DropTable(
                name: "nhanvien");

            migrationBuilder.DropTable(
                name: "cungcap");

            migrationBuilder.DropTable(
                name: "danhmuc");
        }
    }
}
