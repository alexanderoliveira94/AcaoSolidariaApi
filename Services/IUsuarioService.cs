using AcaoSolidariaApi.Models;

namespace AcaoSolidariaApi.Services
{
    public interface IUsuarioService
    {
        void CriarOng(ONG ong);
        void CriarVoluntario(Voluntario voluntario);
        void AtualizarOng(ONG ong);
        void AtualizarVoluntario(Voluntario voluntario);
        ONG ObterOngPorId(int id);
        Voluntario ObterVoluntarioPorId(int id);
        void DeletarOng(int id);
        void DeletarVoluntario(int id);
    }
}
