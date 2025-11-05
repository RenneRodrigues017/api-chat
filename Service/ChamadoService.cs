using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using APIChat.Data;
using APIChat.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace APIChat.Service
{
    public class ChamadoService
    {
        private readonly AppDbContext _context;

        public ChamadoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Chamado>> RetornarChamados()
        {
            return await _context.Chamados
                        .Include(c => c.Usuario)
                        .ToListAsync();
        }

        public async Task<IResult> CriarChamado(Chamado chamado)
        {
            var brasilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Eastern Standard Time");
            chamado.DataAbertura = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brasilTimeZone);     
            chamado.Status = Status.Aberto;
            await _context.Chamados.AddAsync(chamado);
            await _context.SaveChangesAsync();
            return Results.Ok();
        }

        public async Task<List<Chamado>> FiltrarChamados(Status status, Prioridade prioridade)
        {
            return await _context.Chamados
                .Include(c => c.Usuario)
                .Where(c => c.Status == status && c.Prioridade == prioridade)
                .ToListAsync();
        }

        public async Task<Chamado> FinalizarChamado(Chamado chamado)
        {
            var chamadoExistente = await _context.Chamados
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Id == chamado.Id);
            if (chamadoExistente == null)
            {
                return null;
            }

            chamadoExistente.Status = Status.ResolvidoPorSuporte;
            chamadoExistente.DataFechamento = DateTime.UtcNow;

            _context.Chamados.Update(chamadoExistente);
            await _context.SaveChangesAsync();

            return chamadoExistente;
        }

        public async Task<Chamado> FinalizarChamadoUsuario(Chamado chamado)
        {
            var chamadoExistente = await _context.Chamados
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Id == chamado.Id);
            if (chamadoExistente == null)
            {
                return null;
            }

            chamadoExistente.Status = Status.ResolvidoPorIA;
            chamadoExistente.DataFechamento = DateTime.UtcNow;

            _context.Chamados.Update(chamadoExistente);
            await _context.SaveChangesAsync();

            return chamadoExistente;
        }
    }

}