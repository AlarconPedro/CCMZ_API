using CCMZ_API.Models;
using CCMZ_API.Services.Quartos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMZ_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuartoController : ControllerBase
{
    private readonly IQuartosService _service;

    public QuartoController(IQuartosService quartosService)
    {
        _service = quartosService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TbQuarto>>> GetQuartos()
    {
        return Ok(await _service.GetQuartos()); 
    }

    [HttpPost]
    public async Task<ActionResult> PostQuarto(TbQuarto tbQuarto)
    {
        await _service.PostQuarto(tbQuarto);
        return Ok("Usuário cadastrado com sucesso !");
    }

    [HttpPut]
    public async Task<ActionResult> UpdateQuarto(TbQuarto tbQuarto)
    {
        await _service.UpdateQuarto(tbQuarto);
        return Ok("Usuário atualizado com sucesso !");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteQuarto(int id)
    {
        var quarto = await _service.GetQuartoById(id);
        if (quarto != null)
        {
            await _service.DeleteQuarto(quarto);
            return Ok("Usuário Excluído com sucesso !");
        } else
        {
            return BadRequest("Usuário não encontrado !");
        }
    }
}
