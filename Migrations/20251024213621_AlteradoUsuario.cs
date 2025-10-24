using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIChat.Migrations
{
    /// <inheritdoc />
    public partial class AlteradoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TempoMedioResolucaoHoras",
                table: "Relatorios",
                newName: "TempoMedioResolucaoMinutos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TempoMedioResolucaoMinutos",
                table: "Relatorios",
                newName: "TempoMedioResolucaoHoras");
        }
    }
}
