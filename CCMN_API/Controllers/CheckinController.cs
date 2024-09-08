using CCMN_API;
using CCMN_API.Models;
using CCMN_API.Models.Painel.Hospedagem.Checkin;
using CCMN_API.Models.Painel.Hospedagem.QuartoPessoa;
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
    public async Task<ActionResult<IEnumerable<QuartosCheckinEvento>>> GetQuartoCheckin(int codigoEvento)
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