﻿using CCMZ_API.Services.Dashboard;
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

    [HttpGet("pessoasChegas/{codigoEvento:int}")]

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

    [HttpGet("numeroPessoasChegas")]
    public async Task<IActionResult> GetNumeroPessoasChegas()
    {
        try
        {
            var numeroPessoasChegas = await _service.GetNumeroPessoasChegas();
            return Ok(numeroPessoasChegas);
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

    [HttpGet("quartoPessoaChegas/{codigoQuarto:int}")]
    public async Task<ActionResult> GetQuartoPessoaChegas(int codigoQuarto)
    {
        try
        {
            var quartoPessoaChegas = await _service.GetQuartoPessoaChegas(codigoQuarto);
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
