// Projeto API (Models/RelatorioResumo.cs)

public class Relatorio
{
    public int Id { get; set; }
    // Métricas de Volume
    public int TotalChamadosAbertos { get; set; }
    public int TotalChamadosFechados { get; set; }
    
    // Métricas de Desempenho
    public double TempoMedioResolucaoMinutos{ get; set; } // MTTR
    
    // Métricas da IA
    public double TaxaResolucaoIA { get; set; } // % de chamados resolvidos pela IA
    
    // Lista de dados para um Gráfico (Ex: Chamados por Categoria)
    public List<ChamadosPorCategoria> ChamadosPorCategoria { get; set; } = new List<ChamadosPorCategoria>();
}

public class ChamadosPorCategoria
{
    public int Id { get; set; }
    public string Categoria { get; set; }
    public int Total { get; set; }
}