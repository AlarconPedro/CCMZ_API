using CCMZ_API.Models.Painel.QuartoPessoa;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CCMZ_API.Services.QuartoPessoa;

public class QuartoPessoaService : IQuartoPessoaService
{
    private readonly CCMZContext _context;

    public QuartoPessoaService(CCMZContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuartoPessoas>> GetQuartoPessoas(int codigoBloco, int codigoEvento)
    {
        return await _context.TbEventoQuartos.Where(eq => eq.EveCodigo == codigoEvento)
            .Join(_context.TbQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Where(eq => eq.q.BloCodigo == codigoBloco)
            .Select(eq => new QuartoPessoas
            {
                BloCodigo = eq.q.BloCodigo,
                QuaCodigo = eq.q.QuaCodigo,
                QuaNome = eq.q.QuaNome,
                Vagas = _context.TbQuartos.Where(x => x.QuaCodigo == eq.q.QuaCodigo).Select(x => x.QuaQtdcamas - x.TbQuartoPessoas.Count).FirstOrDefault(),
                PessoasQuarto = _context.TbQuartoPessoas
                .Where(qp => qp.QuaCodigo == eq.q.QuaCodigo)
                .Select(x => new PessoaCheckin
                {
                    PesCodigo = x.PesCodigo,
                    PesChave = x.PesChave,
                    QuaCodigo = x.QuaCodigo,
                    PesCheckin = x.PesCheckin,
                    QupCodigo = x.QupCodigo,
                    PesNome = x.PesCodigoNavigation.PesNome,
                }).ToList()
            }).ToListAsync();

        /*return await _context.TbQuartoPessoas.Where(qp => qp.QuaCodigoNavigation.BloCodigo == codigoBloco)
            .Join(_context.TbEventoQuartos, qp => qp.QuaCodigo, eq => eq.QuaCodigo, (qp, eq) => new { qp, eq })
            .Where(x => x.eq.QuaCodigo == x.qp.QuaCodigo && x.eq.EveCodigo == codigoEvento && x.eq.QuaCodigoNavigation.BloCodigo == codigoBloco && x.qp.PesCodigo != 0)
            .Select(x => new QuartoPessoas
            {
                BloCodigo = x.qp.QuaCodigoNavigation.BloCodigo,
                QuaCodigo = x.eq.QuaCodigo,
                QuaNome = x.qp.QuaCodigoNavigation.QuaNome,
                PessoasQuarto = _context.TbQuartoPessoas
                .Select(x => new PessoaCheckin
                {
                    PesCodigo = x.PesCodigo,
                    PesChave = x.PesChave,
                    PesCheckin = x.PesCheckin,
                    QupCodigo = x.QupCodigo,
                    PesNome = x.PesCodigoNavigation.PesNome,
                }).ToList()
            }).ToListAsync();*/
    }

    public async Task<IEnumerable<QuartoPessoas>> GetQuartoPessoasBusca(int codigoEvento, string busca)
    {
        return await _context.TbEventoQuartos.Where(eq => eq.EveCodigo == codigoEvento)
            .Join(_context.TbQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Where(eq =>  eq.q.TbQuartoPessoas.Any(x => x.PesCodigoNavigation.PesNome.Contains(busca)))
            .Select(eq => new QuartoPessoas
            {
                BloCodigo = eq.q.BloCodigo,
                QuaCodigo = eq.q.QuaCodigo,
                QuaNome = eq.q.QuaNome,
                Vagas = _context.TbQuartos.Where(x => x.QuaCodigo == eq.q.QuaCodigo).Select(x => x.QuaQtdcamas - x.TbQuartoPessoas.Count).FirstOrDefault(),
                PessoasQuarto = _context.TbQuartoPessoas
                .Where(qp => qp.QuaCodigo == eq.q.QuaCodigo)
                .Select(x => new PessoaCheckin
                {
                    PesCodigo = x.PesCodigo,
                    PesChave = x.PesChave,
                    QuaCodigo = x.QuaCodigo,
                    PesCheckin = x.PesCheckin,
                    QupCodigo = x.QupCodigo,
                    PesNome = x.PesCodigoNavigation.PesNome,
                }).ToList()
            }).ToListAsync();
    }

    public async Task UpdateQuartoPessoa(TbQuartoPessoa quartoPessoa)
    {
        if (quartoPessoa.PesChave)
        {
            _context.TbQuartoPessoas.Update(quartoPessoa);
            await _context.SaveChangesAsync();
        } else
        {
            var statusQuarto = await _context.TbQuartoPessoas.Where(qp => qp.QuaCodigo == quartoPessoa.QuaCodigo).Select(x => x.PesChave).ToListAsync();
            if (!statusQuarto.Contains(true))
            {
                quartoPessoa.PesChave = true;
                _context.TbQuartoPessoas.Update(quartoPessoa);
                await _context.SaveChangesAsync();
            } else
            {
                _context.TbQuartoPessoas.Update(quartoPessoa);
                await _context.SaveChangesAsync();
            }

        }
        // var retorno = await _context.TbQuartoPessoas.Where(x => x.PesCodigo == quartoPessoa.PesCodigo && x.QuaCodigo == quartoPessoa.QuaCodigo).FirstOrDefaultAsync();
        /*_context.Entry(quartoPessoa).State = EntityState.Modified;*/
       
    }
}
