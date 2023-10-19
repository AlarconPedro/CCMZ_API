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
        try
        {
            return Ok(await _service.GetQuartos());

        }
        catch
        {
            return BadRequest("Request Inválido !");
        }
    }

    [HttpPost]
    public async Task<ActionResult> PostQuarto(TbQuarto tbQuarto)
    {
        try
        {
            await _service.PostQuarto(tbQuarto);
            return Ok("Quarto cadastrado com sucesso !");
        }
        catch
        {
            return BadRequest("Request Inválido !");
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateQuarto(TbQuarto tbQuarto)
    {
        try {
            await _service.UpdateQuarto(tbQuarto);
            return Ok("Quarto atualizado com sucesso !");
        } catch
        {
            return BadRequest("Request Inválido !");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteQuarto(int id)
    {
        try
        {
            var quarto = await _service.GetQuartoById(id);
            if (quarto != null)
            {
                await _service.DeleteQuarto(quarto);
                return Ok("Quarto Excluído com sucesso !");
            }
            else
            {
                return BadRequest("Quarto não encontrado !");
            }
        } catch
        {
            return BadRequest("Request Inválido !");
        }
       
    }
}
