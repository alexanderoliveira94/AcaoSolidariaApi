using System.Collections.Generic;
using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Models;

namespace AcaoSolidariaApi.Services
{
    public class VoluntarioService : IVoluntarioService
    {
        private readonly List<ONG> ongs = new List<ONG>();
        private readonly List<Voluntario> voluntarios = new List<Voluntario>();
        private int lastOngId = 0;
        private int lastVoluntarioId = 0;


        private readonly DataContext _context;

        public VoluntarioService(DataContext context)
        {
            _context = context;
        }


        public void CriarVoluntario(Voluntario voluntario)
        {
            voluntario.Id = ++lastVoluntarioId;
            voluntarios.Add(voluntario);
            _context.SaveChanges();
        }

      
        public void AtualizarVoluntario(Voluntario voluntario)
        {
            var index = voluntarios.FindIndex(v => v.Id == voluntario.Id);
            if (index != -1)
            {
                voluntarios[index] = voluntario;
                _context.SaveChanges();
            }
        }

      

        public Voluntario ObterVoluntarioPorId(int id)
        {
            return voluntarios.Find(voluntario => voluntario.Id == id);
        }

       

        public void DeletarVoluntario(int id)
        {
            var voluntario = voluntarios.Find(v => v.Id == id);
            if (voluntario != null)
            {
                voluntarios.Remove(voluntario);
            }
        }
    }
}
