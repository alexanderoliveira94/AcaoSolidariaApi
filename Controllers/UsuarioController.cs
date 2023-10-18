using Microsoft.AspNetCore.Mvc;
using AcaoSolidariaApi.Models;
using AcaoSolidariaApi.Services;
using AcaoSolidariaApi.Data;

namespace AcaoSolidariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly DataContext _context;

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService, DataContext context)
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

        // Criar Voluntário
        [HttpPost("criarVoluntario")]
        public ActionResult CriarVoluntario(Voluntario voluntario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados de entrada inválidos.");
            }

            // Adicione outras validações conforme necessário

            _usuarioService.CriarVoluntario(voluntario);
            return Ok("Voluntário criado com sucesso.");
        }

        // Atualizar ONG
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

        // Atualizar Voluntário
        [HttpPut("atualizarVoluntario/{id}")]
        public ActionResult AtualizarVoluntario(int id, [FromBody] Voluntario voluntario)
        {
            if (id <= 0 || id != voluntario.Id)
            {
                return BadRequest("ID inválido.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Dados de entrada inválidos.");
            }

            // Adicione outras validações conforme necessário

            _usuarioService.AtualizarVoluntario(voluntario);
            return Ok("Voluntário atualizado com sucesso.");
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

        // Obter Voluntário por ID
        [HttpGet("obterVoluntario/{id}")]
        public ActionResult<Voluntario> ObterVoluntarioPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            var voluntario = _usuarioService.ObterVoluntarioPorId(id);

            if (voluntario == null)
            {
                return NotFound("Voluntário não encontrado.");
            }

            return Ok(voluntario);
        }

        // Deletar ONG
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

        // Deletar Voluntário
        [HttpDelete("deletarVoluntario/{id}")]
        public ActionResult DeletarVoluntario(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            _usuarioService.DeletarVoluntario(id);
            return Ok("Voluntário deletado com sucesso.");
        }
    }
}
