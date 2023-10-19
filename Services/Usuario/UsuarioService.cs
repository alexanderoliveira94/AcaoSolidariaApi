using System.Collections.Generic;
using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Models;

namespace AcaoSolidariaApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly List<ONG> ongs = new List<ONG>();
        private readonly List<Voluntario> voluntarios = new List<Voluntario>();
        private int lastOngId = 0;
        private int lastVoluntarioId = 0;


        private readonly DataContext _context;

        public UsuarioService(DataContext context)
        {
            _context = context;
        }


        public void CriarOng(ONG ong)
        {
            ong.Id = ++lastOngId;
            ongs.Add(ong);
             _context.SaveChanges();
        }

       

        public void AtualizarOng(ONG ong)
        {
            var index = ongs.FindIndex(o => o.Id == ong.Id);
            if (index != -1)
            {
                ongs[index] = ong;
                _context.SaveChanges();
            }
        }

       

        public ONG ObterOngPorId(int id)
        {
            return ongs.Find(ong => ong.Id == id);
            
        }

      

        public void DeletarOng(int id)
        {
            var ong = ongs.Find(o => o.Id == id);
            if (ong != null)
            {
                ongs.Remove(ong);
            }
        }

       
    }
}
