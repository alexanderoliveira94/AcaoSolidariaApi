using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcaoSolidariaApi.Models
{
    public class InscricaoVoluntario
    {
        public int IdInscricao { get; set; }
        public DateTime? DataAprovacao { get; set; }
        public DateTime? DataInscricao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public float? AvaliacaoVoluntario { get; set; }
    }
}