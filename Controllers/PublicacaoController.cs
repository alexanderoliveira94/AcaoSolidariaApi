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
                    DataInicio = publicacao.DataInicio,
                    DataFim = publicacao.DataFim,
                    VagasDisponiveis = publicacao.VagasDisponiveis,
                    Local = publicacao.Local,
                    OngAssociada = publicacao.OngAssociada,
                    DataPublicacao = DateTime.Now
                };

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

        [HttpPost("candidatarProjeto/{idPublicacao}")]

        public async Task<IActionResult> CandidatarProjeto(int idPublicacao)
        {
            try
            {
                // Obter o ID do usuário autenticado a partir do token
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                {
                    return Unauthorized("Usuário não autenticado.");
                }

                // Verificar se o usuário já está associado a esta publicação (adicione essa verificação se necessário)
                var candidaturaExistente = await _context.Candidaturas
                    .FirstOrDefaultAsync(c => c.IdPublicacao == idPublicacao && c.IdUsuario == int.Parse(userId));

                if (candidaturaExistente != null)
                {
                    return BadRequest("Usuário já candidatou-se a este projeto.");
                }

                // Associar o usuário à publicação
                var candidatura = new Candidatura
                {
                    IdPublicacao = idPublicacao,
                    IdUsuario = int.Parse(userId),
                    DataCandidatura = DateTime.Now
                };

                _context.Candidaturas.Add(candidatura);
                await _context.SaveChangesAsync();

                return Ok(new { Mensagem = "Usuário candidatou-se ao projeto com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = "Ocorreu um erro interno no servidor.", ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
            }
        }


    }
}
