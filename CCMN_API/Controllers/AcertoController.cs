using CCMN_API.Services.DespesasComunidade;
using CCMN_API.Services.DespesasEvento;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMN_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AcertoController : ControllerBase
{
    private readonly IDespesaEventoService _eventoService;
    private readonly IDespesaComunidadeService _comunidadeService;

    public AcertoController(IDespesaEventoService eventoService, IDespesaComunidadeService comunidadeService)
    {
        _eventoService = eventoService;
        _comunidadeService = comunidadeService;
    }

    [HttpGet("evento/custo/{codigoEvento}")]
    public async Task<IActionResult> GetEventoCusto(int codigoEvento)
    {
        var eventoCusto = await _eventoService.GetEventoCusto(codigoEvento);

        if (eventoCusto == null)
        {
            return NotFound("Nenhum Custo Cadastrado no Evento !");
        }

        return Ok(eventoCusto);
    }

    [HttpGet("evento/despesas/{codigoEvento}")]
    public async Task<IActionResult> GetDespesasEvento(int codigoEvento)
    {
        var despesasEvento = await _eventoService.GetDespesasEvento(codigoEvento);

        if (despesasEvento == null)
        {
            return NotFound("Nenhuma Despesa Cadastrada no Evento !");
        }

        return Ok(despesasEvento);
    }

    [HttpGet("evento/pessoas/{codigoEvento}")]
    public async Task<IActionResult> GetCobrantesPagantes(int codigoEvento)
    {
        var cobrantesPagantes = await _eventoService.GetCobrantesPagantes(codigoEvento);

        if (cobrantesPagantes == null)
        {
            return NotFound("Nenhuma Pessoa Cadastrada no Evento !");
        }

        return Ok(cobrantesPagantes);
    }

    [HttpGet("comunidade/despesas/{codigoEvento}/{codigoComunidade}")]
    public async Task<IActionResult> GetDespesasComunidade(int codigoEvento, int codigoComunidade)
    {
        var despesasComunidade = await _comunidadeService.GetDespesasComunidade(codigoEvento, codigoComunidade);

        if (despesasComunidade == null)
        {
            return NotFound("Nenhuma Despesa Cadastrada na Comunidade !");
        }

        return Ok(despesasComunidade);
    }

    [HttpGet("comunidade/pessoas/{codigoEvento}/{codigoComunidade}")]
    public async Task<IActionResult> GetCobrantesPagantes(int codigoEvento, int codigoComunidade)
    {
        var cobrantesPagantes = await _comunidadeService.GetCobrantesPagantes(codigoEvento, codigoComunidade);

        if (cobrantesPagantes == null)
        {
            return NotFound("Nenhuma Pessoa Cadastrada na Comunidade !");
        }

        return Ok(cobrantesPagantes);
    }
}
