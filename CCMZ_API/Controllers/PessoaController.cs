using CCMZ_API.Models;
using CCMZ_API.Services.Pessoas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMZ_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PessoaController : ControllerBase
{
    private readonly IPessoasService _pessoasService;

    public PessoaController(IPessoasService pessoasService)
    {
        _pessoasService = pessoasService;
    }

    [HttpGet]
    public async Task<IEnumerable<TbPessoa>> GetPessoas()
    {
        return await _pessoasService.GetPessoas();
    }

    [HttpPost]
    public async Task PostPessoas(TbPessoa tbPessoa)
    {
        await _pessoasService.PostPessoas(tbPessoa);
    }

    [HttpPut]
    public async Task PutPessoas(TbPessoa tbPessoa)
    {
        await _pessoasService.PutPessoas(tbPessoa);
    }

    [HttpDelete]
    public async Task DeletePessoas(TbPessoa tbPessoa)
    {
        await _pessoasService.DeletePessoas(tbPessoa);
    }
}
