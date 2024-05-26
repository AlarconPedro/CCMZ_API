using CCMN_API.Services.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMN_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _service;

    public UsuarioController(IUsuarioService service)
    {
        _service = service;
    }

    [HttpGet("{codigoFirebase}")]
    public async Task<ActionResult<TbUsuario>> LoginSistema(string codigoFirebase)
    {
        var retorno = await _service.LoginSistema(codigoFirebase);
        return Ok(retorno);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TbUsuario>>> GetUsuarios()
    {
        var retorno = await _service.GetUsuarios();
        return Ok(retorno);
    }

    [HttpPost]
    public async Task<ActionResult> CadastrarUsuario(TbUsuario usuario)
    {
        await _service.CadastrarUsuario(usuario);
        return Ok("Cadastrado com Sucesso !");
    }

    [HttpPut]
    public async Task<ActionResult> AtualizarUsuario(TbUsuario usuario)
    {
        await _service.AtualizarUsuario(usuario);
        return Ok("Atualizado com Sucesso !");
    }
}
