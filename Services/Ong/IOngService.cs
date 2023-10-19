using AcaoSolidariaApi.Models;

namespace AcaoSolidariaApi.Services
{
    public interface IOngService
    {
        void CriarOng(ONG ong);
        
        void AtualizarOng(ONG ong);
        
        ONG ObterOngPorId(int id);
        
        void DeletarOng(int id);
       
    }
}