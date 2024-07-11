using CCMN_API.Models;
using CCMZ_API.Services.Alocacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMZ_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlocacaoController : ControllerBase
{
    private readonly IAlocacaoService _service;

    public AlocacaoController(IAlocacaoService alocacaoService)
    {
        _service = alocacaoService;
    }

    [HttpGet("eventos/{filtro:int}")]
    public async Task<ActionResult> GetEventos(int filtro)
    {
        try
        {
            var eventos = await _service.GetEventos(filtro);
            return Ok(eventos);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("blocos/{codigoEvento}")]
    public async Task<ActionResult> GetBlocos(int codigoEvento)
    {
        try
        {
            var blocos = await _service.GetBlocos(codigoEvento);
            return Ok(blocos);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("comunidades/{codigoEvento}")]
    public async Task<ActionResult> GetComunidades(int codigoEvento)
    {
        try
        {
            var comunidades = await _service.GetComunidades(codigoEvento);
            return Ok(comunidades);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("pessoas/comunidade/{codigoEvento}/{codigoComunidade}")]
    public async Task<ActionResult> GetPessoasComunidade(int codigoEvento, int codigoComunidade)
    {
        try
        {
            var pessoas = await _service.GetPessoasComunidade(codigoEvento, codigoComunidade);
            if (pessoas != null)
                return Ok(pessoas);

            return NotFound("Nenhuma pessoa encontrada !");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("pessoas/total/{codigoEvento}/{codigoComunidade}")]
    public async Task<ActionResult> GetPessoasTotal(int codigoEvento, int codigoComunidade)
    {
        try
        {
            var pessoas = await _service.GetPessoasTotal(codigoEvento, codigoComunidade);
            return Ok(pessoas);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("pessoas/quarto/{codigoQuarto}")]
    public async Task<ActionResult> GetPessoasQuarto(int codigoQuarto)
    {
        try
        {
            var pessoas = await _service.GetPessoasQuarto(codigoQuarto);
            return Ok(pessoas);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost("pessoa/quarto")]
    public async Task<ActionResult> AddPessoaQuarto(TbQuartoPessoa quartoPessoa)
    {
        try
        {
            await _service.AddPessoaQuarto(quartoPessoa);
            return Ok("Pessoa adicionada ao quarto com Sucesso !");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut("pessoa/quarto")]
    public async Task<ActionResult> AtualizarPessoaQuarto(TbQuartoPessoa quartoPessoa)
    {
        try
        {
            await _service.AtualizarPessoaQuarto(quartoPessoa);
            return Ok("Pessoa atualizada com Sucesso !");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("pessoa/quarto/{codigoPessoa:int}")]
    public async Task<ActionResult> RemoverPessoaQuarto(int codigoPessoa)
    {
        try
        {
            var quarto = await _service.GetPessoaAlocada(codigoPessoa);
            if (quarto != null)
            {
                await _service.RemoverPessoaQuarto(quarto);
                return Ok("Pessoa removida do quarto com Sucesso !");
            }
           
            return NotFound("Pessoa não encontrada !");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("pessoas/quarto/{codigoQuarto:int}")]
    public async Task<ActionResult> LimpaPessoasAlocadas(int codigoQuarto)
    {
        try
        {
            await _service.LimpaPessoasAlocadas(codigoQuarto);
            return Ok("Pessoas removidas do quarto com Sucesso !");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
