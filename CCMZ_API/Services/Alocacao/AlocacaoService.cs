using CCMZ_API.Models.Painel.Alocacao;
using Microsoft.EntityFrameworkCore;

namespace CCMZ_API.Services.Alocacao;

public class AlocacaoService : IAlocacaoService
{
    private readonly CCMZContext _context;

    public AlocacaoService(CCMZContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EventosNome>> GetEventos()
    {
        return await _context.TbEventos.Select(e => new EventosNome
        {
            EveCodigo = e.EveCodigo,
            EveNome = e.EveNome
        }).ToListAsync();
    }

    public async Task<IEnumerable<BlocoNome>> GetBlocos(int codigoEvento)
    {
        return await _context.TbBlocos
            .Join(_context.TbQuartos, b => b.BloCodigo, q => q.BloCodigo, (b, q) => new {b, q})
            .Join(_context.TbEventoQuartos, x => x.q.QuaCodigo, eq => eq.QuaCodigo, (x, eq) => new {x, eq})
            .Join(_context.TbEventos, x => x.eq.EveCodigo, e => e.EveCodigo, (x, e) => new {x, e})
            .Where(x => x.e.EveCodigo == codigoEvento)
            .Select(x => new BlocoNome
            {
                BloCodigo = x.x.x.b.BloCodigo,
                BloNome = x.x.x.b.BloNome
            }).Distinct().ToListAsync();
    }

    public async Task<IEnumerable<ComunidadeNome>> GetComunidades(int codigoEvento)
    {
        return await _context.TbComunidades.Where(c => c.TbPessoas.Any())
            .Join(_context.TbPessoas, c => c.ComCodigo, p => p.ComCodigo, (c, p) => new { c, p })
            .Join(_context.TbEventoPessoas, x => x.p.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
            .Join(_context.TbEventos, z => z.ep.EveCodigo, e => e.EveCodigo, (z, e) => new { z, e })
            .Where(y => y.e.EveCodigo == codigoEvento && y.z.x.p.PesCodigo == y.z.ep.PesCodigo)
            .Select(c => new ComunidadeNome
            {
                ComCodigo = c.z.x.c.ComCodigo,
                ComNome = c.z.x.c.ComNome,
                ComCidade = c.z.x.c.ComCidade
            }).ToListAsync();
        /*return await _context.TbComunidades
            .Join(_context.TbPessoas, c => c.ComCodigo, p => p.ComCodigo, (c, p) => new {c, p})
            .Join(_context.TbEventoPessoas, x => x.p.PesCodigo, ep => ep.PesCodigo, (x, ep) => new {x, ep})
            .Join(_context.TbEventos, z => z.ep.EveCodigo, e => e.EveCodigo, (z, e) => new {z, e})
            .Where(y => y.e.EveCodigo == codigoEvento && y.z.x.p.PesCodigo == y.z.ep.PesCodigo)
            .Select(y => new ComunidadeNome
            {
                ComCodigo = y.z.x.c.ComCodigo,
                ComNome = y.z.x.c.ComNome,
                ComCidade = y.z.x.c.ComCidade
            }).Distinct().ToListAsync();*/
    }

    public async Task<IEnumerable<QuartosNome>> GetQuartos(int codigoEvento)
    {
        return await _context.TbQuartos
            .Join(_context.TbEventoQuartos, q => q.QuaCodigo, eq => eq.QuaCodigo, (q, eq) => new {q, eq})
            .Join(_context.TbEventos, x => x.eq.EveCodigo, e => e.EveCodigo, (x, e) => new {x, e})
            .Where(x => x.e.EveCodigo == codigoEvento)
            .Select(x => new QuartosNome
            {
                QuaCodigo = x.x.q.QuaCodigo,
                QuaNome = x.x.q.QuaNome
            }).Distinct().ToListAsync();
    }
}
