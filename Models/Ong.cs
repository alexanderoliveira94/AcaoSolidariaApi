using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcaoSolidariaApi.Models
{
    public class ONG
    {
        [Key]
        public int ?IdOng { get; set; }
        public int ?IdFotoOng { get; set; }
        public string NomeOng { get; set; }
        public string EnderecoOng { get; set; }
        public string CNPJOng { get; set; }

        [EmailAddress]
        public string EmailOng { get; set; }

        public string DescricaoOng { get; set; }

        public byte[] ?PasswordHash { get; set; }
        public byte[] ?PasswordSalt { get; set; }

        [NotMapped]
        public string SenhaOng { get; set; }

        //public Endereco Endereco { get; set; }
    }
}