using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AcaoSolidariaApi.Models
{
    public class Usuario
    {
        [Key]
        [JsonIgnore]
        public int IdUsuario { get; set; }
        
        public string Nome { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [JsonIgnore]
        public DateTime? DataRegistro { get; set; }

        public string DescricaoHabilidades { get; set; }

        [JsonIgnore]
        public float? AvaliacaoMedia { get; set; }

        [JsonIgnore]
        public int? IdFotoUsuario { get; set; }

        [NotMapped] 
        public string SenhaUsuario { get; set; }

        [JsonIgnore]
        public byte[]? PasswordHash { get; set; }
        
        [JsonIgnore]
        public byte[]? PasswordSalt { get; set; }

        [NotMapped]
        public string ?Token { get; set; }
    }
}
