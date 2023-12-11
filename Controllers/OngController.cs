using Microsoft.AspNetCore.Mvc;
using AcaoSolidariaApi.Models;
using AcaoSolidariaApi.Services;
using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AcaoSolidariaApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OngController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IOngService _ongService;
        private readonly IConfiguration _configuration;

        public OngController(IOngService ongService, DataContext context, IConfiguration configuration)
        {
            _ongService = ongService;
            _context = context;
            _configuration = configuration;
        }

        private async Task<bool> OngExistente(string email)
        {
            return await _context.ONGs.AnyAsync(x => x.EmailOng.ToLower() == email.ToLower());
        }

        [HttpPost("registrarOng")]
        public async Task<ActionResult> RegistrarOng(ONG ong)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Dados de entrada inválidos.");

                if (await OngExistente(ong.EmailOng))
                    throw new System.Exception("Email já cadastrado");

                Criptografia.CriarPasswordHash(ong.SenhaOng, out byte[] hash, out byte[] salt);
                ong.DataRegistro = System.DateTime.Now;
                ong.SenhaOng = string.Empty;
                ong.PasswordHash = hash;
                ong.PasswordSalt = salt;

                await _ongService.RegistrarOng(ong);

                return Ok(ong.IdOng);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("autenticarOng")]
        public async Task<IActionResult> AutenticarOng(ONG credenciais)
        {
            try
            {
                // Verifica se a ONG existe com as credenciais fornecidas
                ONG? ong = await _context.ONGs
                    .FirstOrDefaultAsync(x => x.EmailOng.ToLower().Equals(credenciais.EmailOng.ToLower()));

                if (ong == null)
                {
                    throw new System.Exception("ONG não encontrada.");
                }
                else if (!Criptografia.VerificarPasswordHash(credenciais.SenhaOng, ong.PasswordHash, ong.PasswordSalt))
                {
                    throw new System.Exception("Senha incorreta.");
                }
                else
                {
                    ong.DataRegistro = System.DateTime.Now;
                    _context.ONGs.Update(ong);
                    await _context.SaveChangesAsync(); // Confirma a alteração no banco
                    ong.PasswordHash = null;
                    ong.PasswordSalt = null;
                    return Ok(ong);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("atualizarOng/{id}")]
        public ActionResult AtualizarOng(int id, [FromBody] ONG ongAtualizacao)
        {
            try
            {
                if (id <= 0 || id != ongAtualizacao.IdOng)
                    return BadRequest("ID inválido. O ID fornecido não corresponde ao ID da ONG.");

                var ongExistente = ObterOngExistente(id);
                if (ongExistente == null)
                    return NotFound($"ONG com o ID {id} não encontrada.");

                AtualizarCamposOngExistente(ongExistente, ongAtualizacao);

                _ongService.AtualizarOng(ongExistente);
                return Ok("ONG atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = "Ocorreu um erro interno no servidor.", ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpGet("obterOng/{id}")]
        public ActionResult<ONG> ObterOngPorId(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("ID inválido. O ID deve ser um número inteiro positivo.");

                var ong = _ongService.ObterOngPorId(id);

                if (ong == null)
                    return NotFound($"ONG com o ID {id} não encontrada.");

                return Ok(ong);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return StatusCode(500, $"Ocorreu um erro interno no servidor: {ex.Message}. Tente novamente mais tarde.");
            }
        }

        [HttpDelete("deletarOng/{id}")]
        public ActionResult DeletarOng(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("ID inválido.");

                _ongService.DeletarOng(id);
                _context.SaveChanges();
                return Ok("ONG deletada com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return StatusCode(500, "Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
            }
        }

        [HttpGet("listarOngs")]
        public ActionResult<IEnumerable<ONG>> ListarOngs()
        {
            try
            {
                var ongs = _ongService.ListarOngs(); // Adicione um método ao seu serviço para obter todas as ONGs
                return Ok(ongs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return StatusCode(500, $"Ocorreu um erro interno no servidor: {ex.Message}. Tente novamente mais tarde.");
            }
        }


        private ONG ObterOngExistente(int id)
        {
            return _ongService.ObterOngPorId(id);
        }

        private void AtualizarCamposOngExistente(ONG ongExistente, ONG ongAtualizacao)
        {
            try
            {
                if (!string.IsNullOrEmpty(ongAtualizacao.NomeOng))
                    ongExistente.NomeOng = ongAtualizacao.NomeOng;

                if (!string.IsNullOrEmpty(ongAtualizacao.EnderecoOng))
                    ongExistente.EnderecoOng = ongAtualizacao.EnderecoOng;

                if (!string.IsNullOrEmpty(ongAtualizacao.CNPJOng))
                    ongExistente.CNPJOng = ongAtualizacao.CNPJOng;

                if (!string.IsNullOrEmpty(ongAtualizacao.EmailOng))
                    AtualizarEmailOngExistente(ongExistente, ongAtualizacao.EmailOng);

                if (!string.IsNullOrEmpty(ongAtualizacao.DescricaoOng))
                    ongExistente.DescricaoOng = ongAtualizacao.DescricaoOng;

                if (!string.IsNullOrEmpty(ongAtualizacao.SenhaOng))
                {
                    Criptografia.CriarPasswordHash(ongAtualizacao.SenhaOng, out byte[] hash, out byte[] salt);
                    ongExistente.SenhaOng = string.Empty;
                    ongExistente.PasswordHash = hash;
                    ongExistente.PasswordSalt = salt;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        private void AtualizarEmailOngExistente(ONG ongExistente, string novoEmail)
        {
            var emailExistente = _context.ONGs.FirstOrDefault(o => o.EmailOng.ToLower() == novoEmail.ToLower() && o.IdOng != ongExistente.IdOng);
            if (emailExistente == null)
                ongExistente.EmailOng = novoEmail;
        }
    }
}
