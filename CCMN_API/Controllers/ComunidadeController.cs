using CCMN_API.Models;
using CCMZ_API.Models;
using CCMZ_API.Services.Comunidade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMZ_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComunidadeController : ControllerBase
{
    private readonly IComunidadeService _service;

    public ComunidadeController(IComunidadeService service)
    {
        _service = service;
    }

    [HttpGet("cidades")]
    public async Task<ActionResult<IEnumerable<string>>> GetCidadesComunidades()
    {
        try
        {
            var cidades = await _service.GetCidadesComunidades();
            return Ok(cidades);
        }catch
        {
            return BadRequest("Erro ao trazer as cidades das comunidades !");
        }
    }

    [HttpGet("{cidade}")]
    public async Task<ActionResult<IEnumerable<TbComunidade>>> GetComunidades(string cidade)
    {
        try
        {
            var comunidades = await _service.GetComunidades(cidade);
            return Ok(comunidades);
        }catch
        {
            return BadRequest("Erro ao trazer as comunidades !");
        }
    }

    [HttpGet("nomes")]
    public async Task<ActionResult<IEnumerable<TbComunidade>>> GetComunidadesNomes()
    {
        try
        {
            var comunidades = await _service.GetComunidadesNomes();
            return Ok(comunidades);
        }catch
        {
            return BadRequest("Erro ao trazer as comunidades !");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TbComunidade>> GetComunidade(int id)
    {
        try
        {
            var comunidade = await _service.GetComunidade(id);
            if (comunidade != null)
                return Ok(comunidade);

            return NotFound("Comunidade não encontrada !");
        }catch
        {
            return BadRequest("Erro ao trazer a comunidade !");
        }
    }

    [HttpPost]
    public async Task<ActionResult> PostComunidade(TbComunidade comunidade)
    {
        try
        {
            await _service.PostComunidade(comunidade);
            return Ok("Comunidade cadastrada com sucesso !");
        }catch
        {
            return BadRequest("Erro ao cadastrar a comunidade !");
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateComunidade(TbComunidade comunidade)
    {
        try
        {
            await _service.UpdateComunidade(comunidade);
            return Ok("Comunidade atualizada com sucesso !");
        }catch
        {
            return BadRequest("Erro ao atualizar a comunidade !");
        }
    }

    [HttpDelete("{comunidadeId:int}")]
    public async Task<ActionResult> DeleteComunidade(int comunidadeId)
    {
        try
        {
            var comunidade = await _service.GetComunidade(comunidadeId);
            if (comunidade == null)
                return NotFound($"Comunidade de id {comunidadeId} não encontrada !");

            await _service.DeleteComunidade(comunidade);
            return Ok("Comunidade deletada com sucesso !");
        }catch
        {
            return BadRequest("Erro ao deletar a comunidade !");
        }
    }
}
