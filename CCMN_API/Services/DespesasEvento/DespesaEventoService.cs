
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

    public async Task<decimal> GetValorCozinha(int codigoEvento)
    {
        return await _context.TbDespesaEventos.Where(x => x.EveCodigo == codigoEvento).Select(x => x.DseCozinha).FirstOrDefaultAsync();
    }

    public async Task<decimal> GetValorHostiaria(int codigoEvento)
    {
        return await _context.TbDespesaEventos.Where(x => x.EveCodigo == codigoEvento).Select(x => x.DseHostiaria).FirstOrDefaultAsync();
    }

    public async Task AddDespesaEvento(TbDespesaEvento despesaEvento)
    {
        _context.TbDespesaEventos.Add(despesaEvento);
        await _context.SaveChangesAsync();
    }

    public async Task AddDespesaCozinha(int codigoEvento, decimal valor)
    {
        var despesa = await _context.TbDespesaEventos.Where(x => x.EveCodigo == codigoEvento).FirstOrDefaultAsync();
        if (despesa != null)
        {
            despesa.DseCozinha = valor;
            _context.TbDespesaEventos.Update(despesa);
        } else
        {
            var ultimaDespesa = await _context.TbDespesaEventos.FirstOrDefaultAsync();
            if (ultimaDespesa != null)
            {
                despesa.DseCodigo = await _context.TbDespesaEventos.MaxAsync(x => x.DseCodigo) + 1;
            }
            else
            {
                despesa.DseCodigo = 1;
            }
            _context.TbDespesaEventos.Add(new TbDespesaEvento
            {
                DseCodigo = despesa.DseCodigo,
                EveCodigo = codigoEvento,
                DseCozinha = valor
            });
        }
       
        await _context.SaveChangesAsync();
    }

    public async Task AddDespesaHostiaria(int codigoEvento, decimal valor)
    {
        var despesa = await _context.TbDespesaEventos.Where(x => x.EveCodigo == codigoEvento).FirstOrDefaultAsync();
        if (despesa != null)
        {
            despesa.DseHostiaria = valor;
            _context.TbDespesaEventos.Update(despesa);
        }
        else
        {
            var ultimaDespesa = await _context.TbDespesaEventos.FirstOrDefaultAsync();
            if (ultimaDespesa != null)
            {
               despesa.DseCodigo = await _context.TbDespesaEventos.MaxAsync(x => x.DseCodigo) + 1;
            } else
            {
                despesa.DseCodigo = 1;
            }
            _context.TbDespesaEventos.Add(new TbDespesaEvento
            {
                DseCodigo = despesa.DseCodigo,
                EveCodigo = codigoEvento,
                DseHostiaria = valor
            });
        }

        await _context.SaveChangesAsync();
    }

    public async Task UpdateDespesaEvento(TbDespesaEvento despesaEvento)
    {
        _context.Entry(despesaEvento).State = EntityState.Modified;
        await _context.SaveChangesAsync();   
    }

    public async Task UpdateDespesaCozinha(int codigoEvento, decimal valor)
    {
        var despesa = await _context.TbDespesaEventos.Where(x => x.EveCodigo == codigoEvento).FirstOrDefaultAsync();
        if (despesa != null)
        {
            despesa.DseCozinha = valor;
            _context.TbDespesaEventos.Update(despesa);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateDespesaHostiaria(int codigoEvento, decimal valor)
    {
        var despesa = await _context.TbDespesaEventos.Where(x => x.EveCodigo == codigoEvento).FirstOrDefaultAsync();
        if (despesa != null)
        {
            despesa.DseHostiaria = valor;
            _context.TbDespesaEventos.Update(despesa);
            await _context.SaveChangesAsync();
        }
    }
}
