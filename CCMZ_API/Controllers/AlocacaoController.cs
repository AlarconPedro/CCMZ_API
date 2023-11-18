using CCMZ_API.Services.Alocacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMZ_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlocacaoController : ControllerBase
{
    private readonly IAlocacaoService _alocacaoService;

    public AlocacaoController(IAlocacaoService alocacaoService)
    {
        _alocacaoService = alocacaoService;
    }

    [HttpGet("eventos")]
    public async Task<IActionResult> GetEventos()
    {
        try
        {
            var eventos = await _alocacaoService.GetEventos();
            return Ok(eventos);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("blocos/{codigoEvento}")]
    public async Task<IActionResult> GetBlocos(int codigoEvento)
    {
        try
        {
            var blocos = await _alocacaoService.GetBlocos(codigoEvento);
            return Ok(blocos);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("comunidades/{codigoEvento}")]
    public async Task<IActionResult> GetComunidades(int codigoEvento)
    {
        try
        {
            var comunidades = await _alocacaoService.GetComunidades(codigoEvento);
            return Ok(comunidades);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("pessoas/comunidade/{codigoEvento}/{codigoComunidade}")]
    public async Task<IActionResult> GetPessoasComunidade(int codigoEvento, int codigoComunidade)
    {
        try
        {
            var pessoas = await _alocacaoService.GetPessoasComunidade(codigoEvento, codigoComunidade);
            return Ok(pessoas);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("quartos/{codigoEvento}")]
    public async Task<IActionResult> GetQuartos(int codigoEvento)
    {
        try
        {
            var quartos = await _alocacaoService.GetQuartos(codigoEvento);
            return Ok(quartos);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("pessoas/quarto/{codigoQuarto}")]
    public async Task<IActionResult> GetPessoasQuarto(int codigoQuarto)
    {
        try
        {
            var pessoas = await _alocacaoService.GetPessoasQuarto(codigoQuarto);
            return Ok(pessoas);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
