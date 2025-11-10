using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIChat.Data;
using APIChat.Models;
using Microsoft.EntityFrameworkCore;

namespace APIChat.Service
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;
        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> RetornarUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task CadastrarUsuario(Usuario usuario)
        {
            if (_context.Usuarios.Any(u => u.Email == usuario.Email))
            {
                throw new InvalidOperationException("Email já cadastrado.");
            }

            usuario.Status = StatusUsuario.Ativo;

            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExcluirUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                usuario.Status = StatusUsuario.Inativo;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        } 
    }
}