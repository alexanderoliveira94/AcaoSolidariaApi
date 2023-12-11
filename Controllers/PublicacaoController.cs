using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Models;
using Microsoft.AspNetCore.Mvc;
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
                if (idUsuario <= 0)
                {
                    return BadRequest(new { ErrorMessage = "ID de usuário inválido." });
                }
                // Verificar se o usuário já está associado a esta publicação
                var candidaturaExistente = await _context.Candidaturas
                    .AnyAsync(c => c.IdUsuario == idUsuario && c.IdPublicacao == idPublicacao);

                if (candidaturaExistente)
                {
                    return BadRequest(new { ErrorMessage = "Usuário já candidatou-se a este projeto." });
                }

                // Verificar se há vagas disponíveis
                var publicacao = await _context.Publicacoes.FindAsync(idPublicacao);
                if (publicacao == null)
                {
                    return BadRequest(new { ErrorMessage = "Publicação não encontrada." });
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

                    // Retorna um objeto anônimo para ser serializado em JSON
                    return new OkObjectResult("Candidatura realizada com sucesso");
                }
                else
                {
                    return BadRequest(new { ErrorMessage = "Não há vagas disponíveis para esta publicação." });
                }
            }
            catch (Exception ex)
            {
                // Retorna uma resposta com status 500 e informações de erro em formato JSON
                return StatusCode(500, new { ErrorMessage = "Ocorreu um erro interno no servidor.", ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpGet("listarCandidaturas")]
        public ActionResult<IEnumerable<Candidatura>> ListarCandidaturas()
        {
            try
            {
                var candidaturas = _context.Candidaturas.ToList();
                return Ok(candidaturas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = "Ocorreu um erro interno no servidor.", ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
            }
        }


    }
}







