﻿using CCMN_API.Models;
using CCMN_API.Services.Categorias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMN_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _service;

    public CategoriaController(ICategoriaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<TbCategoria>> GetCategorias()
    {
        return await _service.GetCategorias();
    }

    [HttpGet("{codigoCategoria}")]
    public async Task<TbCategoria> GetCategoria(int codigoCategoria)
    {
        return await _service.GetCategoria(codigoCategoria);
    }

    [HttpPost]
    public async Task AddCategoria(TbCategoria categoria)
    {
        await _service.AddCategoria(categoria);
    }

    [HttpPut]
    public async Task UpdateCategoria(TbCategoria categoria)
    {
        await _service.UpdateCategoria(categoria);
    }

    [HttpDelete("{codigoCategoria}")]
    public async Task DeleteCategoria(int codigoCategoria)
    {
        await _service.DeleteCategoria(codigoCategoria);
    }
}
