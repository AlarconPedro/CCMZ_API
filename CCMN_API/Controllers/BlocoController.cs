using CCMN_API;
using CCMN_API.Models;
using CCMZ_API.Models;
using CCMZ_API.Services.Blocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMZ_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlocoController : ControllerBase
{
    private readonly IBlocosService _service;

    public BlocoController(IBlocosService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TbBloco>>> GetBlocos()
    {
        try
        {
            var blocos = await _service.GetBlocos();
            if (blocos == null)
                return NotFound("Nennhum bloco cadastrado !");

            return Ok(blocos);
        }catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter os blocos !");
        }
    }

    [HttpGet("nomes")]
    public async Task<ActionResult<IEnumerable<TbBloco>>> GetBlocosNomes()
    {
        try
        {
            var blocos = await _service.GetBlocosNomes();
            if (blocos == null)
                return NotFound("Nennhum bloco cadastrado !");

            return Ok(blocos);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter os blocos !");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TbBloco>> GetBloco(int id)
    {
        try
        {
            var bloco = await _service.GetBloco(id);
            if (bloco == null)
                return NotFound($"Bloco de id {id} não encontrado !");

            return Ok(bloco);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar obter o bloco de id {id} !");
        }
    }

    [HttpPost]
    public async Task<ActionResult> PostBloco(TbBloco bloco)
    {
        try
        {
            await _service.PostBloco(bloco);
            return Ok("Bloco cadastrado com sucesso !");
        }catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar cadastrar o bloco !");
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateBloco(TbBloco bloco)
    {
       try
        {
            await _service.UpdateBloco(bloco);
            return Ok("Bloco atualizado com sucesso !");
        }catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar atualizar o bloco !");
        }
    }

    [HttpDelete("{idBloco:int}")]
    public async Task<ActionResult> DeleteBloco(int idBloco)
    {
        try
        {
            var bloco = await _service.GetBloco(idBloco);
            if (bloco == null)
                return NotFound($"Bloco de id {idBloco} não encontrado !");

            await _service.DeleteBloco(bloco);
            return Ok($"Bloco de id {idBloco} deletado com sucesso !");
        }catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar o bloco de id {idBloco} !");
        }
    }
}
