
using System.ComponentModel.DataAnnotations;

namespace APIChat.Models
{
    public class HistoricoChamado
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int IdChamado { get; set; }
        [Required]
        public int IdUsuarioSuporte { get; set; }
        public DateTime Data { get; set; }
        [Required]
        [MaxLength(100)]
        public string? DescricaoAcao { get; set; }
    }
}