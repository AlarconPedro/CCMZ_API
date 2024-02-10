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

    [HttpGet("pessoascAChegar")]
    public async Task<IActionResult> GetPessoasAChegar(int codigoEvento)
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

    [HttpGet("quartoPessoaAChegar")]
    public async Task<IActionResult> GetQuartoPessoaAChegar(int codigoQuarto)
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
}
