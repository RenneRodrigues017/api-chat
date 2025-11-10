using APIChat.Data;
using APIChat.Models;

namespace APIChat.Service
{
    public class LogService
{
    private readonly AppDbContext _context;

    public LogService(AppDbContext context)
    {
        _context = context;
    }

    public async Task RegistrarAsync(int? usuarioId, string acao, string detalhes, string tipo = "Ação")
    {
        var log = new Log
        {
            UsuarioId = usuarioId,
            Acao = acao,
            Detalhes = detalhes,
            Tipo = tipo
        };

        _context.Logs.Add(log);
        await _context.SaveChangesAsync();
    }
}

}