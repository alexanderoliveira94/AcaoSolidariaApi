using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcaoSolidariaApi.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public int ?IdFotoUsuario { get; set; }
        public string Nome { get; set; }

        public byte[] ?PasswordHash { get; set; }
        public byte[] ?PasswordSalt { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [NotMapped] 
        public string SenhaUsuario { get; set; }
        public DateTime? DataRegistro { get; set; }
        public string DescricaoHabilidades { get; set; }
        public float? AvaliacaoMedia { get; set; }
    }
}