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
    }
}