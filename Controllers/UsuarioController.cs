using Microsoft.AspNetCore.Mvc;
using AcaoSolidariaApi.Models;
using AcaoSolidariaApi.Services;
using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Utils;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("criarUsuario")]
        public ActionResult CriarUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados de entrada inválidos.");

            if (_context.Usuarios.Any(u => u.Email == usuario.Email))
                return BadRequest("O e-mail fornecido já está em uso.");

            Criptografia.CriarPasswordHash(usuario, usuario.SenhaUsuario);
            usuario.SenhaUsuario = string.Empty;
            usuario.DataRegistro = DateTime.Now;
            _usuarioService.CriarUsuario(usuario);
            return Ok("Usuário criado com sucesso.");
        }

        [HttpPut("atualizarUsuario/{id}")]
        public ActionResult AtualizarUsuario(int id, Usuario usuario)
        {
            if (id <= 0 || id != usuario.IdUsuario)
                return BadRequest("ID inválido. O ID fornecido não corresponde ao ID do usuário.");

            if (!ModelState.IsValid)
                return BadRequest("Dados de entrada inválidos.");

            try
            {
                var usuarioExistente = ObterUsuarioExistente(id);
                if (usuarioExistente == null)
                    return NotFound($"Usuário com o ID {id} não encontrado.");

                AtualizarCamposUsuarioExistente(usuarioExistente, usuario);

                _usuarioService.AtualizarUsuario(usuarioExistente);
                return Ok("Usuário atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno no servidor: {ex.Message}. Tente novamente mais tarde.");
            }
        }

        private Usuario ObterUsuarioExistente(int id)
        {
            return _usuarioService.ObterUsuarioPorId(id);
        }

        private void AtualizarCamposUsuarioExistente(Usuario usuarioExistente, Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Nome))
                usuarioExistente.Nome = usuario.Nome;

            if (!string.IsNullOrEmpty(usuario.Email))
            {
                AtualizarEmailUsuarioExistente(usuarioExistente, usuario.Email);
            }

            if (!string.IsNullOrEmpty(usuario.DescricaoHabilidades))
                usuarioExistente.DescricaoHabilidades = usuario.DescricaoHabilidades;

            if (!string.IsNullOrEmpty(usuario.SenhaUsuario))
            {
                Criptografia.CriarPasswordHash(usuarioExistente, usuario.SenhaUsuario);
                usuarioExistente.SenhaUsuario = string.Empty;
            }
        }

        private void AtualizarEmailUsuarioExistente(Usuario usuarioExistente, string novoEmail)
        {
            var emailExistente = _context.Usuarios.FirstOrDefault(u => u.Email == novoEmail && u.IdUsuario != usuarioExistente.IdUsuario);
            if (emailExistente == null)
                usuarioExistente.Email = novoEmail;
        }


        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario(Usuario credenciais)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                   .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(credenciais.Email.ToLower()));

                if (usuario == null)
                {
                    throw new System.Exception("Usuário não encontrado.");
                }
                else if (!Criptografia.VerificarPasswordHash(credenciais.SenhaUsuario, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    throw new System.Exception("Senha incorreta.");
                }
                else
                {
                    usuario.DataRegistro = System.DateTime.Now;
                    _context.Usuarios.Update(usuario);
                    await _context.SaveChangesAsync(); //Confirma a alteração no banco


                    return Ok(usuario);

                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obterUsuario/{id}")]
        public ActionResult<Usuario> ObterUsuarioPorId(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("ID inválido. O ID deve ser um número inteiro positivo.");

                var usuario = _usuarioService.ObterUsuarioPorId(id);

                if (usuario == null)
                    return NotFound($"Usuário com o ID {id} não encontrado.");

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return StatusCode(500, $"Ocorreu um erro interno no servidor: {ex.Message}. Tente novamente mais tarde.");
            }
        }

        [HttpDelete("deletarUsuario/{id}")]
        public ActionResult DeletarUsuario(int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido.");

            try
            {
                _usuarioService.DeletarUsuario(id);
                _context.SaveChanges();
                return Ok("Usuário deletado com sucesso.");
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
