using CCMN_API;
using CCMN_API.Models.Painel.Pessoas;
using CCMZ_API.Models;
using CCMZ_API.Models.Painel.Alocacao;
using CCMZ_API.Models.Painel.Pessoas;
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

    [HttpGet("mes/{mes:int}")]
    public async Task<ActionResult<IEnumerable<TbEvento>>> GetEventos(int mes)
    {
        try
        {
            var eventos = await _service.GetEventos(mes);
            if (eventos == null)
                return NotFound("Nenhum evento encontrado !");

            return Ok(eventos);
        } catch
        {
            return BadRequest("Erro ao trazer os eventos !");
        }
    }

    [HttpGet("ativos")]
    public async Task<ActionResult<IEnumerable<EventosNome>>> GetEventosAtivos()
    {
        try
        {
            var eventos = await _service.GetEventosAtivos();
            if (eventos == null)
                return NotFound("Nenhum evento encontrado !");

            return Ok(eventos);
        } catch
        {
            return BadRequest("Erro ao trazer os eventos !");
        }
    }

    [HttpGet("nomes")]
    public async Task<ActionResult<IEnumerable<EventosNome>>> GetEventoNome()
    {
        try
        {
            var eventos = await _service.GetEventoNome();
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
                return Ok(evento);

            return NotFound($"Nennhum evento encontrado com o id {id} !");
            
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

    [HttpGet("quartos/{codigoPavilhao:int}/{codigoEvento:int}")]
    public async Task<ActionResult<IEnumerable<QuartoPavilhao>>> GetQuartosPavilhao(int codigoPavilhao, int codigoEvento)
    {
        try
        {
            var quartos = await _service.GetQuartosPavilhao(codigoPavilhao, codigoEvento);
            if (quartos == null)
                return NotFound($"Nenhum quarto encontrado para o pavilhão com o código {codigoPavilhao} !");

            return Ok(quartos);
        } catch
        {
            return BadRequest($"Erro ao trazer os quartos do pavilhão com o código {codigoPavilhao} !");
        }
    }

    [HttpGet("quartos/alocados/{codigoPavilhao:int}/{codigoEvento:int}")]
    public async Task<ActionResult<IEnumerable<QuartoPavilhao>>> GetQuartosAlocados(int codigoPavilhao, int codigoEvento)
    {
        try
        {
            var quartos = await _service.GetQuartosAlocados(codigoPavilhao, codigoEvento);
            if (quartos == null)
                return NotFound($"Nenhum quarto encontrado para o pavilhão com o código {codigoPavilhao} !");

            return Ok(quartos);
        } catch
        {
            return BadRequest($"Erro ao trazer os quartos do pavilhão com o código {codigoPavilhao} !");
        }
    }

    [HttpGet("pessoas/{codigoComunidade:int}/{codigoEvento:int}")]
    public async Task<ActionResult<IEnumerable<PessoaEvento>>> GetEventoPessoas(int codigoComunidade, int codigoEvento)
    {
        try
        {
            var pessoas = await _service.GetPessoaEvento(codigoComunidade, codigoEvento);
            if (pessoas == null)
                return NotFound($"Nenhuma pessoa encontrada para a comunidade com o código {codigoComunidade} !");

            return Ok(pessoas);
        } catch
        {
            return BadRequest($"Erro ao trazer as pessoas da comunidade com o código {codigoComunidade} !");
        }
    }

    [HttpGet("pessoas/alocadas/{codigoComunidade:int}/{codigoEvento:int}")]
    public async Task<ActionResult<IEnumerable<PessoaQuarto>>> GetPessoasAlocadas(int codigoComunidade, int codigoEvento)
    {
        try
        {
            var pessoas = await _service.GetPessoasAlocadas(codigoComunidade, codigoEvento);
            if (pessoas == null)
                return NotFound($"Nenhuma pessoa encontrada para a comunidade com o código {codigoComunidade} !");

            return Ok(pessoas);
        } catch
        {
            return BadRequest($"Erro ao trazer as pessoas da comunidade com o código {codigoComunidade} !");
        }
    }

    [HttpGet("pessoas/quarto/{codigoQuarto:int}/{codigoEvento:int}")]
    public async Task<ActionResult<IEnumerable<PessoaQuarto>>> GetPessoasQuarto(int codigoQuarto, int codigoEvento)
    {
        try
        {
            var pessoas = await _service.GetPessoasQuarto(codigoQuarto, codigoEvento);
            if (pessoas == null)
                return NotFound($"Nenhuma pessoa encontrada para o quarto com o código {codigoQuarto} !");

            return Ok(pessoas);
        } catch
        {
            return BadRequest($"Erro ao trazer as pessoas do quarto com o código {codigoQuarto} !");
        }
    }

/*    [HttpGet("quartos/{codigoEvento}/{codigoBloco}")]
    public async Task<ActionResult> GetQuartos(int codigoEvento, int codigoBloco)
    {
        try
        {
            var quartos = await _service.GetQuartos(codigoEvento, codigoBloco);
            return Ok(quartos);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }*/

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

    [HttpGet("hospedes/{codigoEvento:int}")]
    public async Task<ActionResult<IEnumerable<Hospedes>>> GetHospedes(int codigoEvento)
    {
        try
        {
            var hospedes = await _service.GetHospedes(codigoEvento);
            if (hospedes == null)
                return NotFound($"Nenhum hospede encontrado para o evento com o código {codigoEvento} !");

            return Ok(hospedes);
        } catch
        {
            return BadRequest($"Erro ao trazer os hospedes do evento com o código {codigoEvento} !");
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

    [HttpPost("quartos/{codigo:int}")]
    public async Task<ActionResult> PostQuartos(List<TbEventoQuarto> eventoQuarto, int codigo)
    {
        try
        {
            await _service.PostQuartos(eventoQuarto, codigo);
            return Ok("Quartos cadastrados com sucesso !");
        }catch
        {
            return BadRequest("Erro ao cadastrar os quartos !");
        }
    }

    [HttpPost("pessoas")]
    public async Task<ActionResult> PostPessoas(List<TbEventoPessoa> eventoPessoa)
    {
        try
        {
            await _service.PostPessoas(eventoPessoa);
            return Ok("Pessoas cadastradas com sucesso !");
        }catch (Exception ex)
        {
            return BadRequest($"Erro ao cadastrar as pessoas ! {ex}");
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

    [HttpPut("quartos")]
    public async Task<ActionResult> UpdateEventoQuarto(TbEventoQuarto eventoQuarto)
    {
        try
        {
            await _service.UpdateEventoQuarto(eventoQuarto);
            return Ok("Quarto Atualizado com sucesso !");
        }
        catch
        {
            return BadRequest($"Erro ao atualizar o Quarto com o id {eventoQuarto.EvqCodigo} !");
        }
    }

    [HttpPut("pessoas")]
    public async Task<ActionResult> UpdateEventoPessoa(TbEventoPessoa eventoPessoa)
    {
        try
        {
            await _service.UpdateEventoPessoa(eventoPessoa);
            return Ok("Pessoa Atualizada com sucesso !");
        }
        catch
        {
            return BadRequest($"Erro ao atualizar a Pessoa com o id {eventoPessoa.EvpCodigo} !");
        }
    }

    [HttpDelete("quarto/{codigoQuarto:int}")]
    public async Task<ActionResult> RemoveQuartoEvento(int codigoQuarto)
    {
        try
        {
            await _service.RemoveQuartoEvento(codigoQuarto);
            return Ok($"Quarto com o id {codigoQuarto} removido com sucesso !");
        }catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                              $"Erro ao remover o Quarto com o id {codigoQuarto} !");
        }
    }

    [HttpDelete("pessoas")]
    public async Task<ActionResult> DeleteEventoPessoas(List<TbEventoPessoa> evento)
    {
        try
        {
            await _service.DeleteEventoPessoas(evento);
            return Ok("Pessoas deletadas com sucesso !");
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                              $"Erro ao deletar as Pessoas !");
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
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                               $"Erro ao deletar o Evento com o id {idEvento} !");
        }
    }

    [HttpDelete("pessoas/{codigoEvento:int}")]
    public async Task<ActionResult> RemoverPessoasEvento(List<int> codigoPessoas, int codigoEvento)
    {
        try
        {
            await _service.RemoverPessoasEvento(codigoPessoas, codigoEvento);
            return Ok("Pessoas removidas com sucesso !");
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                              $"Erro ao remover as Pessoas !");
        }
    }
}
