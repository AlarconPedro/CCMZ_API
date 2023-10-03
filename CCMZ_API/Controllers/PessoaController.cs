using CCMZ_API.Models;
using CCMZ_API.Models.Painel.Pessoas;
using CCMZ_API.Services.Pessoas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMZ_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PessoaController : ControllerBase
{
    private readonly IPessoasService _service;

    public PessoaController(IPessoasService pessoasService)
    {
        _service = pessoasService;
    }

    [HttpGet]
    public async Task<IEnumerable<Pessoas>> GetPessoas(int skip, int take)
    {
        return await _service.GetPessoas(skip, take);
    }

    [HttpGet("{idPessoa:int}")]
    public async Task<PessoaDetalhes> GetPessoaDetalhe(int idPessoa)
    {
        return await _service.GetPessoaDetalhe(idPessoa);
    }

    [HttpPost]
    public async Task PostPessoas(TbPessoa tbPessoa)
    {
        await _service.PostPessoas(tbPessoa);
    }

    [HttpPut]
    public async Task PutPessoas(TbPessoa tbPessoa)
    {
        await _service.PutPessoas(tbPessoa);
    }

    [HttpDelete]
    public async Task DeletePessoas(TbPessoa tbPessoa)
    {
        await _service.DeletePessoas(tbPessoa);
    }
}
