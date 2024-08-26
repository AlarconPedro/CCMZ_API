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
}
