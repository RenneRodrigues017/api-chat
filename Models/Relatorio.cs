
public class Relatorio
{
    public int Id { get; set; }

    public int TotalChamadosAbertos { get; set; }
    public int TotalChamadosFechados { get; set; }

    public double TempoMedioResolucaoMinutos{ get; set; } 

    public double TaxaResolucaoIA { get; set; } 

    public List<ChamadosPorCategoria> ChamadosPorCategoria { get; set; } = new List<ChamadosPorCategoria>();
}

public class ChamadosPorCategoria
{
    public int Id { get; set; }
    public string? Categoria { get; set; }
    public string? Status { get; set; }
    public int Total { get; set; }
}