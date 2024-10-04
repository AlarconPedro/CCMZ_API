using CCMN_API.Models;
using CCMN_API.Services.Produtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMN_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _service;

    public ProdutoController(IProdutoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<TbProduto>> GetProdutos()
    {
        return await _service.GetProdutos();
    }

    [HttpGet("{codigoProduto}")]
    public async Task<TbProduto> GetProduto(int codigoProduto)
    {
        return await _service.GetProduto(codigoProduto);
    }

    [HttpPost]
    public async Task AddProduto(TbProduto produto)
    {
        await _service.AddProduto(produto);
    }

    [HttpPut]
    public async Task UpdateProduto(TbProduto produto)
    {
        await _service.UpdateProduto(produto);
    }

    [HttpDelete("{codigoProduto}")]
    public async Task DeleteProduto(int codigoProduto)
    {
        try
        {
            await _service.DeleteProduto(codigoProduto);
        }catch (Exception ex)
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            await Response.WriteAsync(ex.Message);
        }
    }
}
