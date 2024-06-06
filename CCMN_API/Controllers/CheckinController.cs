using CCMN_API;
using CCMZ_API.Models.Painel.QuartoPessoa;
using CCMZ_API.Services.QuartoPessoa;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMZ_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CheckinController : ControllerBase
{
    private readonly ICheckinService _service;

    public CheckinController(ICheckinService quartoPessoa)
    {
        _service = quartoPessoa;
    }

    [HttpGet("{codigoEvento:int}")]
    public async Task<ActionResult<IEnumerable<QuartoPessoas>>> GetQuartoCheckin(int codigoEvento)
    {
        try
        {
            return Ok(await _service.GetQuartoCheckin(codigoEvento));
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Request Inválido ! {ex}");
        }
    }

    [HttpGet("{codigoEvento:int}/{busca}")]
    public async Task<ActionResult<IEnumerable<QuartoPessoas>>> GetQuartoPessoasBusca(int codigoEvento, string busca)
    {
        try
        {
            return Ok(await _service.GetQuartoPessoasBusca(codigoEvento, busca));
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Request Inválido ! {ex}");
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateQuartoPessoa(TbQuartoPessoa quartoPessoa)
    {
        try
        {
            await _service.UpdateQuartoPessoa(quartoPessoa);
            return Ok("Quarto Atualizado com Sucesso !");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Request Inválido ! {ex}");
        }
    }
}