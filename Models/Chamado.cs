using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIChat.Models
{
    public enum Status
    {
        Aberto = 1,
        ResolvidoPorIA = 2,
        ResolvidoPorSuporte = 3
    }

    public enum Prioridade
    {
        Baixa = 1,
        Media = 2,
        Alta = 3
    }

    public enum Dispositivo
    {
        Teclado = 1,
        Mouse = 2,
        Monitor = 3,
        Impressora = 4,
        Outros = 5
    }
    public class Chamado
    {
        [Key]
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

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