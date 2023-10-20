using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcaoSolidariaApi.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int IdFoto { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime? DataRegistro { get; set; }
        public string Habilidades { get; set; }
        public float? AvaliacaoMedia { get; set; }
    }
}