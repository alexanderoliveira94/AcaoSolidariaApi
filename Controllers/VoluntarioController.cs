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

        public VoluntarioController(IVoluntarioService voluntarioService, DataContext context)
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
        public ActionResult AtualizarVoluntario(int id, Voluntario voluntario)
        {
            if (id <= 0 || id != voluntario.Id)
            {
                return BadRequest("ID inválido. O ID fornecido não corresponde ao ID do voluntário.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Dados de entrada inválidos.");
            }

            try
            {
                // Realize as operações de atualização conforme necessário
                var voluntarioExistente = _voluntarioService.ObterVoluntarioPorId(id);

                if (voluntarioExistente == null)
                {
                    return NotFound($"Voluntário com o ID {id} não encontrado.");
                }

                voluntarioExistente.Nome = voluntario.Nome; // Atualize os outros campos conforme necessário
                voluntarioExistente.Email = voluntario.Email;
                voluntarioExistente.Senha = voluntario.Senha;
                
                _voluntarioService.AtualizarVoluntario(voluntarioExistente);
                return Ok("Voluntário atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
            }
        }


        [HttpGet("obterVoluntario/{id}")]
        public ActionResult<Voluntario> ObterVoluntarioPorId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID inválido. O ID deve ser um número inteiro positivo.");
                }

                var voluntario = _voluntarioService.ObterVoluntarioPorId(id);

                if (voluntario == null)
                {
                    return NotFound($"Voluntário com o ID {id} não encontrado.");
                }

                return Ok(voluntario);
            }
            catch (Exception ex)
            {
                // Registre a exceção para depuração
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                // Retorne uma resposta de erro mais descritiva
                return StatusCode(500, $"Ocorreu um erro interno no servidor: {ex.Message}. Tente novamente mais tarde.");
            }

        }


        [HttpDelete("deletarVoluntario/{id}")]
        public ActionResult DeletarVoluntario(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            try
            {
                _voluntarioService.DeletarVoluntario(id);
                _context.SaveChanges(); // Salvar mudanças no banco de dados
                return Ok("Voluntário deletado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return StatusCode(500, "Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
            }
        }

    }
}