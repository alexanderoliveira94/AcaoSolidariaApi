using AcaoSolidariaApi.Models;
using System.Threading.Tasks;

namespace AcaoSolidariaApi.Services
{
    public interface IUsuarioService
    {
        Task RegistrarUsuario(Usuario usuario);
        void AtualizarUsuario(Usuario usuario);
        Usuario ObterUsuarioPorId(int id);
        Task DeletarUsuario(int id);
    }
}
