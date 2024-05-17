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

    //GET
    /*[HttpGet("evento/comunidades/{codigoEvento}")]
    public async Task<ActionResult> GetComunidadesEvento(int codigoEvento)
    {
        var comunidadesEvento = await _eventoService.GetComunidadesEvento(codigoEvento);

        if (comunidadesEvento == null)
        {
            return NotFound("Nenhuma Comunidade Cadastrada no Evento !");
        }

        return Ok(comunidadesEvento);
    }*/

    //[HttpGet("evento/comunidades/dados/{codigoEvento}")]
    [HttpGet("evento/comunidades/{codigoEvento}")]
    public async Task<ActionResult> GetComunidadesDados(int codigoEvento)
    {
        var comunidadesDados = await _eventoService.GetComunidadesDados(codigoEvento);

        if (comunidadesDados == null)
        {
            return NotFound("Nenhuma Comunidade Cadastrada no Evento !");
        }

        return Ok(comunidadesDados);
    }

    [HttpGet("evento/custo/{codigoEvento}")]
    public async Task<ActionResult> GetEventoCusto(int codigoEvento)
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

    [HttpGet("evento/despesas/cozinha/{codigoEvento}")]
    public async Task<ActionResult> GetValorCozinha(int codigoEvento)
    {
        var valorCozinha = await _eventoService.GetValorCozinha(codigoEvento);

        if (valorCozinha == 0)
        {
            return NotFound("Nenhuma Despesa de Cozinha Cadastrada no Evento !");
        }

        return Ok(valorCozinha);
    }

    [HttpGet("evento/despesas/hostiaria/{codigoEvento}")]
    public async Task<ActionResult> GetValorHostiaria(int codigoEvento)
    {
        var valorHostiaria = await _eventoService.GetValorHostiaria(codigoEvento);

        if (valorHostiaria == 0)
        {
            return NotFound("Nenhuma Despesa de Hostiaria Cadastrada no Evento !");
        }

        return Ok(valorHostiaria);
    }

    //POST
    [HttpPost("evento/despesas")]
    public async Task<ActionResult> AddDespesaEvento(TbDespesaEvento despesaEvento)
    {
        await _eventoService.AddDespesaEvento(despesaEvento);

        return Ok("Despesa Cadastrada com Sucesso !");
    }

    [HttpPost("evento/despesas/cozinha")]
    public async Task<IActionResult> AddDespesaCozinha(int codigoEvento, decimal valor)
    {
        await _eventoService.AddDespesaCozinha(codigoEvento, valor);

        return Ok("Despesa de Cozinha Cadastrada com Sucesso !");
    }

    [HttpPost("evento/despesas/hostiaria")]
    public async Task<IActionResult> AddDespesaHostiaria(int codigoEvento, decimal valor)
    {
        await _eventoService.AddDespesaHostiaria(codigoEvento, valor);

        return Ok("Despesa de Hostiaria Cadastrada com Sucesso !");
    }

    //PUT
    [HttpPut("evento/despesas")]
    public async Task<IActionResult> UpdateDespesaEvento(TbDespesaEvento despesaEvento)
    {
        await _eventoService.UpdateDespesaEvento(despesaEvento);

        return Ok("Despesa Atualizada com Sucesso !");
    }

    [HttpPut("evento/despesas/cozinha")]
    public async Task<IActionResult> UpdateDespesaCozinha(int codigoEvento, decimal valor)
    {
        await _eventoService.UpdateDespesaCozinha(codigoEvento, valor);

        return Ok("Despesa de Cozinha Atualizada com Sucesso !");
    }

    [HttpPut("evento/despesas/hostiaria")]
    public async Task<IActionResult> UpdateDespesaHostiaria(int codigoEvento, decimal valor)
    {
        await _eventoService.UpdateDespesaHostiaria(codigoEvento, valor);

        return Ok("Despesa de Hostiaria Atualizada com Sucesso !");
    }
}
