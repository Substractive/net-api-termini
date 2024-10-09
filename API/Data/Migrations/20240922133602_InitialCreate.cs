using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usluge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vrsta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trajanje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usluge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zaposlenici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Struka = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposlenici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Termini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UslugaId = table.Column<int>(type: "int", nullable: false),
                    ZaposlenikId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Termini_Usluge_UslugaId",
                        column: x => x.UslugaId,
                        principalTable: "Usluge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Termini_Zaposlenici_ZaposlenikId",
                        column: x => x.ZaposlenikId,
                        principalTable: "Zaposlenici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Termini_UslugaId",
                table: "Termini",
                column: "UslugaId");

            migrationBuilder.CreateIndex(
                name: "IX_Termini_ZaposlenikId",
                table: "Termini",
                column: "ZaposlenikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Termini");

            migrationBuilder.DropTable(
                name: "Usluge");

            migrationBuilder.DropTable(
                name: "Zaposlenici");
        }
    }
}
