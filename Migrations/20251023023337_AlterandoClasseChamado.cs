using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIChat.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoClasseChamado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricosChamado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoricosChamado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescricaoAcao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdChamado = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioSuporte = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosChamado", x => x.Id);
                });
        }
    }
}
