using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AcaoSolidariaApi.Models
{
    public class Publicacao
    {
        [Key]
        public int IdPublicacao { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        [JsonIgnore]
        public DateTime DataPublicacao { get; set; }
        
        [JsonIgnore]
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int VagasDisponiveis { get; set; }
        public string Local { get; set; }
        public int OngAssociada { get; set; }
        
    }
}
