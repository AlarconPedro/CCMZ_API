
using CCMN_API.Models;
using CCMN_API.Models.Painel.Acerto;
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

    public async Task<IEnumerable<ComunidadeEventoDados>> GetComunidadesDados(int codigoEvento)
    {
        return await _context.TbEventoPessoas.Where(x => x.EveCodigo == codigoEvento).Select(x => new ComunidadeEventoDados
        {
            ComCodigo = x.PesCodigoNavigation.ComCodigoNavigation.ComCodigo,
            ComNome = x.PesCodigoNavigation.ComCodigoNavigation.ComNome,
            PagantesCobrantes = new PessoasPagantesCobrantes
            {
                Cobrantes = _context.TbEventoPessoas.Where(ep => ep.EveCodigo == codigoEvento && ep.PesCodigoNavigation.ComCodigo == x.PesCodigoNavigation.ComCodigoNavigation.ComCodigo && x.EvpCobrante == true && x.PesCodigoNavigation.TbQuartoPessoas.Any(qp => qp.PesNaovem) == false).Count(),
                Pagantes = _context.TbEventoPessoas.Where(ep => ep.EveCodigo == codigoEvento && ep.PesCodigoNavigation.ComCodigo == x.PesCodigoNavigation.ComCodigoNavigation.ComCodigo && x.EvpPagante == true && x.PesCodigoNavigation.TbQuartoPessoas.Any(qp => qp.PesNaovem) == false).Count(),
            }
        }).Distinct().ToListAsync();
    }

    public async Task<IEnumerable<TbDespesaEvento>> GetDespesasEvento(int codigoEvento)
    {
        return await _context.TbDespesaEventos.Where(x => x.EveCodigo == codigoEvento).ToListAsync();
    }

    /*public async Task<IEnumerable<ComunidadeNome>> GetComunidadesEvento(int codigoEvento)
    {
        return await _context.TbEventoPessoas.Where(x => x.EveCodigo == codigoEvento).Select(x => new ComunidadeNome
        {
            ComCodigo = x.PesCodigoNavigation.ComCodigoNavigation.ComCodigo,
            ComNome = x.PesCodigoNavigation.ComCodigoNavigation.ComNome,
            ComCidade = x.PesCodigoNavigation.ComCodigoNavigation.ComCidade,
        }).Distinct().ToListAsync();
    }*/

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
        var retorno = await _context.TbEventos.Where(x => x.EveCodigo == codigoEvento).Select(x => x.DseCozinha).FirstOrDefaultAsync();
        if (retorno != null)
        {
            return (decimal)retorno;
        } else
        {
            return 0;
        }
    }

    public async Task<decimal> GetValorHostiaria(int codigoEvento)
    {
        var retorno = await _context.TbEventos.Where(x => x.EveCodigo == codigoEvento).Select(x => x.DseHostiaria).FirstOrDefaultAsync();
        if (retorno != null)
        {
            return (decimal)retorno;
        }
        else
        {
            return 0;
        }
    }

    public async Task AddDespesaEvento(TbDespesaEvento despesaEvento)
    {
        var despesa = await _context.TbDespesaEventos.FirstOrDefaultAsync();
        if (despesa != null)
        {
            despesaEvento.DseCodigo = await _context.TbDespesaEventos.MaxAsync(x => x.DseCodigo) + 1;
        } else
        {
            despesaEvento.DseCodigo = 1;
        }
        _context.TbDespesaEventos.Add(despesaEvento);
        await _context.SaveChangesAsync();
    }

    public async Task AddDespesaCozinha(int codigoEvento, decimal valor)
    {
        var despesa = await _context.TbEventos.Where(x => x.EveCodigo == codigoEvento).FirstOrDefaultAsync();
        if (despesa != null)
        {
            despesa.DseCozinha = valor;
            _context.TbEventos.Update(despesa);
        }
        await _context.SaveChangesAsync();
    }

    public async Task AddDespesaHostiaria(int codigoEvento, decimal valor)
    {
        var despesa = await _context.TbEventos.Where(x => x.EveCodigo == codigoEvento).FirstOrDefaultAsync();
        if (despesa != null)
        {
            despesa.DseHostiaria = valor;
            _context.TbEventos.Update(despesa);
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
        var despesa = await _context.TbEventos.Where(x => x.EveCodigo == codigoEvento).FirstOrDefaultAsync();
        if (despesa != null)
        {
            despesa.DseCozinha = valor;
            _context.TbEventos.Update(despesa);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateDespesaHostiaria(int codigoEvento, decimal valor)
    {
        var despesa = await _context.TbEventos.Where(x => x.EveCodigo == codigoEvento).FirstOrDefaultAsync();
        if (despesa != null)
        {
            despesa.DseHostiaria = valor;
            _context.TbEventos.Update(despesa);
            await _context.SaveChangesAsync();
        }
    }
}
