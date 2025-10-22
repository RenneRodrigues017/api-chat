using APIChat.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Adicionar este using

namespace APIChat.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _appContext;

        public UsuarioController(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        // Use async Task<IActionResult> e ToListAsync()
        [HttpGet("RetornarUsuarios")]
        public async Task<IActionResult> RetornarUsuarios()
        {
            // O ToListAsync() é assíncrono e evita bloqueios
            var usuarios = await _appContext.Usuarios.ToListAsync();

            return Ok(usuarios);
        }

        
    }
}