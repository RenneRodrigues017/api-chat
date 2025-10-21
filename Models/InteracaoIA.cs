
namespace APIChat.Models
{
    public enum Origem
    {
        Usuario,
        Sistema,
        IA
    }
    public class InteracaoIA
    {
        public int Id { get; set; }
        public int IdChamado { get; set; }
        public string? Mensagem { get; set; }
        public Origem Origem { get; set; }
        public DateTime DataEnvio { get; set; }
    }
}