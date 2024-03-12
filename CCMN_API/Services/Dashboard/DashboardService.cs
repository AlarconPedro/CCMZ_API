using CCMZ_API.Models.Painel.Dashboard;
using CCMZ_API.Models.Painel.QuartoPessoa;
using Microsoft.EntityFrameworkCore;

namespace CCMZ_API.Services.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly CCMNContext _context;

    public DashboardService(CCMNContext context)
    {
        _context = context;
    }

    public async Task<int> GetNumeroPessoasAChegar()
    {
        return await _context.TbQuartoPessoas
            .Join(_context.TbEventoQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Join(_context.TbEventos, qp => qp.q.EveCodigo, e => e.EveCodigo, (qp, e) => new { qp, e })
            .Where(x => x.qp.eq.PesCheckin == false && DateTime.Now >= x.qp.q.EveCodigoNavigation.EveDatainicio && DateTime.Now <= x.qp.q.EveCodigoNavigation.EveDatafim).CountAsync();
/*            ).Where(x => x.PesCheckin == false).CountAsync();
*/    }

    public async Task<int> GetNumeroPessoasChegas()
    {
        return await _context.TbQuartoPessoas
            .Join(_context.TbEventoQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Join(_context.TbEventos, qp => qp.q.EveCodigo, e => e.EveCodigo, (qp, e) => new { qp, e })
            .Where(x => x.qp.eq.PesCheckin == true && DateTime.Now >= x.qp.q.EveCodigoNavigation.EveDatainicio && DateTime.Now <= x.qp.q.EveCodigoNavigation.EveDatafim).CountAsync();
    }

    public async Task<IEnumerable<PessoasAChegar>> GetPessoasAChegar(int codigoEvento)
    {
        return await _context.TbQuartoPessoas
            .Join(_context.TbEventoQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Join(_context.TbEventos, qp => qp.q.EveCodigo, e => e.EveCodigo, (qp, e) => new { qp, e })
            .Where(x => x.qp.eq.PesCheckin == false && DateTime.Now >= x.qp.q.EveCodigoNavigation.EveDatainicio && DateTime.Now <= x.qp.q.EveCodigoNavigation.EveDatafim)
            .Select(x => new PessoasAChegar
            {
             PesCodigo = x.qp.eq.PesCodigo,
             PesNome = x.qp.eq.PesCodigoNavigation.PesNome,
             PesGenero = x.qp.eq.PesCodigoNavigation.PesGenero,
             ComNome = x.qp.eq.PesCodigoNavigation.ComCodigoNavigation.ComNome,
             QuaCodigo = x.qp.eq.QuaCodigo
            }).ToListAsync();
    }

    public async Task<IEnumerable<PessoasAChegar>> GetPessoasChegas(int codigoEvento)
    {
        return await _context.TbQuartoPessoas
            .Join(_context.TbEventoQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Join(_context.TbEventos, qp => qp.q.EveCodigo, e => e.EveCodigo, (qp, e) => new { qp, e })
            .Where(x => x.qp.eq.PesCheckin == true && DateTime.Now >= x.qp.q.EveCodigoNavigation.EveDatainicio && DateTime.Now <= x.qp.q.EveCodigoNavigation.EveDatafim)
            .Select(x => new PessoasAChegar
            {
                PesCodigo = x.qp.eq.PesCodigo,
                PesNome = x.qp.eq.PesCodigoNavigation.PesNome,
                PesGenero = x.qp.eq.PesCodigoNavigation.PesGenero,
                ComNome = x.qp.eq.PesCodigoNavigation.ComCodigoNavigation.ComNome,
                QuaCodigo = x.qp.eq.QuaCodigo
            }).ToListAsync();
    }

    public async Task<QuartoPessoas> GetQuartoPessoaAChegar(int codigoQuarto)
    {
        return await _context.TbEventoQuartos
           .Join(_context.TbQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
           .Where(eq => eq.q.QuaCodigo == codigoQuarto)
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
           }).FirstOrDefaultAsync();
        /* return await _context.TbQuartoPessoas
             .Where(x => x.QuaCodigo == codigoQuarto && x.PesCheckin == false)
             .Select(x => new QuartoPessoas
             {
                 PesCodigo = x.PesCodigo,
                 PesNome = x.PesCodigoNavigation.PesNome,
                 ComNome = x.PesCodigoNavigation.ComCodigoNavigation.ComNome
             }).FirstOrDefaultAsync();*/
    }
    
    public async Task<QuartoPessoas> GetQuartoPessoaChegas(int codigoQuarto)
    {
        return await _context.TbEventoQuartos
            .Join(_context.TbQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Where(eq => eq.q.QuaCodigo == codigoQuarto)
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
            }).FirstOrDefaultAsync();
    }

    public async Task<int> GetIdEventoAtivo()
    {
        return await _context.TbEventos.Where(x => DateTime.Now >= x.EveDatainicio && DateTime.Now <= x.EveDatafim).Select(x => x.EveCodigo).FirstOrDefaultAsync();
    }
}
