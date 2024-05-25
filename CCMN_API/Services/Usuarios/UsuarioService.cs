
using CCMZ_API;
using Microsoft.EntityFrameworkCore;

namespace CCMN_API.Services.Usuarios;

public class UsuarioService : IUsuarioService
{
    private readonly CCMNContext _context;

    public UsuarioService(CCMNContext context)
    {
        _context = context;
    }

    public async Task<TbUsuario> LoginSistema(string codigoFirebase)
    {
        return await _context.TbUsuarios.FirstOrDefaultAsync(u => u.UsuCodigoFirebase == codigoFirebase);
    }

    public async Task CadastrarUsuario(TbUsuario usuario)
    {
        var ultimoUsuario = await _context.TbUsuarios.FirstOrDefaultAsync();
        if (ultimoUsuario != null)
        {
            usuario.UsuCodigo = await _context.TbUsuarios.MaxAsync(u => u.UsuCodigo) + 1;
        } else
        {
            usuario.UsuCodigo = 1;
        }
        _context.TbUsuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }
}
