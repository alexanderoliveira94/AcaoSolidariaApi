using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Models;
using AcaoSolidariaApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class UsuarioService : IUsuarioService
{
    private readonly DataContext _context;

    public UsuarioService(DataContext context)
    {
        _context = context;
    }

    public async Task RegistrarUsuario(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();

    }

    public void AtualizarUsuario(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        _context.SaveChanges();
    }

    public Usuario ObterUsuarioPorId(int id)
    {
        return _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
    }

    public async Task DeletarUsuario(int id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
