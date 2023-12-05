using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Models;
using Microsoft.AspNetCore.Mvc;
using AcaoSolidariaApi.Services;

namespace AcaoSolidariaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                if (!await _publicacaoService.VerificarOngAutenticada(publicacao.OngAssociada))
                {
                    return Unauthorized("ONG não autorizada.");
                }

                publicacao.DataPublicacao = DateTime.Now;

                if (await _publicacaoService.CriarPublicacao(publicacao))
                {
                    return Ok(new { Mensagem = "Publicação criada com sucesso." });
                }
                else
                {
                    return StatusCode(500, "Falha ao criar a publicação.");
                }
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

    }
}
