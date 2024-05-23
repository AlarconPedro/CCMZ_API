using CCMN_API;
using CCMN_API.Models.Painel.Pessoas;
using CCMZ_API.Models;
using CCMZ_API.Models.Painel.Alocacao;
using CCMZ_API.Models.Painel.Pessoas;
using CCMZ_API.Models.Painel.Quartos;
using Microsoft.EntityFrameworkCore;

namespace CCMZ_API.Services.Eventos;

public class EventosService : IEventosService
{
    private readonly CCMNContext _context;

    public EventosService(CCMNContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TbEvento>> GetEventos(int mes)
    {
        if (mes != 0)
            return await _context.TbEventos.Where(e => e.EveDatafim.Value.Month == mes).ToListAsync();
        else
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

    public async Task<IEnumerable<EventosNome>> GetEventosAtivos()
    {
        return await _context.TbEventos.Where(e => e.EveDatafim.Value.AddDays(1) >= DateTime.Now)
            .Select(e => new EventosNome
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

    public async Task<IEnumerable<QuartoPavilhao>> GetQuartosPavilhao(int codigoPavilhao, int codigoEvento)
    {
        var evento = await _context.TbEventos.Where(e => e.EveCodigo == codigoEvento).FirstOrDefaultAsync();
        var datanicio = evento.EveDatainicio;
        var datafim = evento.EveDatafim;

        return await _context.TbQuartos.Where(q => q.BloCodigo == codigoPavilhao && q.TbEventoQuartos.All(eq => eq.EveCodigo == codigoEvento 
                   ? true 
                              : (eq.EveCodigoNavigation.EveDatafim < datanicio || eq.EveCodigoNavigation.EveDatainicio > datafim)))
            .Select(q => new QuartoPavilhao
            {
                QuaCodigo = q.QuaCodigo,
                QuaNome = q.QuaNome,
                QuaQtdcamas = q.QuaQtdcamas,
            }).ToListAsync();
    }

    public async Task<IEnumerable<QuartoPavilhao>> GetQuartosAlocados(int codigoPavilhao, int codigoEvento)
    {
    /*    var evento = await _context.TbEventos.Where(e => e.EveCodigo == codigoEvento).FirstOrDefaultAsync();
        var datanicio = evento.EveDatainicio;
        var datafim = evento.EveDatafim;*/

        return await _context.TbQuartos.Where(q => q.BloCodigo == codigoPavilhao && q.TbEventoQuartos.Where(eq => eq.EveCodigo == codigoEvento).FirstOrDefault() != null)
            .Select(q => new QuartoPavilhao
            {
                QuaCodigo = q.QuaCodigo,
                QuaNome = q.QuaNome,
                QuaQtdcamas = q.QuaQtdcamas,  
                QuaQtdcamasdisponiveis = q.QuaQtdcamas - _context.TbPessoas.Where(p => p.TbQuartoPessoas
                                                           .Where(qp => qp.QuaCodigo == q.QuaCodigo && p.TbEventoPessoas
                                                           .Where(eq => eq.EveCodigo == codigoEvento).FirstOrDefault() != null)
                                                           .FirstOrDefault() != null && p.TbEventoPessoas.Where(eq => eq.EveCodigo == codigoEvento).FirstOrDefault() != null).Count(),
                PessoasQuarto = _context.TbPessoas.Where(p => p.TbQuartoPessoas
                                                  .Where(qp => qp.QuaCodigo == q.QuaCodigo && p.TbEventoPessoas
                                                           .Where(eq => eq.EveCodigo == codigoEvento).FirstOrDefault() != null)
                                                           .FirstOrDefault() != null && p.TbEventoPessoas.Where(eq => eq.EveCodigo == codigoEvento).FirstOrDefault() != null).Select(p => new PessoasNome
                                                           {
                                                                PesCodigo = p.PesCodigo,
                                                                PesNome = p.PesNome,
                                                                PesGenero = p.PesGenero
                                                           }).ToList()
            }).ToListAsync();
    }

    public async Task<IEnumerable<PessoaEvento>> GetPessoaEvento(int codigoComunidade, int codigoEvento)
    {
        return await _context.TbPessoas
            .Where(p => p.ComCodigo == codigoComunidade)
            .Select(x => new PessoaEvento
            {
                EvpPagante = _context.TbEventoPessoas.Where(ep => ep.PesCodigo == x.PesCodigo && ep.EveCodigo == codigoEvento).Select(ep => ep.EvpPagante).FirstOrDefault(),
                Comunidade = _context.TbComunidades.Where(c => c.ComCodigo == x.ComCodigo).Select(c => c.ComNome).FirstOrDefault(),
                EvpCobrante = _context.TbEventoPessoas.Where(ep => ep.PesCodigo == x.PesCodigo && ep.EveCodigo == codigoEvento).Select(ep => ep.EvpCobrante).FirstOrDefault(),
                EvpCodigo = _context.TbEventoPessoas.Where(ep => ep.PesCodigo == x.PesCodigo && ep.EveCodigo == codigoEvento).Select(ep => ep.EvpCodigo).FirstOrDefault(),
                PesCodigo = x.PesCodigo,
                PesGenero = x.PesGenero,
                PesNome = x.PesNome
           /* PesCodigo = x.PesCodigo,
            PesNome = x.PesNome,
            PesGenero = x.PesGenero,
            Comunidade = _context.TbComunidades.Where(c => c.ComCodigo == x.ComCodigo).Select(c => c.ComNome).FirstOrDefault(),
            Cobrante = _context.TbEventoPessoas.Where(ep => ep.PesCodigo == x.PesCodigo).Select(ep => ep.EvpCobrante).FirstOrDefault(),
            Pagante = _context.TbEventoPessoas.Where(ep => ep.PesCodigo == x.PesCodigo).Select(ep => ep.EvpPagante).FirstOrDefault()*/
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

    public async Task<IEnumerable<PessoaQuarto>> GetPessoasQuarto(int codigoQuarto, int codigoEvento)
    {
        return await _context.TbPessoas.Where(p => p.TbQuartoPessoas.Where(qp => qp.QuaCodigo == codigoQuarto).FirstOrDefault() != null
                && p.TbEventoPessoas.Where(ep => ep.EveCodigo == codigoEvento).FirstOrDefault() != null)
            .Select(ep => new PessoaQuarto
            {
                PesCodigo = ep.PesCodigo,
                PesNome = ep.PesNome
            }).ToListAsync();
    }

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
        //await _context.TbEventoQuartos.Where(eq => eq.QuaCodigoNavigation.QuaCodigo == codigo).ExecuteDeleteAsync();
        foreach (var item in eventoQuarto)
        {
            await _context.TbEventoQuartos.Where(eq => eq.QuaCodigoNavigation.QuaCodigo == item.QuaCodigo && eq.EveCodigo == item.EveCodigo).ExecuteDeleteAsync();
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

    public async Task PostPessoas(List<TbEventoPessoa> eventoPessoa)
    {
        //await _context.TbEventoPessoas.Where(ep => ep.PesCodigoNavigation.ComCodigo == codigo).ExecuteDeleteAsync();
        foreach (var item in eventoPessoa)
        {
            /*if (item.EvpCodigo != 0)
            {
                await _context.TbEventoPessoas.Where(ep => ep.EvpCodigo == item.EvpCodigo).ExecuteDeleteAsync();
            }
            else */
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
                _context.TbEventoPessoas.Add(item);
            }
            else
            {
                await UpdateEventoPessoa(item);
            }
            
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

    public async Task DeleteEventoPessoas(List<TbEventoPessoa> evento)
    {
        _context.TbEventoPessoas.RemoveRange(evento);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveQuartoEvento(int codigoQuarto)
    {
        await _context.TbEventoQuartos.Where(eq => eq.QuaCodigo == codigoQuarto).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEvento(TbEvento evento)
    {
        _context.TbEventos.Remove(evento);
        await _context.SaveChangesAsync();
    }
/*
    public Task DeletePessoasEvento(TbEvento evento)
    {
        throw new NotImplementedException();
    }*/
}
