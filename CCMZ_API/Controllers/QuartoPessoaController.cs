using CCMZ_API.Models.Painel.QuartoPessoa;
using CCMZ_API.Services.QuartoPessoa;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMZ_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuartoPessoaController : ControllerBase
{
    private readonly IQuartoPessoaService _service;

    public QuartoPessoaController(IQuartoPessoaService quartoPessoa)
    {
        _service = quartoPessoa;
    }

    [HttpGet("{codigoBloco:int}/{codigoEvento:int}")]
    public async Task<ActionResult<IEnumerable<QuartoPessoas>>> GetQuartoPessoas(int codigoBloco, int codigoEvento)
    {
        try
        {
            return Ok(await _service.GetQuartoPessoas(codigoBloco, codigoEvento));
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