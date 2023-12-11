using System.ComponentModel.DataAnnotations;


namespace AcaoSolidariaApi.Models
{
    public class Candidatura
    {
        [Key]
        public int IdCandidatura { get; set; }
        
        public int IdPublicacao { get; set; }

        public int IdUsuario { get; set; }

        public DateTime DataCandidatura { get; set; }
    }
}
