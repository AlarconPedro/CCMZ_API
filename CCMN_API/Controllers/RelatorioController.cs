using CCMN_API.Services.Relatorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCMN_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RelatorioController : ControllerBase
{
    private readonly IRelatoriosService _service;

    public RelatorioController(IRelatoriosService service)
    {
        _service = service;
    }

    [HttpGet("ProdutosAcabando")]
    public async Task<IActionResult> GetProdutosAcabando()
    {
        try
        {
            var produtos = await _service.GetProdutosAcabando();
            return Ok(produtos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
