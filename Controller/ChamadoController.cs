using System.Runtime.InteropServices;
using APIChat.Data;
using APIChat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace APIChat
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadoController : ControllerBase
    {
        private readonly AppDbContext _appContext;

        public ChamadoController(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        [HttpGet("RetornarChamados")]
        public async Task<IActionResult> RetornarChamados()
        {
            var chamados = await _appContext.Chamados.ToListAsync();
            return Ok(chamados);
        }

        [HttpPost("CriarChamado")]
        public async Task<IActionResult> CriarChamado([FromBody] Chamado chamado)
        {
            if (chamado == null)
            {
                return BadRequest();
            }

            await _appContext.Chamados.AddAsync(chamado);
            await _appContext.SaveChangesAsync();

            return CreatedAtAction(nameof(RetornarChamados), new { id = chamado.Id }, chamado);
        }

        [HttpGet("Filtrar")]
        public async Task<IActionResult> FiltrarChamados(Status status, Prioridade prioridade)
        {
            var chamados = await _appContext.Chamados
                .Where(c => c.Status == status && c.Prioridade == prioridade)
                .ToListAsync();

            return Ok(chamados);
        }
    }
}