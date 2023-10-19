using Microsoft.AspNetCore.Mvc;
using AcaoSolidariaApi.Models;
using AcaoSolidariaApi.Services;
using AcaoSolidariaApi.Data;

namespace AcaoSolidariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OngController : ControllerBase
    {

        private readonly DataContext _context;

        private readonly IUsuarioService _usuarioService;

        public OngController(IUsuarioService usuarioService, DataContext context)
        {
            _usuarioService = usuarioService;
            _context = context;
        }

        // Criar ONG
        [HttpPost("criarOng")]
        public ActionResult CriarOng([FromBody] ONG ong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados de entrada inválidos.");
            }

            // Adicione outras validações conforme necessário

            _usuarioService.CriarOng(ong);
            return Ok("ONG criada com sucesso.");
        }

        [HttpPut("atualizarOng/{id}")]
        public ActionResult AtualizarOng(int id, [FromBody] ONG ong)
        {
            if (id <= 0 || id != ong.Id)
            {
                return BadRequest("ID inválido.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Dados de entrada inválidos.");
            }

            // Adicione outras validações conforme necessário

            _usuarioService.AtualizarOng(ong);
            return Ok("ONG atualizada com sucesso.");
        }

        // Obter ONG por ID
        [HttpGet("obterOng/{id}")]
        public ActionResult<ONG> ObterOngPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            var ong = _usuarioService.ObterOngPorId(id);

            if (ong == null)
            {
                return NotFound("ONG não encontrada.");
            }

            return Ok(ong);
        }

        [HttpDelete("deletarOng/{id}")]
        public ActionResult DeletarOng(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            _usuarioService.DeletarOng(id);
            return Ok("ONG deletada com sucesso.");
        }

    }
}