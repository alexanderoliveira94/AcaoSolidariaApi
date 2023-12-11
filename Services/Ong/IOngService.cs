using AcaoSolidariaApi.Models;

namespace AcaoSolidariaApi.Services
{
    public interface IOngService
    {


        Task RegistrarOng(ONG ong);
        Task AtualizarOng(ONG ong);
        ONG ObterOngPorId(int id);
        Task DeletarOng(int id);
         IEnumerable<ONG> ListarOngs();
    }

}
