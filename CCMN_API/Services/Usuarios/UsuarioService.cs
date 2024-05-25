
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
}
