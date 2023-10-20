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
            var ongExistente = _context.ONGs.FirstOrDefault(v => v.Id == ong.Id);

            if (ongExistente != null)
            {
                ongExistente.Nome = ong.Nome;
                ongExistente.Endereco = ong.Endereco;
                ongExistente.CNPJ = ong.CNPJ;
                ongExistente.Descricao = ong.Descricao;
                ongExistente.Senha = ong.Senha;


                _context.ONGs.Update(ongExistente);
                _context.SaveChanges();
            }
        }



         public ONG ObterOngPorId(int id)
        {
            var ong = _context.ONGs.FirstOrDefault(v => v.Id == id);
            if (ong == null)
            {
                throw new Exception($"Voluntário com o ID {id} não encontrado.");
            }
            return ong;
        }



         public void DeletarOng(int id)
        {
            var ong = _context.ONGs.FirstOrDefault(v => v.Id == id);
            if (ong != null)
            {
                _context.ONGs.Remove(ong);
            }
        }


    }
}
