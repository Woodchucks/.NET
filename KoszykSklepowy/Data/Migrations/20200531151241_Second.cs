using Microsoft.EntityFrameworkCore.Migrations;

namespace Labo7_vol2.Data.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProduktWKoszyku",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProduktId = table.Column<int>(nullable: true),
                    Ilosc = table.Column<int>(nullable: false),
                    PrzepisId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduktWKoszyku", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProduktWKoszyku_Produkt_ProduktId",
                        column: x => x.ProduktId,
                        principalTable: "Produkt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProduktWKoszyku_Produkty_PrzepisId",
                        column: x => x.PrzepisId,
                        principalTable: "Produkty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProduktWKoszyku_ProduktId",
                table: "ProduktWKoszyku",
                column: "ProduktId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduktWKoszyku_PrzepisId",
                table: "ProduktWKoszyku",
                column: "PrzepisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduktWKoszyku");
        }
    }
}
