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

        private readonly IOngService _ongservice;

        public OngController(IOngService ongservice, DataContext context)
        {
            _ongservice = ongservice;
            _context = context;
        }

        // Criar ONG
        [HttpPost("criarOng")]
        public ActionResult CriarOng(ONG ong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados de entrada inválidos.");
            }

            
            _ongservice.CriarOng(ong);
            return Ok("Ong criada com sucesso.");
        }

         // Atualizar Ong
        [HttpPut("atualizarOng/{id}")]
        public ActionResult AtualizarOng(int id, ONG ong)
        {
            if (id <= 0 || id != ong.Id)
            {
                return BadRequest("ID inválido. O ID fornecido não corresponde ao ID da ONG.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Dados de entrada inválidos.");
            }

            try
            {
                // Realize as operações de atualização conforme necessário
                var ongExistente = _ongservice.ObterOngPorId(id);

                if (ongExistente == null)
                {
                    return NotFound($"Ong com o ID {id} não encontrado.");
                }

                ongExistente.Nome = ong.Nome; 
                ongExistente.Endereco = ong.Endereco;
                ongExistente.CNPJ = ong.CNPJ;
                ongExistente.Descricao = ong.Descricao;
                ongExistente.Senha = ong.Senha;
                
                
                _ongservice.AtualizarOng(ongExistente);
                return Ok("Ong atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
            }
        }

       [HttpGet("obterOng/{id}")]
        public ActionResult<ONG> ObterOngPorId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID inválido. O ID deve ser um número inteiro positivo.");
                }

                var ong = _ongservice.ObterOngPorId(id);

                if (ong == null)
                {
                    return NotFound($"Ong com o ID {id} não encontrado.");
                }

                return Ok(ong);
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

        [HttpDelete("deletarOng/{id}")]
        public ActionResult DeletarOng(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            try
            {
                _ongservice.DeletarOng(id);
                _context.SaveChanges(); 
                return Ok("Ong deletado com sucesso.");
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