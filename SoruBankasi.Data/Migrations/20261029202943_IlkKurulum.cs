using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoruBankasi.Data.Migrations
{
    /// <inheritdoc />
    public partial class IlkKurulum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kazanimlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kod = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ders = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kazanimlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParolaHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sinavlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgretmenId = table.Column<int>(type: "int", nullable: false),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SureDk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinavlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sinavlar_Kullanicilar_OgretmenId",
                        column: x => x.OgretmenId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sorular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KazanimId = table.Column<int>(type: "int", nullable: false),
                    OgretmenId = table.Column<int>(type: "int", nullable: false),
                    Metin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zorluk = table.Column<int>(type: "int", nullable: false),
                    AnahtarNoktalar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sorular", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sorular_Kazanimlar_KazanimId",
                        column: x => x.KazanimId,
                        principalTable: "Kazanimlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sorular_Kullanicilar_OgretmenId",
                        column: x => x.OgretmenId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sonuclar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SinavId = table.Column<int>(type: "int", nullable: false),
                    OgrenciId = table.Column<int>(type: "int", nullable: false),
                    ToplamPuan = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sonuclar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sonuclar_Kullanicilar_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sonuclar_Sinavlar_SinavId",
                        column: x => x.SinavId,
                        principalTable: "Sinavlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Secenekler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoruId = table.Column<int>(type: "int", nullable: false),
                    Metin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogruMu = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secenekler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Secenekler_Sorular_SoruId",
                        column: x => x.SoruId,
                        principalTable: "Sorular",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SinavSorular",
                columns: table => new
                {
                    SinavId = table.Column<int>(type: "int", nullable: false),
                    SoruId = table.Column<int>(type: "int", nullable: false),
                    SiraNo = table.Column<int>(type: "int", nullable: false),
                    Puan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinavSorular", x => new { x.SinavId, x.SoruId });
                    table.ForeignKey(
                        name: "FK_SinavSorular_Sinavlar_SinavId",
                        column: x => x.SinavId,
                        principalTable: "Sinavlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SinavSorular_Sorular_SoruId",
                        column: x => x.SoruId,
                        principalTable: "Sorular",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cevaplar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SonucId = table.Column<int>(type: "int", nullable: false),
                    SoruId = table.Column<int>(type: "int", nullable: false),
                    VerilenCevap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Puan = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true),
                    AiOnPuan = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cevaplar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cevaplar_Sonuclar_SonucId",
                        column: x => x.SonucId,
                        principalTable: "Sonuclar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cevaplar_Sorular_SoruId",
                        column: x => x.SoruId,
                        principalTable: "Sorular",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cevaplar_SonucId",
                table: "Cevaplar",
                column: "SonucId");

            migrationBuilder.CreateIndex(
                name: "IX_Cevaplar_SoruId",
                table: "Cevaplar",
                column: "SoruId");

            migrationBuilder.CreateIndex(
                name: "IX_Kazanimlar_Kod",
                table: "Kazanimlar",
                column: "Kod");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_Email",
                table: "Kullanicilar",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Secenekler_SoruId",
                table: "Secenekler",
                column: "SoruId");

            migrationBuilder.CreateIndex(
                name: "IX_Sinavlar_OgretmenId",
                table: "Sinavlar",
                column: "OgretmenId");

            migrationBuilder.CreateIndex(
                name: "IX_SinavSorular_SoruId",
                table: "SinavSorular",
                column: "SoruId");

            migrationBuilder.CreateIndex(
                name: "IX_Sonuclar_OgrenciId",
                table: "Sonuclar",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_Sonuclar_SinavId",
                table: "Sonuclar",
                column: "SinavId");

            migrationBuilder.CreateIndex(
                name: "IX_Sorular_KazanimId",
                table: "Sorular",
                column: "KazanimId");

            migrationBuilder.CreateIndex(
                name: "IX_Sorular_OgretmenId",
                table: "Sorular",
                column: "OgretmenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cevaplar");

            migrationBuilder.DropTable(
                name: "Secenekler");

            migrationBuilder.DropTable(
                name: "SinavSorular");

            migrationBuilder.DropTable(
                name: "Sonuclar");

            migrationBuilder.DropTable(
                name: "Sorular");

            migrationBuilder.DropTable(
                name: "Sinavlar");

            migrationBuilder.DropTable(
                name: "Kazanimlar");

            migrationBuilder.DropTable(
                name: "Kullanicilar");
        }
    }
}
