
using CCMN_API.Models.Painel.Evento;
using CCMN_API.Models.Painel.EventoDespesas;
using CCMZ_API;
using CCMZ_API.Models.Painel.Alocacao;
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

    public async Task<IEnumerable<ComunidadeNome>> GetComunidadesEvento(int codigoEvento)
    {
        return await _context.TbEventoPessoas.Where(x => x.EveCodigo == codigoEvento).Select(x => new ComunidadeNome
        {
            ComCodigo = x.PesCodigoNavigation.ComCodigoNavigation.ComCodigo,
            ComNome = x.PesCodigoNavigation.ComCodigoNavigation.ComNome
        }).Distinct().ToListAsync();
    }

    public async Task<EventoCusto> GetEventoCusto(int codigoEvento)
    {
        return await _context.TbEventos.Where(x => x.EveCodigo == codigoEvento).Select(x => new EventoCusto
        {
            EveValor = x.EveValor,
            EveTipoCobranca = x.EveTipoCobranca
        }).FirstOrDefaultAsync();
    }

    public async Task AddDespesaEvento(TbDespesaEvento despesaEvento)
    {
        _context.TbDespesaEventos.Add(despesaEvento);
        await _context.SaveChangesAsync();
    }
        
    public async Task UpdateDespesaEvento(TbDespesaEvento despesaEvento)
    {
        _context.Entry(despesaEvento).State = EntityState.Modified;
        await _context.SaveChangesAsync();    }

    Task<TbDespesaEvento> IDespesaEventoService.AddDespesaEvento(TbDespesaEvento despesaEvento)
    {
        throw new NotImplementedException();
    }

    Task<TbDespesaEvento> IDespesaEventoService.UpdateDespesaEvento(TbDespesaEvento despesaEvento)
    {
        throw new NotImplementedException();
    }
}
