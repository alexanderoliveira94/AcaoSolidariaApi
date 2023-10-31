using System.Collections.Generic;
using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Models;
using System.Linq;

namespace AcaoSolidariaApi.Services
{
    public class OngService : IOngService
    {
        private readonly List<ONG> ongs = new List<ONG>();

        private readonly DataContext _context;

        public OngService(DataContext context)
        {
            _context = context;
        }


        public void CriarOng(ONG ong)
        {

            _context.ONGs.Add(ong);
            _context.SaveChanges();
        }



         public void AtualizarOng(ONG ong)
        {
            _context.ONGs.Update(ong);
            _context.SaveChanges();
        }



         public ONG ObterOngPorId(int id)
        {
            var ong = _context.ONGs.FirstOrDefault(v => v.IdOng == id);
            if (ong == null)
            {
                throw new Exception($"Voluntário com o ID {id} não encontrado.");
            }
            return ong;
        }



         public void DeletarOng(int id)
        {
            var ong = _context.ONGs.FirstOrDefault(v => v.IdOng == id);
            if (ong != null)
            {
                _context.ONGs.Remove(ong);
            }
        }


    }
}
