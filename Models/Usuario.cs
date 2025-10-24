
namespace APIChat.Models
{
    public enum Cargo
    {
        Usuario = 1,
        Suporte = 2,
        Gerente = 3
    }
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }   
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public DateTime DataCadastro { get; set; }  
        public Cargo Cargo { get; set; }
    }
}