using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Models;
using Microsoft.EntityFrameworkCore;
using AcaoSolidariaApi.Services;
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
            // Verificar se a ONG associada é válida
            if (!await VerificarOngAutenticada(publicacao.OngAssociada))
            {
                return false;
            }

            // Adicione a lista de candidaturas à publicação
            // publicacao.Candidaturas = new List<Candidatura>();
            publicacao.DataInicio = DateTime.Now;
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

    

    public async Task<bool> VerificarCandidaturaExistente(int usuarioId, int publicacaoId)
    {
        return await _context.Candidaturas.AnyAsync(c => c.IdUsuario == usuarioId && c.IdPublicacao == publicacaoId);
    }

    public async Task<bool> CandidatarUsuarioAoProjeto(int usuarioId, int publicacaoId)
    {
        try
        {
            // Verificar se a publicação existe (adicione essa verificação se necessário)
            var publicacaoExistente = await _context.Publicacoes.FindAsync(publicacaoId);

            if (publicacaoExistente == null)
            {
                return false; // Ou adote outra lógica conforme necessário
            }

            // Candidatar o usuário ao projeto
            var candidatura = new Candidatura
            {
                IdUsuario = usuarioId,
                IdPublicacao = publicacaoId,
                DataCandidatura = DateTime.Now
            };

            _context.Candidaturas.Add(candidatura);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            // Lida com a exceção conforme necessário
            return false;
        }    
    }

    
}

