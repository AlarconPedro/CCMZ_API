using CCMZ_API.Models;
using CCMZ_API.Models.Painel.Alocacao;
using CCMZ_API.Models.Painel.Pessoas;
using CCMZ_API.Models.Painel.Quartos;
using Microsoft.EntityFrameworkCore;

namespace CCMZ_API.Services.Eventos;

public class EventosService : IEventosService
{
    private readonly CCMZContext _context;

    public EventosService(CCMZContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TbEvento>> GetEventos()
    {
        return await _context.TbEventos.ToListAsync();
    }

    public async Task<IEnumerable<EventosNome>> GetEventoNome()
    {
        return await _context.TbEventos.Select(e => new EventosNome
        {
            EveCodigo = e.EveCodigo,
            EveNome = e.EveNome
        }).ToListAsync();
    }

    public async Task<TbEvento> GetEvento(int id)
    {
        return await _context.TbEventos.FirstOrDefaultAsync(e => e.EveCodigo == id);
    }
    public async Task<IEnumerable<BlocoNome>> GetPavilhoes()
    {
        return await _context.TbBlocos.Select(b => new BlocoNome
        {
            BloCodigo = b.BloCodigo,
            BloNome = b.BloNome
        }).ToListAsync();
    }

    public async Task<IEnumerable<QuartoPavilhao>> GetQuartosPavilhao(int codigoPavilhao)
    {
        return await _context.TbQuartos.Where(q => q.BloCodigo == codigoPavilhao)
        /*&& !q.TbEventoQuartos.Any())*/
          .Select(q => new QuartoPavilhao
          {
              QuaCodigo = q.QuaCodigo,
              QuaNome = q.QuaNome,
              QuaQtdcamas = q.QuaQtdcamas,
              QuaQtdcamasdisponiveis = _context.TbQuartos.Where(x => x.QuaCodigo == q.QuaCodigo).Select(x => x.QuaQtdcamas - x.TbQuartoPessoas.Count).FirstOrDefault(),
          }).ToListAsync();
    }

    public async Task<IEnumerable<QuartoPavilhao>> GetQuartosAlocados(int codigoPavilhao, int codigoEvento)
    {
        return await _context.TbEventoQuartos.Where(eq => eq.EveCodigo == codigoEvento)
            .Join(_context.TbQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Where(eq => eq.q.BloCodigo == codigoPavilhao)
            .Select(eq => new QuartoPavilhao
            {
                QuaCodigo = eq.q.QuaCodigo,
                QuaNome = eq.q.QuaNome,
                QuaQtdcamas = eq.q.QuaQtdcamas,
                QuaQtdcamasdisponiveis = _context.TbQuartos.Where(x => x.QuaCodigo == eq.q.QuaCodigo).Select(x => x.QuaQtdcamas - x.TbQuartoPessoas.Count).FirstOrDefault(),
                PessoasQuarto = _context.TbQuartoPessoas.Where(qp => qp.QuaCodigo == eq.q.QuaCodigo)
                .Join(_context.TbPessoas, p => p.PesCodigo, qp => qp.PesCodigo, (p, qp) => new { p, qp }).Select(x =>  new PessoasNome
                {
                    PesCodigo = x.qp.PesCodigo,
                    PesNome = x.qp.PesNome,
                    PesGenero = x.qp.PesGenero,
                }).ToList(),
            }).ToListAsync();
    }

    public async Task<IEnumerable<Hospedes>> GetPessoaEvento(int codigoComunidade)
    {
        return await _context.TbPessoas
            .Where(p => p.ComCodigo == codigoComunidade)
            .Select(x => new Hospedes
            {
            PesCodigo = x.PesCodigo,
            PesNome = x.PesNome,
            PesGenero = x.PesGenero,
            Comunidade = _context.TbComunidades.Where(c => c.ComCodigo == x.ComCodigo).Select(c => c.ComNome).FirstOrDefault(),
            Cobrante = _context.TbEventoPessoas.Where(ep => ep.PesCodigo == x.PesCodigo).Select(ep => ep.EvpCobrante).FirstOrDefault(),
            Pagante = _context.TbEventoPessoas.Where(ep => ep.PesCodigo == x.PesCodigo).Select(ep => ep.EvpPagante).FirstOrDefault()
        }).ToListAsync();
    }

    public async Task<IEnumerable<PessoaQuarto>> GetPessoasAlocadas(int codigoComunidade, int codigoEvento)
    {
        return await _context.TbEventoPessoas.Where(ep => ep.EveCodigo == codigoEvento)
            .Join(_context.TbPessoas, ep => ep.PesCodigo, p => p.PesCodigo, (ep, p) => new { ep, p })
            .Where(ep => ep.p.ComCodigo == codigoComunidade)
            .Select(ep => new PessoaQuarto
            {
                PesCodigo = ep.p.PesCodigo,
                PesNome = ep.p.PesNome
            }).ToListAsync();
    }

    public async Task<IEnumerable<PessoaQuarto>> GetPessoasQuarto(int codigoQuarto)
    {
        return await _context.TbPessoas.Where(p => p.TbQuartoPessoas.Any(qp => qp.QuaCodigo == codigoQuarto))
            .Select(ep => new PessoaQuarto
            {
                PesCodigo = ep.PesCodigo,
                PesNome = ep.PesNome
            }).ToListAsync();
    }

/*    public async Task<IEnumerable<QuartosNome>> GetQuartos(int codigoEvento, int codigoBloco)
    {
        return await _context.TbQuartos.Where(q => q.BloCodigo == codigoBloco)
            .Join(_context.TbEventoQuartos, q => q.QuaCodigo, eq => eq.QuaCodigo, (q, eq) => new { q, eq })
            .Where(x => x.eq.EveCodigo == codigoEvento)
            .Select(x => new QuartosNome
            {
                QuaCodigo = x.q.QuaCodigo,
                QuaNome = x.q.QuaNome
            }).GroupBy(q => q.QuaNome).Select(q => q.First()).ToListAsync();
    }*/

    public async Task<IEnumerable<ComunidadeNome>> GetComunidades()
    {
        return await _context.TbComunidades.Select(c => new ComunidadeNome
        {
            ComCodigo = c.ComCodigo,
            ComNome = c.ComNome
        }).ToListAsync();
    }

    public async Task<IEnumerable<Hospedes>> GetHospedes(int codigoEvento)
    {
        return await _context.TbPessoas
            .Join(_context.TbEventoPessoas, p => p.PesCodigo, ep => ep.PesCodigo, (p, ep) => new { p, ep })
            .Where(p => p.ep.EveCodigo == codigoEvento)
            .Select(x => new Hospedes
            {
                PesCodigo = x.p.PesCodigo,
                PesNome = x.p.PesNome,
                Comunidade = _context.TbComunidades.Where(c => c.ComCodigo == x.p.ComCodigo).Select(c => c.ComNome).FirstOrDefault(),
                Pagante = x.ep.EvpPagante,
                Cobrante = x.ep.EvpCobrante
            }).ToListAsync();
    }

    public async Task PostEvento(TbEvento evento)
    {
        if (evento.EveCodigo == 0)
        {
            var lastEvento = await _context.TbEventos.FirstOrDefaultAsync();
            if (lastEvento != null)
            {
                evento.EveCodigo = await _context.TbEventos.MaxAsync(e => e.EveCodigo) + 1;
            } else
            {
                evento.EveCodigo = 1;
            }
        } else
        {
            await UpdateEvento(evento);
        }
        _context.TbEventos.Add(evento);
        await _context.SaveChangesAsync();
    }


    public async Task PostQuartos(List<TbEventoQuarto> eventoQuarto, int codigo)
    {
        await _context.TbEventoQuartos.Where(eq => eq.QuaCodigoNavigation.BloCodigo == codigo).ExecuteDeleteAsync();
        foreach (var item in eventoQuarto)
        {
            if (item.EvqCodigo == 0)
            {
                var lastEventoQuarto = await _context.TbEventoQuartos.FirstOrDefaultAsync();
                if (lastEventoQuarto != null)
                {
                    item.EvqCodigo = await _context.TbEventoQuartos.MaxAsync(e => e.EvqCodigo) + 1;
                }
                else
                {
                    item.EvqCodigo = 1;
                }
            }
            else
            {
                await UpdateEventoQuarto(item);
            }
            _context.TbEventoQuartos.Add(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task PostPessoas(List<TbEventoPessoa> eventoPessoa, int codigo)
    {
        await _context.TbEventoPessoas.Where(ep => ep.PesCodigoNavigation.ComCodigo == codigo).ExecuteDeleteAsync();
        foreach (var item in eventoPessoa)
        {
            if (item.EvpCodigo == 0)
            {
                var lastEventoPessoa = await _context.TbEventoPessoas.FirstOrDefaultAsync();
                if (lastEventoPessoa != null)
                {
                    item.EvpCodigo = await _context.TbEventoPessoas.MaxAsync(e => e.EvpCodigo) + 1;
                }
                else
                {
                    item.EvpCodigo = 1;
                }
            }
            else
            {
                await UpdateEventoPessoa(item);
            }
            _context.TbEventoPessoas.Add(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateEvento(TbEvento evento)
    {
        _context.TbEventos.Update(evento);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEventoQuarto(TbEventoQuarto eventoQuarto)
    {
        _context.TbEventoQuartos.Update(eventoQuarto);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEventoPessoa(TbEventoPessoa eventoPessoa)
    {
        _context.TbEventoPessoas.Update(eventoPessoa);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEvento(TbEvento evento)
    {
        _context.TbEventos.Remove(evento);
        await _context.SaveChangesAsync();
    }
}
