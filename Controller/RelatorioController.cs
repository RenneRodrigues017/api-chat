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
        var chamadosNoPeriodo = _context.Chamados
            .Where(c => c.DataAbertura >= dataInicio && c.DataAbertura <= dataFim)
            .ToList();

        if (!chamadosNoPeriodo.Any())
            return NotFound("Nenhum chamado encontrado no período.");

        int total = chamadosNoPeriodo.Count;
        int resolvidosIA = chamadosNoPeriodo.Count(c => c.Status == Status.ResolvidoPorIA);

        var relatorio = new Relatorio
        {
            TotalChamadosAbertos = chamadosNoPeriodo.Count(c => c.Status == Status.Aberto),
            TotalChamadosFechados = chamadosNoPeriodo.Count(c => c.Status == Status.ResolvidoPorSuporte || c.Status == Status.ResolvidoPorIA),
            TempoMedioResolucaoHoras = 0, // deixa 0 para simplificar
            TaxaResolucaoIA = (double)resolvidosIA / total * 100,
            ChamadosPorCategoria = new List<ChamadosPorCategoria>() // vazio, opcional
        };

        return Ok(relatorio);
    }

}