using APIChat.Data;
using APIChat.Models;
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

        [HttpPost("CadastrarUsuario")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest(new { Mensagem = "Dados do usuário inválidos." });
            }

            try
            {
                await _usuarioService.CadastrarUsuario(usuario);
            }
            catch (InvalidOperationException)
            {
                return Conflict(new { Mensagem = "Email já cadastrado." });
            }

            return CreatedAtAction(nameof(RetornarUsuarios), new { id = usuario.Id }, usuario);
        }

        [HttpDelete("ExcluirUsuario/{id}")]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            var usuarioExcluido = await _usuarioService.ExcluirUsuario(id);
           if (usuarioExcluido == false || !usuarioExcluido)
            {
                return NotFound(new { Mensagem = "Usuário não encontrado." });
            }

            return Ok(new { Mensagem = "Usuário excluído com sucesso." });
        }
    }
}