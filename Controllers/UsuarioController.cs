using Microsoft.AspNetCore.Mvc;
using AcaoSolidariaApi.Models;
using AcaoSolidariaApi.Services;
using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace AcaoSolidariaApi.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUsuarioService _usuarioService;

        private readonly IConfiguration _configuration;

        public UsuarioController(IUsuarioService usuarioService, DataContext context, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _context = context;
            _configuration = configuration;
        }

        private async Task<bool> UsuarioExistente(string email)
        {
            if (await _context.Usuarios.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }


        [HttpPost("Registrar")]
        public async Task<ActionResult> RegistrarUsuario(Usuario usuario)
        {
            try
            {

                if (await UsuarioExistente(usuario.Email))
                    throw new System.Exception("Email já cadastrado");



                Criptografia.CriarPasswordHash(usuario.SenhaUsuario, out byte[] hash, out byte[] salt);
                usuario.DataRegistro = System.DateTime.Now;
                usuario.SenhaUsuario = string.Empty;
                usuario.PasswordHash = hash;
                usuario.PasswordSalt = salt;
                await _usuarioService.RegistrarUsuario(usuario);

                return Ok(usuario.IdUsuario);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario(Usuario credenciais)
        {
            try
            {
                // Verifica se o usuário existe com as credenciais fornecidas
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
                    await _context.SaveChangesAsync(); // Confirma a alteração no banco
                    usuario.PasswordHash = null;
                    usuario.PasswordSalt = null;
                    return Ok(usuario);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        
        [HttpPut("atualizarUsuario/{id}")]
        public ActionResult AtualizarUsuario(int id, [FromBody] Usuario usuarioAtualizacao)
        {
            try
            {
                // Verifica se o ID é válido
                if (id <= 0 || id != usuarioAtualizacao.IdUsuario)
                    return BadRequest("ID inválido. O ID fornecido não corresponde ao ID do usuário.");

                // Obtém o usuário existente
                var usuarioExistente = ObterUsuarioExistente(id);
                if (usuarioExistente == null)
                    return NotFound($"Usuário com o ID {id} não encontrado.");

                // Atualiza apenas os atributos permitidos
                AtualizarCamposUsuarioExistente(usuarioExistente, usuarioAtualizacao);

                // Chama o serviço para atualizar o usuário
                _usuarioService.AtualizarUsuario(usuarioExistente);

                return Ok("Usuário atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = "Ocorreu um erro interno no servidor.", ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        private void AtualizarCamposUsuarioExistente(Usuario usuarioExistente, Usuario usuarioAtualizacao)
        {
            try
            {
                if (!string.IsNullOrEmpty(usuarioAtualizacao.Nome))
                    usuarioExistente.Nome = usuarioAtualizacao.Nome;

                if (!string.IsNullOrEmpty(usuarioAtualizacao.DescricaoHabilidades))
                    usuarioExistente.DescricaoHabilidades = usuarioAtualizacao.DescricaoHabilidades;

                if (!string.IsNullOrEmpty(usuarioAtualizacao.SenhaUsuario))
                {
                    Criptografia.CriarPasswordHash(usuarioAtualizacao.SenhaUsuario, out byte[] hash, out byte[] salt);
                    usuarioExistente.SenhaUsuario = string.Empty;
                    usuarioExistente.PasswordHash = hash;
                    usuarioExistente.PasswordSalt = salt;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
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


    }
}
