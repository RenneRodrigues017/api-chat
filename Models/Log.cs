using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIChat.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public string Acao { get; set; }
        public string Detalhes { get; set; }
        public DateTime DataHora { get; set; } = DateTime.Now;
        public string Tipo { get; set; }

        public Usuario Usuario { get; set; }
    }

}