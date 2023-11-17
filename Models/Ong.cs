using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AcaoSolidariaApi.Models
{
    public class ONG
    {
        [Key]
        public int ?IdOng { get; set; }

        [JsonIgnore]
        public int ?IdFotoOng { get; set; }  
        public string NomeOng { get; set; } = string.Empty;
        [JsonIgnore]
        public string EnderecoOng { get; set; } = string.Empty;
        public string CNPJOng { get; set; } = string.Empty;

        [JsonIgnore]
        public DateTime? DataRegistro { get; set; }

        [EmailAddress]
        public string EmailOng { get; set; }

        public string DescricaoOng { get; set; } = string.Empty;

        [JsonIgnore]
        public byte[] ?PasswordHash { get; set; }

        [JsonIgnore]
        public byte[] ?PasswordSalt { get; set; }

        [NotMapped]
        public string SenhaOng { get; set; } = string.Empty;

        //public Endereco Endereco { get; set; }
    }
}