using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class PublicacaoService : IPublicacaoService
{
    private readonly DataContext _context;

    public PublicacaoService(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> VerificarOngAutenticada(int ongId)
    {
        return await _context.ONGs.AnyAsync(x => x.IdOng == ongId);
    }

    public async Task<bool> CriarPublicacao(Publicacao publicacao)
    {
        try
        {
            publicacao.DataPublicacao = DateTime.Now;

            _context.Publicacoes.Add(publicacao);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            // Lida com a exceção ou loga conforme necessário
            return false;
        }
    }
}
