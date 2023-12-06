using System;
using System.ComponentModel.DataAnnotations;

namespace AcaoSolidariaApi.Models
{
    public class Candidatura
    {
        [Key]
        public int IdCandidatura { get; set; }

        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public int IdPublicacao { get; set; }
        public Publicacao Publicacao { get; set; }

        public DateTime DataCandidatura { get; set; }
    }
}
