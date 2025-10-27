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

        public LoginService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ValidarLogin(string email, string senha)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);

            if (usuario == null)
            {
                return null;
            }

            if (usuario.Cargo == Cargo.Gerente || usuario.Cargo == Cargo.Suporte)
            {
                return usuario;
            }

            return usuario;
        }

        public async Task<Usuario> ValidarLoginUsuario(string email, string senha)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha && u.Cargo == Cargo.Usuario);

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