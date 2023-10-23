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

        // Criar Usuário
        [HttpPost("criarUsuario")]
        public ActionResult CriarUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados de entrada inválidos.");
            }

            var usuarioExistente = _context.Usuarios.FirstOrDefault(u => u.Email == usuario.Email);
            if (usuarioExistente != null)
            {
                return BadRequest("O e-mail fornecido já está em uso.");
            }
            usuario.DataRegistro = DateTime.Now;
            _usuarioService.CriarUsuario(usuario);
            return Ok("Usuário criado com sucesso.");
        }

        // Atualizar Usuário
        [HttpPut("atualizarUsuario/{id}")]
        public ActionResult AtualizarUsuario(int id, Usuario usuario)
        {
            if (id <= 0 || id != usuario.IdUsuario)
            {
                return BadRequest("ID inválido. O ID fornecido não corresponde ao ID do usuário.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Dados de entrada inválidos.");
            }

            try
            {
                // Realize as operações de atualização conforme necessário
                var usuarioExistente = _usuarioService.ObterUsuarioPorId(id);

                if (usuarioExistente == null)
                {
                    return NotFound($"Usuário com o ID {id} não encontrado.");
                }

                usuarioExistente.Nome = usuario.Nome; // Atualize os outros campos conforme necessário
                usuarioExistente.Email = usuario.Email;
                usuarioExistente.Senha = usuario.Senha;

                _usuarioService.AtualizarUsuario(usuarioExistente);
                return Ok("Usuário atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
            }
        }

        [HttpGet("obterUsuario/{id}")]
        public ActionResult<Usuario> ObterUsuarioPorId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID inválido. O ID deve ser um número inteiro positivo.");
                }

                var usuario = _usuarioService.ObterUsuarioPorId(id);

                if (usuario == null)
                {
                    return NotFound($"Usuário com o ID {id} não encontrado.");
                }

                return Ok(usuario);
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

        [HttpDelete("deletarUsuario/{id}")]
        public ActionResult DeletarUsuario(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            try
            {
                _usuarioService.DeletarUsuario(id);
                _context.SaveChanges(); // Salvar mudanças no banco de dados
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
