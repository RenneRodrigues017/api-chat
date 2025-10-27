using System.Runtime.InteropServices;
using APIChat.Data;
using APIChat.Models;
using APIChat.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace APIChat
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadoController : ControllerBase
    {
        private readonly ChamadoService _chamadoService;
        
        public ChamadoController( ChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
        }

        [HttpGet("RetornarChamados")]
        public async Task<IActionResult> RetornarChamados()
        {
            var chamados = await _chamadoService.RetornarChamados();
            return Ok(chamados);
        }

        [HttpPost("CriarChamado")]
        public async Task<IActionResult> CriarChamado([FromBody] Chamado chamado)
        {
            if (chamado == null)
            {
                return BadRequest();
            }

            await _chamadoService.CriarChamado(chamado);
            return CreatedAtAction(nameof(RetornarChamados), new { id = chamado.Id }, chamado);
        }

        [HttpGet("Filtrar")]
        public async Task<IActionResult> FiltrarChamados(Status status, Prioridade prioridade)
        {
            var chamados = await _chamadoService.FiltrarChamados(status, prioridade);
            return Ok(chamados);
        }


        [HttpPut("FinalizarChamado")]
        public async Task<IActionResult> FinalizarChamado([FromBody] Chamado chamado)
        {
            var resultado = await _chamadoService.FinalizarChamado(chamado);

            if (resultado == null)
            {
                return NotFound(new { Mensagem = "Chamado não encontrado." });
            }

            return Ok(new { Mensagem = "Chamado finalizado com sucesso!" });
        }
        /*[HttpGet("testar-conexao")]
        public IActionResult TestarConexao()
        {
            try
            {
                int totalUsuarios = _context.Usuarios.Count();
                return Ok(new { Mensagem = "Conexão OK!", TotalUsuarios = totalUsuarios });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro na conexão", Detalhes = ex.Message });
            }
        }*/
    }
}