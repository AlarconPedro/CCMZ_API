using CCMN_API.Models;

namespace CCMN_API.Services.Usuarios;

public interface IUsuarioService
{
    Task<TbUsuario> LoginSistema(string codigoFirebase);
    Task<IEnumerable<TbUsuario>> GetUsuarios();

    Task CadastrarUsuario(TbUsuario usuario);

    Task AtualizarUsuario(TbUsuario usuario);

    Task DeletarUsuario(int codigoUsuario);
}
