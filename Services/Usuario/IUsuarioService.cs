using AcaoSolidariaApi.Models;

namespace AcaoSolidariaApi.Services
{
    public interface IUsuarioService
    {
        void CriarUsuario(Usuario usuario);
        void AtualizarUsuario(Usuario usuario);
        Usuario ObterUsuarioPorId(int id);
        void DeletarUsuario(int id);

    }
}
