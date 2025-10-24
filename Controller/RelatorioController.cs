using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIChat.Data; 
using APIChat.Models;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace APIChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RelatorioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("gerar")]
        public async Task<ActionResult<Relatorio>> GerarRelatorio(
            [FromQuery] DateTime dataInicio, 
            [FromQuery] DateTime dataFim)
        {
            DateTime inicioAjustado = dataInicio.Date;
            DateTime fimAjustado = dataFim.Date;

            var chamadosNoPeriodo = await _context.Chamados
                .Where(c => c.DataAbertura.Date >= inicioAjustado && 
                            c.DataAbertura.Date <= fimAjustado)
                .ToListAsync();

            if (!chamadosNoPeriodo.Any())
            {
                return NotFound("Nenhum chamado encontrado no período.");
            }
            
            // -----------------------------------------------------
            // Lógica do Tempo Médio de Resolução
            // -----------------------------------------------------
            
            // 1. Filtrar apenas os chamados que foram resolvidos (fechados)
            var chamadosFechados = chamadosNoPeriodo
                .Where(c => c.Status == Status.ResolvidoPorIA || c.Status == Status.ResolvidoPorSuporte)
                .Where(c => c.DataFechamento.HasValue) // Garantir que a DataFechamento não é nula
                .ToList();
            
            double tempoMedioResolucaoMinutos = 0;

            if (chamadosFechados.Any())
            {
                // 2. Calcular a diferença de tempo em minutos para cada chamado fechado
                var diferencasEmMinutos = chamadosFechados
                    .Select(c => (c.DataFechamento.Value - c.DataAbertura).TotalMinutes)
                    .ToList();

                // 3. Calcular a média das diferenças
                tempoMedioResolucaoMinutos = diferencasEmMinutos.Average();
            }
            // -----------------------------------------------------

            int total = chamadosNoPeriodo.Count;
            
            int totalAbertos = chamadosNoPeriodo.Count(c => c.Status == Status.Aberto);
            
            int resolvidosIA = chamadosNoPeriodo.Count(c => c.Status == Status.ResolvidoPorIA);
            int resolvidosSuporte = chamadosNoPeriodo.Count(c => c.Status == Status.ResolvidoPorSuporte);
            int totalFechados = resolvidosIA + resolvidosSuporte;

            double taxaResolucaoIA = 0;
            if (total > 0)
            {
                taxaResolucaoIA = (double)resolvidosIA / total * 100;
            }
            
            var chamadosPorCategoria = chamadosNoPeriodo
                .GroupBy(c => c.Dispositivo.ToString())
                .Select(g => new ChamadosPorCategoria
                {
                    Categoria = g.Key,
                    Total = g.Count()
                })
                .ToList();

            var relatorio = new Relatorio
            {
                TotalChamadosAbertos = totalAbertos,
                TotalChamadosFechados = totalFechados,
                
               
                TempoMedioResolucaoMinutos = tempoMedioResolucaoMinutos, 
                
                TaxaResolucaoIA = taxaResolucaoIA,
                ChamadosPorCategoria = chamadosPorCategoria
            };

            return Ok(relatorio);
        }
    }
}
