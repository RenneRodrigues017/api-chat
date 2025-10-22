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
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Senha))
            {
              return BadRequest(new { Mensagem = "Email e Senha sao obrigatorios." });
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == login.Email && u.Senha == login.Senha);

            if (usuario == null)
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