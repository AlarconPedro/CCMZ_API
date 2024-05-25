namespace CCMN_API.Services.Usuarios;

public interface IUsuarioService
{
    Task<TbUsuario> LoginSistema(string codigoFirebase);
}
