using AcaoSolidariaApi.Models;

namespace AcaoSolidariaApi.Services
{
    public interface IVoluntarioService
    {
        
        void CriarVoluntario(Voluntario voluntario);
        
        void AtualizarVoluntario(Voluntario voluntario);
        
        Voluntario ObterVoluntarioPorId(int id);
        
        void DeletarVoluntario(int id);
    }
}
