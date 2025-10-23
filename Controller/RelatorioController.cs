using Microsoft.AspNetCore.Mvc;
using APIChat.Data; 
using APIChat.Models;
using Microsoft.EntityFrameworkCore;
namespace APIChat.Controllers;

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
        // 1. Aplicar o filtro de data
        var chamadosNoPeriodo = _context.Chamados
            .Where(c => c.DataAbertura >= dataInicio && c.DataAbertura <= dataFim)
            .ToList(); 
        if (!chamadosNoPeriodo.Any())
        {
            return NotFound("Nenhum chamado encontrado no período.");
        }

        // 2. CÁLCULO DAS MÉTRICAS (Exemplo simples)
        var total = chamadosNoPeriodo.Count;
        var resolvidosIA = chamadosNoPeriodo.Count(c => c.Status == Status.ResolvidoPorIA);
        
        var relatorio = new Relatorio
        {
            TotalChamadosAbertos = total,
            TotalChamadosFechados = chamadosNoPeriodo.Count(c => c.Status == Status.ResolvidoPorSuporte || c.Status == Status.ResolvidoPorIA),
            TaxaResolucaoIA = (double)resolvidosIA / total * 100,
        };

        return Ok(relatorio);
    }
}