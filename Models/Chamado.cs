using System.ComponentModel.DataAnnotations;

namespace APIChat.Models
{
    public enum Status
    {
        Aberto,
        ResolvidoPorIA,
        ResolvidoPorSuporte
    }

    public enum Prioridade
    {
        Baixa,
        Media,
        Alta
    }

    public enum Dispositivo
    {
        Teclado,
        Mouse,
        Monitor,
        Impressora,
        Outros  
    }
    public class Chamado
    {
        [Key]
        public int Id { get; set; }
        public int IdUsuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Descricao { get; set; }

        public Status Status { get; set; }

        [Required]
        public Prioridade Prioridade { get; set; }

        public DateTime DataAbertura { get; set; }

        public DateTime? DataFechamento { get; set; }

        [Required]
        public Dispositivo Dispositivo { get; set; }
    }
}