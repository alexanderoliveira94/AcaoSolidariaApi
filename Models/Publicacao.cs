using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcaoSolidariaApi.Models
{
    public class Publicacao
    {
        public int IdPublicacao { get; set; }
        public int ProjetoAssociado { get; set; }
        public string Conteudo { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public int? OngAssociada { get; set; }
    }
}