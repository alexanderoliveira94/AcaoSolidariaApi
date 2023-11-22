using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcaoSolidariaApi.Models
{
    public class TiposAvaliacao
    {
        public int IdTipoAvaliador { get; set; }
        public string DsTipoAvaliador { get; set; }
        public int TpAvaliado { get; set; }

        public string TesteGit { get; set; };
    }
}