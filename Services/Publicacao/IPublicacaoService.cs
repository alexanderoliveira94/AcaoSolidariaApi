using AcaoSolidariaApi.Models;
using System.Threading.Tasks;

public interface IPublicacaoService
{
    Task<bool> VerificarOngAutenticada(int ongId);
    Task<bool> CriarPublicacao(Publicacao publicacao);

    Task<bool> VerificarCandidaturaExistente(int usuarioId, int publicacaoId);
    Task<bool> CandidatarUsuarioAoProjeto(int usuarioId, int publicacaoId);
}
