using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIChat.Data;
using APIChat.Service;

namespace APIChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly RelatorioService _service;

        public RelatorioController(AppDbContext context, RelatorioService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet("gerar")]
        public async Task<ActionResult<Relatorio>> GerarRelatorio(
            [FromQuery] DateTime dataInicio, 
            [FromQuery] DateTime dataFim)
        {
            var relatorio = _service.GerarRelatorioSerivce(dataInicio, dataFim);

            if (relatorio == null)
            {
                return BadRequest("Erro ao gerar relatorio");
            }

            return Ok(relatorio);
        }
    }
}
