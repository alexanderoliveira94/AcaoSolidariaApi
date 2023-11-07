using Microsoft.AspNetCore.Mvc;
using AcaoSolidariaApi.Models;
using AcaoSolidariaApi.Services;
using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Utils;
using System;
using System.Linq;

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

        [HttpPost("criarOng")]
        public ActionResult CriarOng(ONG ong)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados de entrada inválidos.");

            if (_context.ONGs.Any(o => o.EmailOng == ong.EmailOng))
                return BadRequest("O e-mail fornecido já está em uso.");

            Criptografia.CriarPasswordHash(ong.SenhaOng,out byte[] hash, out byte[] salt);
            ong.SenhaOng = string.Empty;
            _ongservice.CriarOng(ong);
            return Ok("ONG criada com sucesso.");
        }

        [HttpPut("atualizarOng/{id}")]
        public ActionResult AtualizarOng(int id, ONG ong)
        {
            if (id <= 0 || id != ong.IdOng)
                return BadRequest("ID inválido. O ID fornecido não corresponde ao ID da ONG.");

            if (!ModelState.IsValid)
                return BadRequest("Dados de entrada inválidos.");

            try
            {
                var ongExistente = ObterOngExistente(id);
                if (ongExistente == null)
                    return NotFound($"ONG com o ID {id} não encontrada.");

                AtualizarCamposOngExistente(ongExistente, ong);

                _ongservice.AtualizarOng(ongExistente);
                return Ok("ONG atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno no servidor: {ex.Message}. Tente novamente mais tarde.");
            }
        }

        private ONG ObterOngExistente(int id)
        {
            return _ongservice.ObterOngPorId(id);
        }

        private void AtualizarCamposOngExistente(ONG ongExistente, ONG ong)
        {
            if (!string.IsNullOrEmpty(ong.NomeOng))
                ongExistente.NomeOng = ong.NomeOng;

            if (!string.IsNullOrEmpty(ong.EnderecoOng))
                ongExistente.EnderecoOng = ong.EnderecoOng;

            if (!string.IsNullOrEmpty(ong.CNPJOng))
                ongExistente.CNPJOng = ong.CNPJOng;

            if (!string.IsNullOrEmpty(ong.EmailOng))
                AtualizarEmailOngExistente(ongExistente, ong.EmailOng);

            if (!string.IsNullOrEmpty(ong.DescricaoOng))
                ongExistente.DescricaoOng = ong.DescricaoOng;

            if (!string.IsNullOrEmpty(ong.SenhaOng))
            {
                Criptografia.CriarPasswordHash(ong.SenhaOng,out byte[] hash, out byte[] salt);
                ongExistente.SenhaOng = string.Empty;
            }
        }

        private void AtualizarEmailOngExistente(ONG ongExistente, string novoEmail)
        {
            var emailExistente = _context.ONGs.FirstOrDefault(o => o.EmailOng == novoEmail && o.IdOng != ongExistente.IdOng);
            if (emailExistente == null)
                ongExistente.EmailOng = novoEmail;
        }

        [HttpGet("obterOng/{id}")]
        public ActionResult<ONG> ObterOngPorId(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("ID inválido. O ID deve ser um número inteiro positivo.");

                var ong = _ongservice.ObterOngPorId(id);

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
            if (id <= 0)
                return BadRequest("ID inválido.");

            try
            {
                _ongservice.DeletarOng(id);
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
    }
}
