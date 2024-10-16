﻿using CCMN_API;
using CCMN_API.Models;
using CCMN_API.Models.Painel.Hospedagem.Alocacao;
using CCMN_API.Models.Painel.Hospedagem.Comunidade;
using CCMN_API.Models.Painel.Hospedagem.Pessoas;
using CCMZ_API.Models;
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

    [HttpGet("comunidade/{codigoComunidade:int}/{cidade}")]
    public async Task<ActionResult<IEnumerable<Pessoas>>> GetPessoas(int codigoComunidade, string cidade)
    {
        try
        {
            var pessoas =  await _service.GetPessoas(codigoComunidade, cidade);
            return Ok(pessoas);
        }
        catch
        {
            return BadRequest("Erro ao trazer as pessoas !");
        }
    }

    [HttpGet("cidades")]
    public async Task<ActionResult<IEnumerable<string>>> GetCidades()
    {
        try
        {
            var cidades = await _service.GetCidades();
            return Ok(cidades);
        }
        catch
        {
            return BadRequest("Erro ao trazer as cidades !");
        }
    }

    [HttpGet("comunidades/{cidade}")]
    public async Task<ActionResult<IEnumerable<ComunidadesNome>>> GetComunidadesNomes(string cidade)
    {
        try
        {
            var comunidades = await _service.GetComunidadesNomes(cidade);
            return Ok(comunidades);
        }
        catch
        {
            return BadRequest("Erro ao trazer as comunidades !");
        }
    }

    [HttpGet("comunidade/{codigoComunidade:int}/busca/{busca}")]
    public async Task<ActionResult<IEnumerable<Pessoas>>> GetPessoasBusca(int codigoComunidade, string busca)
    {
        try
        {
            var pessoas =  await _service.GetPessoasBusca(codigoComunidade, busca);
            return Ok(pessoas);
        }
        catch
        {
            return BadRequest("Erro ao trazer as pessoas !");
        }
    }

    [HttpGet("{idPessoa:int}")]
    public async Task<ActionResult<TbPessoa>> GetPessoaId(int idPessoa)
    {
        try
        {
            var pessoa =  await _service.GetPessoaId(idPessoa);
            return Ok(pessoa);
        } catch
        {
            return BadRequest("Erro ao trazer a pessoa !");
        }
    }

    [HttpGet("detalhes/{idPessoa:int}")]
    public async Task<ActionResult<PessoaDetalhes>> GetPessoaDetalhe(int idPessoa)
    {
        try
        {
            var pessoas =  await _service.GetPessoaDetalhe(idPessoa);
            return Ok(pessoas);
        }
        catch
        {
            return BadRequest("Erro ao trazer os detalhes da pessoa !");
        }
    }

    [HttpPost]
    public async Task<ActionResult> PostPessoas(TbPessoa tbPessoa)
    {
        try
        {
            await _service.PostPessoas(tbPessoa);
            return Ok("Pessoa cadastrada com sucesso !");
        }
        catch
        {
            return BadRequest("Erro ao cadastrar a pessoa !");
        }
    }

    [HttpPut]
    public async Task<ActionResult> PutPessoas(TbPessoa tbPessoa)
    {
        try
        {
            await _service.PutPessoas(tbPessoa);
            return Ok("Pessoa atualizada com sucesso !");
        }
        catch
        {
            return BadRequest("Erro ao atualizar a pessoa !");
        }
    }

    [HttpDelete("{idPessoa:int}")]
    public async Task<ActionResult> DeletePessoas(int idPessoa)
    {
        var podeExcluir = await _service.GetPessoaPodeExcluir(idPessoa);
        if (podeExcluir) {
            try
            {
                var pessoa = await _service.GetPessoaId(idPessoa);
                if (pessoa == null)
                    return NotFound("Pessoa não encontrada !");

                await _service.DeletePessoas(pessoa);
                return Ok("Pessoa deletada com sucesso !");
            }
            catch
            {
                return BadRequest("Erro ao deletar a pessoa !");
            }
        } else
        {
            return BadRequest("Não é possível excluir essa pessoa, pois ela está vinculada há alguma atividade !");
        }
    }
}
