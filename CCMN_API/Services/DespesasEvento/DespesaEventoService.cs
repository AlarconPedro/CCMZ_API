
using CCMN_API.Models.Painel.Evento;
using CCMN_API.Models.Painel.EventoDespesas;
using CCMZ_API;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CCMN_API.Services.DespesasEvento;

public class DespesaEventoService : IDespesaEventoService
{
    private readonly CCMNContext _context;

    public DespesaEventoService(CCMNContext context)
    {
        _context = context;
    }

    public async Task<PessoasPagantesCobrantes> GetCobrantesPagantes(int codigoEvento)
    {
        return await _context.TbEventoPessoas.Where(x => x.EveCodigo == codigoEvento).Select(x => new PessoasPagantesCobrantes
        {
            Cobrantes = _context.TbEventoPessoas.Where(x => x.EveCodigo == codigoEvento && x.EvpCobrante == true && x.PesCodigoNavigation.TbQuartoPessoas.Any(qp => qp.PesNaovem) == false).Count(),
            Pagantes = _context.TbEventoPessoas.Where(x => x.EveCodigo == codigoEvento && x.EvpPagante == true && x.PesCodigoNavigation.TbQuartoPessoas.Any(qp => qp.PesNaovem) == false).Count(),
        }).FirstOrDefaultAsync();
    }

    public async Task<TbDespesaEvento> GetDespesasEvento(int codigoEvento)
    {
        return await _context.TbDespesaEventos.Where(x => x.EveCodigo == codigoEvento).FirstOrDefaultAsync();
    }

    public async Task<EventoCusto> GetEventoCusto(int codigoEvento)
    {
        return await _context.TbEventos.Where(x => x.EveCodigo == codigoEvento).Select(x => new EventoCusto
        {
            EveValor = x.EveValor,
            EveTipoCobranca = x.EveTipoCobranca
        }).FirstOrDefaultAsync();
    }
}
