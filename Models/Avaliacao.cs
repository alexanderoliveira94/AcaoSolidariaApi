using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcaoSolidariaApi.Models
{
    public class Avaliacao
    {
    public int Id { get; set; }
    public int IdTipoAvaliador { get; set; }
    public int IdAvaliador { get; set; }
    public int IdAvaliado { get; set; }
    public float NotaAvaliacao { get; set; }
    public string TipoAvaliacao { get; set; }
    public string DsAvaliacao { get; set; }
    public DateTime DtAvaliacao { get; set; }
    
    }
}