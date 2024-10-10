using CCMN_API.Models.Painel.Hospedagem.Promocao;
using CCMN_API.Models.Painel.Promocao;
using CCMN_API.Services.Promocoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMN_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PromocaoController : ControllerBase
{
    private readonly IPromocoesService _service;

    public PromocaoController(IPromocoesService Service)
    {
        _service = Service;
    }

    //GET
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ListarPromocoes>>> GetPromocoes()
    {
        try
        {
            var retorno = await _service.GetPromocoes();
            if (retorno != null)
            {
                return Ok(retorno);
            } else
            {
                return NotFound("Nenhuma Promoção encontrada !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar Promoções !");
        }
    }

    [HttpGet("ganhador/{filtro}/{skip:int}/{take:int}")]
    public async Task<ActionResult<IEnumerable<ListarGanhadorCupom>>> GetGanhador(string filtro, int skip, int take, string? codigoCupom)
    {
        try
        {
            var retorno = await _service.GetGanhador(filtro, skip, take, codigoCupom);
            if (retorno != null)
            {
                return Ok(retorno);
            }
            else
            {
                return NotFound("Nenhum Ganhador encontrado !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar Ganhador !" + ex.Message);
        }
    }

    [HttpGet("participantes/{codigoPromocao}")]
    public async Task<ActionResult<IEnumerable<ListarParticipantes>>> GetParticipantes(int codigoPromocao)
    {
        try
        {
            var retorno = await _service.GetParticipantes(codigoPromocao);
            if (retorno != null)
            {
                return Ok(retorno);
            } else
            {
                return NotFound("Nenhum Participante encontrado !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar Participantes !");
        }
    }

    [HttpGet("sorteios")]
    public async Task<ActionResult<IEnumerable<ListarSorteios>>> GetSorteios()
    {
        try
        {
            var retorno = await _service.GetSorteios();
            if (retorno != null)
            {
                return Ok(retorno);
            } else
            {
                return NotFound("Nenhum Sorteio encontrado !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar Sorteios !");
        }
    }

    [HttpGet("participante/dados/{cpfParticipantes}")]
    public async Task<ActionResult<TbPromocoesParticipante>> GetDadosParticipantes(string cpfParticipantes)
    {
        try
        {
            var retorno = await _service.GetDadosParticipantes(cpfParticipantes);
            if (retorno != null)
            {
                return Ok(retorno);
            } else
            {
                return NotFound("Nenhum Participante encontrado !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar Participantes !");
        }
    }

    [HttpGet("cupons/{codigoParticipante}")]
    public async Task<ActionResult<IEnumerable<TbPromocoesCupon>>> GetCuponsParticipante(int codigoParticipante)
    {
        try
        {
            var retorno = await _service.GetCuponsParticipante(codigoParticipante);
            if (retorno != null)
            {
                return Ok(retorno);
            } else
            {
                return NotFound("Nenhum Cupom encontrado !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar Cupons !");
        }
    }

    [HttpGet("sortear/cupom/{cupom}")]
    public async Task<ActionResult<ListarGanhadorCupom>> SortearCupom(string cupom)
    {
        try
        {
            var retorno = await _service.SortearCupom(cupom);
            if (retorno.Item1.Equals(200))
            {
                return Ok(retorno.Item2);
            }
            else if (retorno.Item1.Equals(404))
            {
                return NotFound("Nenhum Cupom encontrado !");
            } else if (retorno.Item1.Equals(400))
            {
                return BadRequest("Cupom já sorteado !");
            } else if (retorno.Item1.Equals(401))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Cupom não vendido !");
            } else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao sortear Cupom !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao sortear Cupom !");
        }
    }

    //POST
    [HttpPost("participante")]
    public async Task<ActionResult<TbPromocoesParticipante>> AddParticipantes(TbPromocoesParticipante participantes)
    {
        try
        {
            var codigoParticipante = await _service.AddParticipantes(participantes);
            return Ok(codigoParticipante);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar Participante !");
        }
    }

    [HttpPost("cupons")]
    public async Task<ActionResult> AddCupons(TbPromocoesCupon cupons)
    {
        try
        {
            var retorno = await _service.AddCupons(cupons);
            if (retorno.Item1)
                return Ok(retorno.Item2);
            else if (retorno.Item1 == false && retorno.Item2 == "Nenhum Cupom Encontrado !")
                return NotFound("Cupom não encontrado !");
            else
                return BadRequest(retorno.Item2);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar Cupom !");
        }
    }

    [HttpPost("sorteios")]
    public async Task<ActionResult> AddSorteios(TbPromocoesSorteio sorteios)
    {
        try
        {
            await _service.AddSorteios(sorteios);
            return Ok("Sorteio adicionado com sucesso !");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar Sorteio !");
        }
    }

    [HttpPost("promocoes")]
    public async Task<ActionResult> AddPromocoes(TbPromoco promocoes)
    {
        try
        {
            await _service.AddPromocoes(promocoes);
            return Ok("Promoção adicionada com sucesso !");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar Promoção !");
        }
    }

    //PUT
    [HttpPut("participantes")]
    public async Task<ActionResult> UpdateParticipantes(TbPromocoesParticipante participantes)
    {
        try
        {
            await _service.UpdateParticipantes(participantes);
            return Ok("Participante atualizado com sucesso !");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar Participante !");
        }
    }

    [HttpPut("cupons")]
    public async Task<ActionResult> UpdateCupons(TbPromocoesCupon cupons)
    {
        try
        {
            await _service.UpdateCupons(cupons);
            return Ok("Cupom atualizado com sucesso !");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar Cupom !");
        }
    }

    [HttpPut("sorteios")]
    public async Task<ActionResult> UpdateSorteios(TbPromocoesSorteio sorteios)
    {
        try
        {
            await _service.UpdateSorteios(sorteios);
            return Ok("Sorteio atualizado com sucesso !");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar Sorteio !");
        }
    }

    [HttpPut("promocoes")]
    public async Task<ActionResult> UpdatePromocoes(TbPromoco promocoes)
    {
        try
        {
            await _service.UpdatePromocoes(promocoes);
            return Ok("Promoção atualizada com sucesso !");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar Promoção !");
        }
    }

    //DELETE
    [HttpDelete("participantes/{codigoParticipante}")]
    public async Task<ActionResult> DeleteParticipantes(int codigoParticipante)
    {
        try
        {
            await _service.DeleteParticipantes(codigoParticipante);
            return Ok("Participante deletado com sucesso !");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar Participante !");
        }
    }

    [HttpDelete("cupons/{codigoCupom}")]
    public async Task<ActionResult> DeleteCupons(int codigoCupom)
    {
        try
        {
            await _service.DeleteCupons(codigoCupom);
            return Ok("Cupom deletado com sucesso !");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar Cupom !");
        }
    }

    [HttpDelete("sorteios/{codigoSorteio}")]
    public async Task<ActionResult> DeleteSorteios(int codigoSorteio)
    {
        try
        {
            await _service.DeleteSorteios(codigoSorteio);
            return Ok("Sorteio deletado com sucesso !");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar Sorteio !");
        }
    }

    [HttpDelete("promocoes/{codigoPromocao}")]
    public async Task<ActionResult> DeletePromocoes(int codigoPromocao)
    {
        try
        {
            await _service.DeletePromocoes(codigoPromocao);
            return Ok("Promoção deletada com sucesso !");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar Promoção !");
        }
    }
}
