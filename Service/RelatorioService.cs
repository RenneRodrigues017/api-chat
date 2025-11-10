using System.Data;
using APIChat.Data;
using APIChat.Models;
namespace APIChat.Service
{
    public class RelatorioService
    {
        private readonly AppDbContext _context;
        public RelatorioService(AppDbContext context)
        {
            _context = context;
            
        }
        public Relatorio GerarRelatorioSerivce(DateTime dataInicio, DateTime dataFim )
        {
            DateTime inicioAjustado = dataInicio.Date;
            DateTime fimAjustado = dataFim.Date;

            var chamadosNoPeriodo =  _context.Chamados
                .Where(c => c.DataAbertura.Date >= inicioAjustado && 
                            c.DataAbertura.Date <= fimAjustado)
                .ToList();

            if (!chamadosNoPeriodo.Any())
            {
                return null; ;
            }
            var chamadosFechados = chamadosNoPeriodo
                .Where(c => c.Status == Status.ResolvidoPorIA || c.Status == Status.ResolvidoPorSuporte)
                .Where(c => c.DataFechamento.HasValue) 
                .ToList();
            
            double tempoMedioResolucaoMinutos = 0;

            if (chamadosFechados.Any())
            {
                var diferencasEmMinutos = chamadosFechados
                    .Select(c => (c.DataFechamento.Value - c.DataAbertura).TotalMinutes)
                    .ToList();

                tempoMedioResolucaoMinutos = diferencasEmMinutos.Average();
            }

            int total = chamadosNoPeriodo.Count;
            
            int totalAbertos = chamadosNoPeriodo.Count(c => c.Status == Status.Aberto);
            
            int resolvidosIA = chamadosNoPeriodo.Count(c => c.Status == Status.ResolvidoPorIA);
            int resolvidosSuporte = chamadosNoPeriodo.Count(c => c.Status == Status.ResolvidoPorSuporte);
            int totalFechados = resolvidosIA + resolvidosSuporte;

            double taxaResolucaoIA = 0;
            if (total > 0)
            {
                taxaResolucaoIA = (double)resolvidosIA / total * 100;
            }
            
            var chamadosPorCategoria = chamadosNoPeriodo
                .GroupBy(c => new { c.Dispositivo, c.Status })
                .Select(g => new ChamadosPorCategoria
                {
                    Categoria = g.Key.Dispositivo.ToString(),
                    Status = g.Key.Status.ToString(),
                    Total = g.Count()
                })
                .ToList();

            var relatorio = new Relatorio
            {
                TotalChamadosAbertos = totalAbertos,
                TotalChamadosFechados = totalFechados,
                
               
                TempoMedioResolucaoMinutos = tempoMedioResolucaoMinutos, 
                
                TaxaResolucaoIA = taxaResolucaoIA,
                ChamadosPorCategoria = chamadosPorCategoria
            };

            return relatorio;
        }
    }
}