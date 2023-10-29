using CCMZ_API.Models;
using CCMZ_API.Models.Painel.Bloco;
using CCMZ_API.Models.Painel.Comunidade;
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
        return await _context.TbQuartos.Where(q => q.BloCodigo == codigoPavilhao).Select(q => new QuartoPavilhao
        {
            QuaCodigo = q.QuaCodigo,
            QuaNome = q.QuaNome
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
                QuaNome = eq.q.QuaNome
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


    public async Task PostQuartos(List<TbEventoQuarto> eventoQuarto)
    {
        foreach (var item in eventoQuarto)
        {
            if (item.EvqCodigo == 0)
            {
                var lastEventoQuarto = await _context.TbEventoQuartos.FirstOrDefaultAsync();
                if (lastEventoQuarto != null)
                {
                    item.EvqCodigo = await _context.TbEventoQuartos.MaxAsync(e => e.EvqCodigo) + 1;
                } else
                {
                    item.EvqCodigo = 1;
                }
            } else
            {
                await UpdateEventoQuarto(item);
            }
            _context.TbEventoQuartos.Add(item);
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

    public async Task DeleteEvento(TbEvento evento)
    {
        _context.TbEventos.Remove(evento);
        await _context.SaveChangesAsync();
    }
}
