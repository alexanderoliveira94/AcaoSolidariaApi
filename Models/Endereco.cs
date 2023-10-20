using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcaoSolidariaApi.Models
{
    public class Endereco
    {
    public int CEP { get; set; }
    public string Logradouro { get; set; }
    public int Numero { get; set; }
    public string Regiao { get; set; }
    public string UF { get; set; }
    public string EnderecoDetalhado  { get; set; }
    public string Bairro { get; set; }
    }
}