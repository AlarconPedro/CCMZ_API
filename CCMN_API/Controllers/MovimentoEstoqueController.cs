using CCMN_API.Models;
using CCMN_API.Services.MovimentoProdutos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMN_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovimentoEstoqueController : ControllerBase
{
    private readonly IMovimentoEstoqueServicecs _service;

    public MovimentoEstoqueController(IMovimentoEstoqueServicecs service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<TbMovimentoProduto>> GetMovimentosEstoque()
    {
        return await _service.GetMovimentosEstoque();
    }

    [HttpGet("{codigoMovimento}")]
    public async Task<TbMovimentoProduto> GetMovimentoEstoque(int codigoMovimento)
    {
        return await _service.GetMovimentoEstoque(codigoMovimento);
    }

    [HttpPost]
    public async Task AddMovimento(TbMovimentoProduto movimentoEstoque)
    {
        await _service.AddMovimento(movimentoEstoque);
    }

    [HttpPut]
    public async Task UpdateMovimento(TbMovimentoProduto movimentoEstoque)
    {
        await _service.UpdateMovimento(movimentoEstoque);
    }

    [HttpDelete("{codigoMovimento}")]
    public async Task DeleteMovimento(int codigoMovimento)
    {
        await _service.DeleteMovimento(codigoMovimento);
    }   
}
