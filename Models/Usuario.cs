
using System.Text.Json.Serialization;

namespace APIChat.Models
{
    public enum Cargo
    {
        Usuario = 1,
        Suporte = 2,
        Gerente = 3
    }

    public enum StatusUsuario
    {
        Inativo = 0,
        Ativo = 1
    }
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }   
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public DateTime DataCadastro { get; set; }  
        public Cargo Cargo { get; set; }
        [JsonPropertyName("Ativo")]
        public StatusUsuario Status { get; set; }
        public ICollection<Chamado> Chamados { get; set; } = new List<Chamado>();
    }
}