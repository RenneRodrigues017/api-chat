using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIChat.Migrations
{
    /// <inheritdoc />
    public partial class AjusteUsuarioEChamado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Usuarios_NomeDoUsuarioId",
                table: "Chamados");

            migrationBuilder.RenameColumn(
                name: "NomeDoUsuarioId",
                table: "Chamados",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Chamados_NomeDoUsuarioId",
                table: "Chamados",
                newName: "IX_Chamados_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Usuarios_UsuarioId",
                table: "Chamados",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Usuarios_UsuarioId",
                table: "Chamados");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Chamados",
                newName: "NomeDoUsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Chamados_UsuarioId",
                table: "Chamados",
                newName: "IX_Chamados_NomeDoUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Usuarios_NomeDoUsuarioId",
                table: "Chamados",
                column: "NomeDoUsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
