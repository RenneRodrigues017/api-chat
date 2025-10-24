using Microsoft.AspNetCore.Mvc;
using APIChat.Models; 
using APIChat.Data; 
using Microsoft.EntityFrameworkCore;

namespace APIChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha))
            {
                return BadRequest(new { Mensagem = "Email e Senha sao obrigatorios." });
            }

            var usuarioEncontrado = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == usuario.Email && u.Senha == usuario.Senha && u.Cargo == Cargo.Gerente);

            if (usuarioEncontrado == null)
            {
                throw new ArgumentNullException("Usuario nao encontrado.");
            }

            if (Cargo.Gerente != usuario.Cargo)
            {
                throw new UnauthorizedAccessException("Acesso negado. Cargo invalido.");
            }

            return Ok(new
            {
                Mensagem = "Login realizado com sucesso!"
            });
        }

        [HttpPost]
        public async Task<IActionResult> FinalizarChamado(Chamado chamado, Status status)
        {
            if (chamado == null || chamado.Id == 0)
            {
                return BadRequest(new { Mensagem = "Chamado invalido." });
            }

            var chamadoExistente = await _context.Chamados.FindAsync(chamado.Id);
            if (chamadoExistente == null)
            {
                return NotFound(new { Mensagem = "Chamado nao encontrado." });
            }

            if (chamadoExistente.Status == Status.ResolvidoPorIA || chamadoExistente.Status == Status.ResolvidoPorSuporte)
            {
                return BadRequest(new { Mensagem = "Chamado ja esta finalizado." });
            }

            chamadoExistente.Status = status;
            await _context.SaveChangesAsync();

            return Ok(new { Mensagem = "Chamado finalizado com sucesso!" });
        }
        [HttpGet("testar-conexao")]
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
        }
        [HttpPost]
        public async Task<IActionResult> LoginUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha))
            {
                return BadRequest(new { Mensagem = "Email e Senha sao obrigatorios." });
            }

            var usuarioEncontrado = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == usuario.Email && u.Senha == usuario.Senha && u.Cargo == Cargo.Usuario);

            if (Cargo.Usuario != usuario.Cargo)
            {
                return Unauthorized(new { Mensagem = "Acesso negado. Cargo invalido." });
            }   

            if (usuarioEncontrado == null)
            {
                return Unauthorized(new { Mensagem = "Usuario nao encontrado." });
            }

            return Ok(new
            {
                Mensagem = "Login realizado com sucesso!"
            });
        }
    }
}