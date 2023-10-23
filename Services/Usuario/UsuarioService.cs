using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Models;
using AcaoSolidariaApi.Services;

public class UsuarioService : IUsuarioService
{
    private readonly DataContext _context;

    public UsuarioService(DataContext context)
    {
        _context = context;
    }

    public void CriarUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public void AtualizarUsuario(Usuario usuario)
    {
        var usuarioExistente = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == usuario.IdUsuario);
        if (usuarioExistente != null)
        {
            // Atualize os campos do usuário conforme necessário
            _context.Usuarios.Update(usuarioExistente);
            _context.SaveChanges();
        }
    }

    public Usuario ObterUsuarioPorId(int id)
    {
        return _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

    }

    public void DeletarUsuario(int id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }
}
