using Microsoft.AspNetCore.Mvc;
using APIChat.Models; 
using APIChat.Data; 
using Microsoft.EntityFrameworkCore;
using APIChat.Service;

namespace APIChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService  _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha))
            {
                return BadRequest(new { Mensagem = "Email e Senha são obrigatórios." });
            }

            try
            {
                var usuarioEncontrado = await _loginService.ValidarLogin(usuario.Email, usuario.Senha);

                if (usuarioEncontrado == null)
                {
                    return Unauthorized(new { Mensagem = "Email ou Senha incorretos. Tente novamente." });
                }

                return Ok(new
                {
                    Mensagem = "Login realizado com sucesso!",
                    Cargo = usuarioEncontrado.Cargo
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Mensagem = ex.Message }); 
            }
            catch (Exception)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro interno e inesperado no login." });
            }
        }

        [HttpPost("LoginUsuario")]
        public async Task<IActionResult> LoginUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha))
            {
                return BadRequest(new { Mensagem = "Email e Senha sao obrigatorios." });
            }

            var usuarioEncontrado = await _loginService.ValidarLoginUsuario(usuario.Email, usuario.Senha);

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