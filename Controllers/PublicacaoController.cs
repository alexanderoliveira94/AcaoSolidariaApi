using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Models;
using Microsoft.AspNetCore.Mvc;
using AcaoSolidariaApi.Services;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


namespace AcaoSolidariaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicacaoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IPublicacaoService _publicacaoService;


        public PublicacaoController(DataContext context, IPublicacaoService publicacaoService)
        {
            _context = context;
            _publicacaoService = publicacaoService;

        }

        [HttpPost("criarPublicacao")]
        public async Task<IActionResult> CriarPublicacao([FromBody] Publicacao publicacao)
        {
            try
            {
                // Validar se a ONG autenticada é válida (opcional dependendo da sua lógica)
                // Aqui, estou apenas assumindo que a ONG está sendo enviada no request
                if (publicacao.OngAssociada <= 0)
                {
                    return BadRequest("OngAssociada é obrigatório.");
                }

                // Validar datas
                if (publicacao.DataFim <= publicacao.DataInicio)
                {
                    return BadRequest("A data de término deve ser posterior à data de início.");
                }

                // Criar a publicação
                var criarPublicacao = new Publicacao
                {
                    Titulo = publicacao.Titulo,
                    Descricao = publicacao.Descricao,
                    DataInicio = System.DateTime.Now,
                    DataFim = publicacao.DataFim,
                    VagasDisponiveis = publicacao.VagasDisponiveis,
                    Local = publicacao.Local,
                    OngAssociada = publicacao.OngAssociada,
                    DataPublicacao = System.DateTime.Now,
                };
                publicacao.DataPublicacao = System.DateTime.Now;
                publicacao.DataInicio = System.DateTime.Now;
                _context.Publicacoes.Add(publicacao);
                await _context.SaveChangesAsync();

                return Ok(new { Mensagem = "Publicação criada com sucesso.", IdPublicacao = publicacao.IdPublicacao });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = "Ocorreu um erro interno no servidor.", ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
            }
        }



        [HttpGet("listarPublicacoes")]
        public ActionResult<IEnumerable<Publicacao>> ListarPublicacoes()
        {
            try
            {
                var publicacoes = _context.Publicacoes.ToList();
                return Ok(publicacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = "Ocorreu um erro interno no servidor.", ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpGet("obterPublicacao/{idPublicacao}")]
        public ActionResult<Publicacao> ObterPublicacao(int idPublicacao)
        {
            try
            {
                var publicacao = _context.Publicacoes.FirstOrDefault(p => p.IdPublicacao == idPublicacao);

                if (publicacao == null)
                {
                    return NotFound("Publicação não encontrada.");
                }

                return Ok(publicacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = "Ocorreu um erro interno no servidor.", ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpPost("candidatarProjeto")]
        public async Task<IActionResult> CandidatarProjeto(int idUsuario, int idPublicacao)
        {
            try
            {
                // Verificar se o usuário já está associado a esta publicação
                var candidaturaExistente = await _context.Candidaturas
                    .AnyAsync(c => c.IdUsuario == idUsuario && c.IdPublicacao == idPublicacao);

                if (candidaturaExistente)
                {
                    return BadRequest("Usuário já candidatou-se a este projeto.");
                }

                // Verificar se há vagas disponíveis
                var publicacao = await _context.Publicacoes.FindAsync(idPublicacao);
                if (publicacao == null)
                {
                    return BadRequest("Publicação não encontrada.");
                }

                if (publicacao.VagasDisponiveis > 0)
                {
                    // Candidatar o usuário ao projeto
                    var candidatura = new Candidatura
                    {
                        IdUsuario = idUsuario,
                        IdPublicacao = idPublicacao,
                        DataCandidatura = DateTime.Now
                        // O IdCandidatura será gerado automaticamente pelo banco de dados
                    };

                    // Remover uma vaga disponível
                    publicacao.VagasDisponiveis--;

                    _context.Candidaturas.Add(candidatura);
                    await _context.SaveChangesAsync();

                    return Ok(new { Mensagem = "Usuário candidatou-se ao projeto com sucesso." });
                }
                else
                {
                    return BadRequest("Não há vagas disponíveis para esta publicação.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = "Ocorreu um erro interno no servidor.", ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        // [HttpGet("obterProjetosAssociados")]
        // public ActionResult<IEnumerable<object>> ObterProjetosAssociados(int? idUsuario, int? idOng)
        // {
        //     try
        //     {
        //         // Verificar se foi fornecido pelo menos um dos IDs
        //         if (idUsuario == null && idOng == null)
        //         {
        //             return BadRequest("É necessário fornecer pelo menos um dos IDs (idUsuario ou idOng).");
        //         }

        //         // Filtrar as candidaturas com base no ID do usuário ou ONG fornecido
        //         var candidaturas = _context.Candidaturas
        //             .Where(c => idUsuario == null || c.IdUsuario == idUsuario)
        //             .Where(c => idOng == null || _context.Publicacoes.Any(p => p.OngAssociada == idOng && p.IdPublicacao == c.IdPublicacao))
        //             .Include(c => c.Publicacao)
        //             .Include(c => c.Usuario)
        //             .ToList();

        //         // Montar a resposta final
        //         var projetosAssociados = candidaturas.Select(c => new
        //         {
        //             Projeto = new
        //             {
        //                 c.Publicacao.IdPublicacao,
        //                 c.Publicacao.Titulo,
        //                 c.Publicacao.Descricao,
        //                 c.Publicacao.DataFim,
        //                 c.Publicacao.VagasDisponiveis,
        //                 c.Publicacao.Local,
        //                 OngAssociada = c.Publicacao.OngAssociada,
        //             },
        //             UsuarioAssociado = new
        //             {
        //                 c.Usuario.IdUsuario,
        //                 c.Usuario.Nome,
        //                 c.Usuario.Email,
        //                 c.Usuario.DescricaoHabilidades,
        //             }
        //         });

        //         return Ok(projetosAssociados);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, new { ErrorMessage = "Ocorreu um erro interno no servidor.", ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
        //     }
        // }




    }
}
