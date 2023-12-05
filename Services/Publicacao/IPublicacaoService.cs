using AcaoSolidariaApi.Models;
using System.Threading.Tasks;

public interface IPublicacaoService
{
    Task<bool> VerificarOngAutenticada(int ongId);
    Task<bool> CriarPublicacao(Publicacao publicacao);
}
