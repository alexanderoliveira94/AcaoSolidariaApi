using Microsoft.AspNetCore.Mvc;
using AcaoSolidariaApi.Models;
using AcaoSolidariaApi.Services;
using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Utils;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AcaoSolidariaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService, DataContext context)
        {
            _usuarioService = usuarioService;
            _context = context;
        }

        [HttpPost("Registrar")]
        public async Task<ActionResult> RegistrarUsuario(Usuario usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Dados de entrada inválidos.");

                if (_context.Usuarios.Any(u => u.Email == usuario.Email))
                    return BadRequest("O e-mail fornecido já está em uso.");

                Criptografia.CriarPasswordHash(usuario, usuario.SenhaUsuario);
                usuario.SenhaUsuario = string.Empty;
                usuario.DataRegistro = DateTime.Now;
                await _usuarioService.RegistrarUsuario(usuario);
                return NoContent(); // Retorna uma resposta vazia com status 200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(500); // Retorna um status 500 Internal Server Error sem mensagem
            }
        }




        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario(Usuario credenciais)
        {
            try
            {
                Usuario? usuario = await _context.Usuarios
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

                    return NoContent();
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("atualizarUsuario/{id}")]
        public ActionResult AtualizarUsuario(int id, Usuario usuario)
        {
            try
            {
                if (id <= 0 || id != usuario.IdUsuario)
                    return BadRequest("ID inválido. O ID fornecido não corresponde ao ID do usuário.");

                if (!ModelState.IsValid)
                    return BadRequest("Dados de entrada inválidos.");

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
            try
            {
                if (id <= 0)
                    return BadRequest("ID inválido.");

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

        private Usuario ObterUsuarioExistente(int id)
        {
            return _usuarioService.ObterUsuarioPorId(id);
        }

        private void AtualizarCamposUsuarioExistente(Usuario usuarioExistente, Usuario usuario)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        private void AtualizarEmailUsuarioExistente(Usuario usuarioExistente, string novoEmail)
        {
            try
            {
                var emailExistente = _context.Usuarios.FirstOrDefault(u => u.Email == novoEmail && u.IdUsuario != usuarioExistente.IdUsuario);
                if (emailExistente == null)
                    usuarioExistente.Email = novoEmail;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
