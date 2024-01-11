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

    [HttpGet("{codigoBloco:int}")]
    public async Task<ActionResult<IEnumerable<QuartoPessoas>>> GetQuartoPessoas(int codigoBloco)
    {
        try
        {
            return Ok(await _service.GetQuartoPessoas(codigoBloco));
        }
        catch
        {
            return BadRequest("Request Inválido !");
        }
    }
}