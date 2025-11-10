using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIChat.Data;
using APIChat.Models;
using Microsoft.EntityFrameworkCore;

namespace APIChat.Service
{
    public class LoginService
    {
        private readonly AppDbContext _context;
        private readonly LogService _logService;

        public LoginService(AppDbContext context, LogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<Usuario> ValidarLogin(string email, string senha)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);

                await _logService.RegistrarAsync(
                    usuarioId: usuario?.Id,
                    acao: "Criou um chamado",
                    detalhes: $"Usuario com o Nome: {usuario?.Nome} fez login no sistema, data e hora: {DateTime.UtcNow}"
                );

            if (usuario == null)
            {
                return null;
            }

            if (usuario.Cargo == Cargo.Gerente || usuario.Cargo == Cargo.Suporte)
            {
                return usuario;
            }

            throw new UnauthorizedAccessException("Usuário não autorizado. Cargo inválido.");
        }

        public async Task<Usuario> ValidarLoginUsuario(string email, string senha)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha && u.Cargo == Cargo.Usuario);
                await _logService.RegistrarAsync(
                    usuarioId: usuario?.Id,
                    acao: "Fazer Login",
                    detalhes: $"Usuario com o Nome: {usuario?.Nome} fez login no sistema, data e hora: {DateTime.UtcNow}"
                );

            if (usuario == null)
            {
                return null;
            }

            if (usuario.Cargo == Cargo.Usuario)
            {
                return usuario;
            }
            return usuario;
        }

    }
}