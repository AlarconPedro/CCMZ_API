using CCMZ_API.Models.Painel.Alocacao;
using CCMZ_API.Models.Painel.Pessoas;
using Microsoft.EntityFrameworkCore;

namespace CCMZ_API.Services.Alocacao;

using CCMZ_API.Models.Painel.Comunidade;

public class AlocacaoService : IAlocacaoService
{
    private readonly CCMNContext _context;

    public AlocacaoService(CCMNContext context)
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
        return await _context.TbBlocos.Where(b => b.TbQuartos.Any())
            .Join(_context.TbQuartos, b => b.BloCodigo, q => q.BloCodigo, (b, q) => new {b, q})
            .Join(_context.TbEventoQuartos, x => x.q.QuaCodigo, eq => eq.QuaCodigo, (x, eq) => new {x, eq})
            .Join(_context.TbEventos, x => x.eq.EveCodigo, e => e.EveCodigo, (x, e) => new {x, e})
            .Where(x => x.e.EveCodigo == codigoEvento && x.x.x.q.QuaCodigo == x.x.eq.QuaCodigo)
            .Select(x => new BlocoNome
            {
                BloCodigo = x.x.x.b.BloCodigo,
                BloNome = x.x.x.b.BloNome
            }).Distinct().ToListAsync();
    }

    public async Task<IEnumerable<Comunidade>> GetComunidades(int codigoEvento)
    {
        return await _context.TbComunidades.Where(c => c.TbPessoas.Any())
            .Join(_context.TbPessoas, c => c.ComCodigo, p => p.ComCodigo, (c, p) => new { c, p })
            .Join(_context.TbEventoPessoas, x => x.p.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
            .Where(y => y.ep.EveCodigo == codigoEvento && y.x.p.PesCodigo == y.ep.PesCodigo)
            .Select(c => new Comunidade
            {
                ComCodigo = c.x.c.ComCodigo,
                ComNome = c.x.c.ComNome,
                ComCidade = c.x.c.ComCidade,
                ComUf = c.x.c.ComUf
            }).GroupBy(c => c.ComCodigo).Select(c => c.First()).ToListAsync();
    }

    public async Task<IEnumerable<PessoasNome>> GetPessoasComunidade(int codigoEvento, int codigoComunidade)
    {
        //var listaPessoas = await _context.TbQuartoPessoas.ToListAsync();
        return await _context.TbPessoas
            .Where(p => p.ComCodigo == codigoComunidade && !p.TbQuartoPessoas.Any(qp => qp.PesCodigo == p.PesCodigo))
            .Join(_context.TbEventoPessoas, p => p.PesCodigo, ep => ep.PesCodigo, (p, ep) => new {p, ep})
            .Where(y => y.ep.EveCodigo == codigoEvento)
            .Select(x => new PessoasNome
            {
                PesCodigo = x.p.PesCodigo,
                PesNome = x.p.PesNome,
                PesGenero = x.p.PesGenero,
            }).ToListAsync();
    }

    public async Task<IEnumerable<PessoasAlocadas>> GetPessoasQuarto(int codigoQuarto)
    {
        return await _context.TbPessoas.Where(p => p.TbQuartoPessoas.Any(q => q.QuaCodigo == codigoQuarto))
            .Select(x => new PessoasAlocadas
            {
                PesCodigo = x.PesCodigo,
                PesNome = x.PesNome,
                QtdCamas = x.TbQuartoPessoas.Count(q => q.QuaCodigo == codigoQuarto)
            }).ToListAsync();
    }

    public async Task<IEnumerable<PessoasNome>> GetPessoasTotal(int codigoEvento, int codigoComunidade)
    {
        return await _context.TbPessoas.Where(p => p.ComCodigo == codigoComunidade)
            .Join(_context.TbEventoPessoas, p => p.PesCodigo, ep => ep.PesCodigo, (p, ep) => new {p, ep})
            .Where(y => y.ep.EveCodigo == codigoEvento)
            .Select(x => new PessoasNome
            {
                PesCodigo = x.p.PesCodigo,
                PesNome = x.p.PesNome
            }).ToListAsync();
    }

    public async Task<TbQuartoPessoa> GetPessoaAlocada(int codigoPessoa)
    {
        return await _context.TbQuartoPessoas.FirstOrDefaultAsync(qp => qp.PesCodigo == codigoPessoa);
    }

    public async Task AddPessoaQuarto(TbQuartoPessoa quartoPessoa)
    {
        if (quartoPessoa.QupCodigo == 0)
        {
            //var lastQuartoPessoa = await _context.TbQuartoPessoas.OrderByDescending(qp => qp.QupCodigo).FirstOrDefaultAsync();
            var lastQuartoPessoa = await _context.TbQuartoPessoas.FirstOrDefaultAsync();
            if (lastQuartoPessoa != null)
            {
                quartoPessoa.QupCodigo = await _context.TbQuartoPessoas.MaxAsync(qp => qp.QupCodigo) + 1;
            } else
            {
                quartoPessoa.QupCodigo = 1;
            }
             _context.TbQuartoPessoas.Add(quartoPessoa);
            await _context.SaveChangesAsync();
        } else
        {
            await AtualizarPessoaQuarto(quartoPessoa);
        }
        /*var quartoPessoa = new TbQuartoPessoa
        {
            QupCodigo = 0,
            PesCodigo = codigoPessoa,
            QuaCodigo = codigoQuarto
        };
        await _context.TbQuartoPessoas.AddAsync(quartoPessoa);
        await _context.SaveChangesAsync();*/
    }

    public async Task AtualizarPessoaQuarto(TbQuartoPessoa quartoPessoa)
    {
        _context.TbQuartoPessoas.Update(quartoPessoa);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverPessoaQuarto(TbQuartoPessoa quartoPessoa)
    {
        _context.TbQuartoPessoas.Remove(quartoPessoa);
        await _context.SaveChangesAsync();
    }

    public async Task LimpaPessoasAlocadas(int codigoQuarto)
    {
        var pessoas = await _context.TbQuartoPessoas.Where(qp => qp.QuaCodigo == codigoQuarto).ToListAsync();
        _context.TbQuartoPessoas.RemoveRange(pessoas);
        await _context.SaveChangesAsync();
    }
}
