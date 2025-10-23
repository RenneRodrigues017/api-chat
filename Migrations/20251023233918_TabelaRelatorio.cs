using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIChat.Migrations
{
    /// <inheritdoc />
    public partial class TabelaRelatorio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relatorios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalChamadosAbertos = table.Column<int>(type: "int", nullable: false),
                    TotalChamadosFechados = table.Column<int>(type: "int", nullable: false),
                    TempoMedioResolucaoHoras = table.Column<double>(type: "float", nullable: false),
                    PorcentagemChamadosForaSLA = table.Column<double>(type: "float", nullable: false),
                    TaxaResolucaoIA = table.Column<double>(type: "float", nullable: false),
                    TaxaEscaladaHumano = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatorios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChamadosPorCategoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    RelatorioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChamadosPorCategoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChamadosPorCategoria_Relatorios_RelatorioId",
                        column: x => x.RelatorioId,
                        principalTable: "Relatorios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChamadosPorCategoria_RelatorioId",
                table: "ChamadosPorCategoria",
                column: "RelatorioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChamadosPorCategoria");

            migrationBuilder.DropTable(
                name: "Relatorios");
        }
    }
}
