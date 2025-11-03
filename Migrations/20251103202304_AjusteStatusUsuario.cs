using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIChat.Migrations
{
    /// <inheritdoc />
    public partial class AjusteStatusUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "Usuarios",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Usuarios",
                newName: "Ativo");
        }
    }
}
