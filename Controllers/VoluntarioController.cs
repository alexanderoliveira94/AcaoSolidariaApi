using Microsoft.AspNetCore.Mvc;
using AcaoSolidariaApi.Models;
using AcaoSolidariaApi.Services;
using AcaoSolidariaApi.Data;

namespace AcaoSolidariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoluntarioController : ControllerBase
    {

        private readonly DataContext _context;

        private readonly IVoluntarioService _voluntarioService;

        public VoluntarioController (IVoluntarioService voluntarioService, DataContext context)
        {
            _voluntarioService = voluntarioService;
            _context = context;
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

            _voluntarioService.CriarVoluntario(voluntario);
            return Ok("Voluntário criado com sucesso.");
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

            _voluntarioService.AtualizarVoluntario(voluntario);
            return Ok("Voluntário atualizado com sucesso.");
        }

        [HttpGet("obterVoluntario/{id}")]
        public ActionResult<Voluntario> ObterVoluntarioPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            var voluntario = _voluntarioService.ObterVoluntarioPorId(id);

            if (voluntario == null)
            {
                return NotFound("Voluntário não encontrado.");
            }

            return Ok(voluntario);
        }

        [HttpDelete("deletarVoluntario/{id}")]
        public ActionResult DeletarVoluntario(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            _voluntarioService.DeletarVoluntario(id);
            return Ok("Voluntário deletado com sucesso.");
        }
    }
}