using System.Threading.Tasks;
using AcaoSolidariaApi.Models;
using AcaoSolidariaApi.Data;
using Microsoft.EntityFrameworkCore;

namespace AcaoSolidariaApi.Services
{
    public class OngService : IOngService
    {
        private readonly DataContext _context;

        public OngService(DataContext context)
        {
            _context = context;
        }

        public async Task RegistrarOng(ONG ong)
        {
            _context.ONGs.Add(ong);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarOng(ONG ong)
        {
            _context.ONGs.Add(ong);
            await _context.SaveChangesAsync();
        }


        public ONG ObterOngPorId(int id)
        {
            return _context.ONGs.FirstOrDefault(u => u.IdOng == id);
        }

        public async Task DeletarOng(int id)
        {
            var ong = await _context.ONGs.FindAsync(id);
            if (ong != null)
            {
                _context.ONGs.Remove(ong);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<ONG> ListarOngs()
        {
            return _context.ONGs.ToList();
        }



    }
}
