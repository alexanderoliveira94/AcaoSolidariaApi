using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcaoSolidariaApi.Models
{
    public class ONG
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string CNPJ { get; set; }
        public string Descricao { get; set; }
        public string Senha { get; set; }
    }
}