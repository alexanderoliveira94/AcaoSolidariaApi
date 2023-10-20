using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcaoSolidariaApi.Models
{
    public class Projeto
    {
        public int IdProjeto { get; set; }
        public int IdInscricao { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public int? CapacidadeMaxima { get; set; }
        public float? AvaliacaoMedia { get; set; }
    }
}