using CCMZ_API.Services.Dashboard;
using CCMZ_API.Services.Eventos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMZ_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _service;
    private readonly IEventosService _eventoService;

    public DashboardController(IDashboardService dashboardService)
    {
        _service = dashboardService;
    }

    [HttpGet("pessoasAChegar/{codigoEvento:int}")]
    public async Task<ActionResult> GetPessoasAChegar(int codigoEvento)
    {
        try
        {
            var pessoasAChegar = await _service.GetPessoasAChegar(codigoEvento);
            return Ok(pessoasAChegar);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("pessoasChegas/{codigoEvento:int}")]

    [HttpGet("numeroPessoasAChegar/{codigoEvento:int}")]
    public async Task<IActionResult> GetNumeroPessoasAChegar(int codigoEvento)
    {
        try
        {
            var numeroPessoasAChegar = await _service.GetNumeroPessoasAChegar(codigoEvento);
            return Ok(numeroPessoasAChegar);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("numeroPessoasChegas/{codigoEvento:int}")]
    public async Task<IActionResult> GetNumeroPessoasChegas(int codigoEvento)
    {
        try
        {
            var numeroPessoasChegas = await _service.GetNumeroPessoasChegas(codigoEvento);
            return Ok(numeroPessoasChegas);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("numeroPessoasNaoVem/{codigoEvento:int}")]
    public async Task<IActionResult> GetNumeroPessoasNaoVem(int codigoEvento)
    {
        try
        {
            var numeroPessoasNaoVem = await _service.GetNumeroPessoasNaoVem(codigoEvento);
            return Ok(numeroPessoasNaoVem);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("numeroPessoasCobrantes/{codigoEvento:int}")]
    public async Task<IActionResult> GetNumeroPessoasCobrantes(int codigoEvento)
    {
        try
        {
            var numeroPessoasCobrantes = await _service.GetNumeroPessoasCobrantes(codigoEvento);
            return Ok(numeroPessoasCobrantes);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("numeroPessoasPagantes/{codigoEvento:int}")]
    public async Task<IActionResult> GetNumeroPessoasPagantes(int codigoEvento)
    {
        try
        {
            var numeroPessoasPagantes = await _service.GetNumeroPessoasPagantes(codigoEvento);
            return Ok(numeroPessoasPagantes);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("numeroCamasLivres")]
    public async Task<IActionResult> GetNumeroCamasLivres()
    {
        try
        {
            var numeroCamasLivres = await _service.GetNumeroCamasLivres();
            return Ok(numeroCamasLivres);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("numeroCamasOcupadas/{codigoEvento:int}")]
    public async Task<IActionResult> GetNumeroCamasOcupadas(int codigoEvento)
    {
        try
        {
            var numeroCamasOcupadas = await _service.GetNumeroCamasOcupadas(codigoEvento);
            return Ok(numeroCamasOcupadas);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("quartoPessoaAChegar/{codigoQuarto:int}/{codigoEvento:int}")]
    public async Task<ActionResult> GetQuartoPessoaAChegar(int codigoQuarto, int codigoEvento)
    {
        try
        {
            var quartoPessoaAChegar = await _service.GetQuartoPessoaAChegar(codigoQuarto, codigoEvento);
            return Ok(quartoPessoaAChegar);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("quartoPessoaChegas/{codigoQuarto:int}/{codigoEvento:int}")]
    public async Task<ActionResult> GetQuartoPessoaChegas(int codigoQuarto, int codigoEvento)
    {
        try
        {
            var quartoPessoaChegas = await _service.GetQuartoPessoaChegas(codigoQuarto, codigoEvento);
            return Ok(quartoPessoaChegas);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("quartoVagas/{codigoQuarto:int}")]
    public async Task<ActionResult> GetQuartoVagas(int codigoQuarto)
    {
        try
        {
            var quartoVagas = await _service.GetQuartoVagas(codigoQuarto);
            return Ok(quartoVagas);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("quartoOcupados/{codigoQuarto:int}")]
    public async Task<ActionResult> GetQuartoOcupados(int codigoQuarto)
    {
        try
        {
            var quartoOcupados = await _service.GetQuartoOcupados(codigoQuarto);
            return Ok(quartoOcupados);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("eventos")]
    public async Task<ActionResult> GetEventos()
    {
        try
        {
            var idEventoAtivo = await _service.GetIdEventoAtivo();
            return Ok(idEventoAtivo);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
