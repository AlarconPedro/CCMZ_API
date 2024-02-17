using CCMZ_API.Services.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMZ_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _service;

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

    [HttpGet("numeroPessoasAChegar")]
    public async Task<IActionResult> GetNumeroPessoasAChegar()
    {
        try
        {
            var numeroPessoasAChegar = await _service.GetNumeroPessoasAChegar();
            return Ok(numeroPessoasAChegar);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("quartoPessoaAChegar/{codigoQuarto:int}")]
    public async Task<ActionResult> GetQuartoPessoaAChegar(int codigoQuarto)
    {
        try
        {
            var quartoPessoaAChegar = await _service.GetQuartoPessoaAChegar(codigoQuarto);
            return Ok(quartoPessoaAChegar);
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
