using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AcaoSolidariaApi.Models
{
    public class Publicacao
    {
        [Key]
        public int IdPublicacao { get; set; }
        public int ProjetoAssociado { get; set; } = 0;
        public string Conteudo { get; set; } = string.Empty;

        [JsonIgnore]
        public DateTime? DataPublicacao { get; set; }
        public int OngAssociada { get; set; }

        // Adicione a propriedade de navegação para a lista de candidaturas associadas a esta publicação
        public List<Candidatura> Candidaturas { get; set; }
    }
}
