using CCMZ_API.Models;
using CCMZ_API.Models.Painel.Bloco;
using CCMZ_API.Models.Painel.Comunidade;
using CCMZ_API.Models.Painel.Quartos;
using CCMZ_API.Services.Eventos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMZ_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly IEventosService _service;

    public EventoController(IEventosService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TbEvento>>> GetEventos()
    {
        try
        {
            var eventos = await _service.GetEventos();
            if (eventos == null)
                return NotFound("Nenhum evento encontrado !");

            return Ok(eventos);
        } catch
        {
            return BadRequest("Erro ao trazer os eventos !");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TbEvento>> GetEvento(int id)
    {
        try
        {
            var evento = await _service.GetEvento(id);
            if (evento != null)
                return NotFound($"Nennhum evento encontrado com o id {id} !");

            return Ok(evento);
        } catch
        {
            return BadRequest("Erro ao trazer o evento !");
        }
    }

    [HttpGet("pavilhoes")]
    public async Task<ActionResult<IEnumerable<BlocoNome>>> GetPavilhoes()
    {
        try
        {
            var pavilhoes = await _service.GetPavilhoes();
            if (pavilhoes == null)
                return NotFound("Nenhum pavilhão encontrado !");

            return Ok(pavilhoes);
        } catch
        {
            return BadRequest("Erro ao trazer os pavilhões !");
        }
    }

    [HttpGet("quartos/{codigoPavilhao:int}")]
    public async Task<ActionResult<IEnumerable<QuartoPavilhao>>> GetQuartosPavilhao(int codigoPavilhao)
    {
        try
        {
            var quartos = await _service.GetQuartosPavilhao(codigoPavilhao);
            if (quartos == null)
                return NotFound($"Nenhum quarto encontrado para o pavilhão com o código {codigoPavilhao} !");

            return Ok(quartos);
        } catch
        {
            return BadRequest($"Erro ao trazer os quartos do pavilhão com o código {codigoPavilhao} !");
        }
    }

    [HttpGet("comunidades")]
    public async Task<ActionResult<IEnumerable<ComunidadeNome>>> GetComunidades()
    {
        try
        {
            var comunidades = await _service.GetComunidades();
            if (comunidades == null)
                return NotFound("Nenhuma comunidade encontrada !");

            return Ok(comunidades);
        } catch
        {
            return BadRequest("Erro ao trazer as comunidades !");
        }
    }

    [HttpPost]
    public async Task<ActionResult> PostEvento(TbEvento evento)
    {
        try
        {
            await _service.PostEvento(evento);
            return Ok("Evento cadastrado com sucesso !");
        }catch
        {
            return BadRequest("Erro ao cadastrar o Evento !");
        }
    }

    [HttpPost("quartos")]
    public async Task<ActionResult> PostQuartos(List<TbEventoQuarto> eventoQuarto)
    {
        try
        {
            await _service.PostQuartos(eventoQuarto);
            return Ok("Quartos cadastrados com sucesso !");
        }catch
        {
            return BadRequest("Erro ao cadastrar os quartos !");
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateEvento(TbEvento evento)
    {
        try
        {
            await _service.UpdateEvento(evento);
            return Ok("Evento Atualizado com sucesso !");
        }
        catch
        {
            return BadRequest($"Erro ao atualizar o Evento com o id {evento.EveCodigo} !");
        }
    }

    [HttpDelete("{idEvento:int}")]
    public async Task<ActionResult> DeleteEvento(int idEvento)
    {
        try
        {
            var eventoToDelete = await _service.GetEvento(idEvento);
            if (eventoToDelete == null)
                return NotFound($"Nenhum Evento encontrado com o id {idEvento} !");

            await _service.DeleteEvento(eventoToDelete);
            return Ok($"Evento com o id {idEvento} deletado com sucesso!");
        }catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                               $"Erro ao deletar o Evento com o id {idEvento} !");
        }
    }
}
