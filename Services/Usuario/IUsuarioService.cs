using AcaoSolidariaApi.Models;
using System.Threading.Tasks;

namespace AcaoSolidariaApi.Services
{
    public interface IUsuarioService
    {
        Task RegistrarUsuario(Usuario usuario);
        Task AtualizarUsuario(Usuario usuario);
        Usuario ObterUsuarioPorId(int id);
        Task DeletarUsuario(int id);
    }

}