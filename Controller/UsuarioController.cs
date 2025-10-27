using APIChat.Data;
using APIChat.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Adicionar este using

namespace APIChat.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("RetornarUsuarios")]
        public async Task<IActionResult> RetornarUsuarios()
        {
            var usuarios = await _usuarioService.RetornarUsuarios();

            if (usuarios == null || !usuarios.Any())
            {
                return NotFound(new { Mensagem = "Nenhum usuário encontrado." });
            }

            return Ok(usuarios);
        }

        
    }
}