using CCMN_API.Models.Painel.Promocao;
using CCMZ_API;
using Microsoft.EntityFrameworkCore;

namespace CCMN_API.Services.Promocoes;

public class PromocoesService : IPromocoesService
{
    private readonly CCMNContext _context;

    public PromocoesService(CCMNContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ListarParticipantes>> GetParticipantes(int codigoPromocao)
    {
        return await _context.TbParticipantesCupons
            .Where(p => p.ParcupCodigo == codigoPromocao)
            .Select(p => new ListarParticipantes
            {
                
            }).ToListAsync();
    }

    public async Task<IEnumerable<ListarPromocoes>> GetPromocoes()
    {
        return await _context.TbPromocoes
            .Select(p => new ListarPromocoes
            {
                ProCodigo = p.ProCodigo,
                ProDatafim = p.ProDatafim,
                ProDatainicio = p.ProDatainicio,
                ProNome = p.ProNome
            }).ToListAsync();
    }
}
