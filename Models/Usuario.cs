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
        
        public string Nome { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [JsonIgnore]
        public DateTime? DataRegistro { get; set; }

        public string DescricaoHabilidades { get; set; } = string.Empty;

        [JsonIgnore]
        public float? AvaliacaoMedia { get; set; }

        [JsonIgnore]
        public int? IdFotoUsuario { get; set; }

        [NotMapped] 
        public string SenhaUsuario { get; set; } = string.Empty;

        [JsonIgnore]
        public byte[]? PasswordHash { get; set; }
        
        [JsonIgnore]
        public byte[]? PasswordSalt { get; set; }

        [NotMapped]
        public string ?Token { get; set; }
    }
}
