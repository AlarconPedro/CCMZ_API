using CCMN_API.Models.Painel.Hospedagem.Dashboard;
using CCMN_API.Models.Painel.Hospedagem.QuartoPessoa;
using Microsoft.EntityFrameworkCore;

namespace CCMZ_API.Services.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly CCMNContext _context;

    public DashboardService(CCMNContext context)
    {
        _context = context;
    }

    public async Task<int> GetNumeroPessoasAChegar(int codigoEvento)
    {
        return await _context.TbQuartoPessoas.Where(x => x.PesCheckin == false && x.PesNaovem != true && x.PesCodigoNavigation.TbEventoPessoas.Any(eq => eq.EveCodigo == codigoEvento)).CountAsync();
            /*.Join(_context.TbEventoQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Join(_context.TbEventos, qp => qp.q.EveCodigo, e => e.EveCodigo, (qp, e) => new { qp, e })
            .Where(x => x.qp.eq.PesCheckin == false && x.qp.eq.PesNaovem != true && x.e.EveCodigo == codigoEvento).CountAsync();*/
/*            ).Where(x => x.PesCheckin == false).CountAsync();
*/    }

    public async Task<int> GetNumeroPessoasChegas(int codigoEvento)
    {
        return await _context.TbQuartoPessoas.Where(x => (x.PesCheckin == true || x.PesChave == true) && x.PesNaovem != true && x.PesCodigoNavigation.TbEventoPessoas.Any(eq => eq.EveCodigo == codigoEvento)).CountAsync();
           /* .Join(_context.TbEventoQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Join(_context.TbEventos, qp => qp.q.EveCodigo, e => e.EveCodigo, (qp, e) => new { qp, e })
            .Where(x => x.qp.eq.PesCheckin == true && x.e.EveCodigo == codigoEvento).CountAsync();*/
    }

    public async Task<int> GetNumeroPessoasNaoVem(int codigoEvento)
    {
        return await _context.TbQuartoPessoas.Where(x => x.PesNaovem == true && x.PesCodigoNavigation.TbEventoPessoas.Any(eq => eq.EveCodigo == codigoEvento)).CountAsync();
            /*.Join(_context.TbEventoQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Join(_context.TbEventos, qp => qp.q.EveCodigo, e => e.EveCodigo, (qp, e) => new { qp, e })
            .Where(x => x.qp.eq.PesNaovem == true && x.e.EveCodigo == codigoEvento).CountAsync();*/
    }

    public async Task<int> GetNumeroPessoasCobrantes(int codigoEvento)
    {
        return await _context.TbEventoPessoas.Where(x => x.EveCodigo == codigoEvento && x.EvpCobrante == true && x.PesCodigoNavigation.TbQuartoPessoas.Any(qp => qp.PesNaovem) == false).CountAsync();
    }

    public async Task<int> GetNumeroPessoasPagantes(int codigoEvento)
    {
        return await _context.TbEventoPessoas.Where(x => x.EveCodigo == codigoEvento && x.EvpPagante == true && x.PesCodigoNavigation.TbQuartoPessoas.Any(qp => qp.PesNaovem) == false).CountAsync();
    }

    public async Task<int> GetNumeroCamasLivres()
    {
        return await _context.TbQuartos
            .Join(_context.TbEventoQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Join(_context.TbEventos, qp => qp.q.EveCodigo, e => e.EveCodigo, (qp, e) => new { qp, e })
            .Where(x => x.qp.eq.TbQuartoPessoas.Count == 0 && DateTime.Now >= x.qp.q.EveCodigoNavigation.EveDatainicio && DateTime.Now <= x.qp.q.EveCodigoNavigation.EveDatafim).CountAsync();
    }

    public async Task<int> GetNumeroCamasOcupadas(int codigoEvento)
    {
        return await _context.TbQuartoPessoas
            .Where(qp => qp.QuaCodigoNavigation.TbEventoQuartos.Where(x => x.EveCodigo == codigoEvento).FirstOrDefault().EveCodigo == codigoEvento).CountAsync();
    }

    public async Task<IEnumerable<PessoasAChegar>> GetPessoasAChegar(int codigoEvento)
    {
        return await _context.TbPessoas
            .Join(_context.TbQuartoPessoas, p => p.PesCodigo, qp => qp.PesCodigo, (p, qp) => new { p, qp })
            .Join(_context.TbEventoPessoas, x => x.qp.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
            .Where(xx => xx.ep.EveCodigo.Equals(codigoEvento) && xx.x.qp.PesCheckin.Equals(false))
            .Select(y => new PessoasAChegar
            {
                ComNome = y.x.p.ComCodigoNavigation.ComNome,
                PesCodigo = y.x.p.PesCodigo,
                PesGenero = y.x.p.PesGenero,
                PesNome = y.x.p.PesNome,
                QuaCodigo = y.x.qp.QuaCodigo,
            }).ToListAsync();

        /*return await _context.TbQuartoPessoas
            .Join(_context.TbEventoQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Join(_context.TbEventos, qp => qp.q.EveCodigo, e => e.EveCodigo, (qp, e) => new { qp, e })
            .Where(x => x.qp.eq.PesCheckin == false && x.e.EveCodigo == codigoEvento)
            .Select(x => new PessoasAChegar
            {
                PesCodigo = x.qp.eq.PesCodigo,
                PesNome = x.qp.eq.PesCodigoNavigation.PesNome,
                PesGenero = x.qp.eq.PesCodigoNavigation.PesGenero,
                ComNome = x.qp.eq.PesCodigoNavigation.ComCodigoNavigation.ComNome,
                QuaCodigo = x.qp.eq.QuaCodigo
            }).ToListAsync();*/
    }

    public async Task<IEnumerable<PessoasAChegar>> GetPessoasChegas(int codigoEvento)
    {
        return await _context.TbQuartoPessoas
            .Join(_context.TbEventoQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Join(_context.TbEventos, qp => qp.q.EveCodigo, e => e.EveCodigo, (qp, e) => new { qp, e })
            .Where(x => x.qp.eq.PesCheckin == true && x.e.EveCodigo == codigoEvento
                )
            .Select(x => new PessoasAChegar
            {
                PesCodigo = x.qp.eq.PesCodigo,
                PesNome = x.qp.eq.PesCodigoNavigation.PesNome,
                PesGenero = x.qp.eq.PesCodigoNavigation.PesGenero,
                ComNome = x.qp.eq.PesCodigoNavigation.ComCodigoNavigation.ComNome,
                QuaCodigo = x.qp.eq.QuaCodigo
            }).ToListAsync();
    }

    public async Task<IEnumerable<PessoasAChegar>> GetPessoasNaoVem(int codigoEvento)
    {
        return await _context.TbQuartoPessoas.Where(qp => (qp.PesNaovem == true) && (qp.PesChave == false && qp.PesCheckin == false) && qp.QuaCodigoNavigation.TbEventoQuartos.Any(eq => eq.EveCodigo == codigoEvento))
            .Select(x => new PessoasAChegar
            {
                PesCodigo = x.PesCodigo,
                PesNome = x.PesCodigoNavigation.PesNome,
                PesGenero = x.PesCodigoNavigation.PesGenero,
                ComNome = x.PesCodigoNavigation.ComCodigoNavigation.ComNome,
                QuaCodigo = x.QuaCodigo,
            }).ToListAsync();
    }

    public async Task<IEnumerable<PessoasAChegar>> GetQuartosLivres(int codigoEvento)
    {
        return await _context.TbQuartoPessoas
            .Join(_context.TbEventoQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Join(_context.TbEventos, qp => qp.q.EveCodigo, e => e.EveCodigo, (qp, e) => new { qp, e })
            .Where(x => x.qp.eq.PesCheckin == false 
                && DateTime.Now >= x.qp.q.EveCodigoNavigation.EveDatainicio 
                && DateTime.Now <= x.qp.q.EveCodigoNavigation.EveDatafim
                && x.e.EveCodigo == codigoEvento)
            .Select(x => new PessoasAChegar
            {
                PesCodigo = x.qp.eq.PesCodigo,
                PesNome = x.qp.eq.PesCodigoNavigation.PesNome,
                PesGenero = x.qp.eq.PesCodigoNavigation.PesGenero,
                ComNome = x.qp.eq.PesCodigoNavigation.ComCodigoNavigation.ComNome,
                QuaCodigo = x.qp.eq.QuaCodigo
            }).ToListAsync();
    }

    public async Task<IEnumerable<PessoasAChegar>> GetQuartosOcupados(int codigoEvento)
    {
        return await _context.TbQuartoPessoas
            .Join(_context.TbEventoQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Join(_context.TbEventos, qp => qp.q.EveCodigo, e => e.EveCodigo, (qp, e) => new { qp, e })
            .Where(x => x.qp.eq.PesCheckin == true 
                && DateTime.Now >= x.qp.q.EveCodigoNavigation.EveDatainicio 
                && DateTime.Now <= x.qp.q.EveCodigoNavigation.EveDatafim
                && x.e.EveCodigo == codigoEvento)
            .Select(x => new PessoasAChegar
            {
                PesCodigo = x.qp.eq.PesCodigo,
                PesNome = x.qp.eq.PesCodigoNavigation.PesNome,
                PesGenero = x.qp.eq.PesCodigoNavigation.PesGenero,
                ComNome = x.qp.eq.PesCodigoNavigation.ComCodigoNavigation.ComNome,
                QuaCodigo = x.qp.eq.QuaCodigo
            }).ToListAsync();
    }

    public async Task<QuartoPessoas> GetQuartoPessoaAChegar(int codigoQuarto, int codigoEvento)
    {
        return await _context.TbEventoQuartos
           .Join(_context.TbQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
           .Where(eq => eq.q.QuaCodigo == codigoQuarto)
           .Select(eq => new QuartoPessoas
           {
               BloCodigo = eq.q.BloCodigo,
               QuaCodigo = eq.q.QuaCodigo,
               QuaNome = eq.q.QuaNome,
               Vagas = eq.q.QuaQtdcamas - _context.TbQuartoPessoas.Join(_context.TbPessoas, qp => qp.PesCodigo, p => p.PesCodigo, (qp, p) => new { qp, p })
                                                        .Join(_context.TbEventoPessoas, x => x.qp.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
                                                        .Where(x => x.x.qp.QuaCodigo == eq.q.QuaCodigo && x.ep.EveCodigo == codigoEvento).Count(),
               PessoasQuarto = _context.TbQuartoPessoas.Join(_context.TbPessoas, qp => qp.PesCodigo, p => p.PesCodigo, (qp, p) => new { qp, p })
                                                        .Join(_context.TbEventoPessoas, x => x.qp.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
                                                        .Where(x => x.x.qp.QuaCodigo == eq.q.QuaCodigo && x.ep.EveCodigo == codigoEvento)
                                                        .Select(x => new PessoaCheckin
                                                        {
                                                            PesCodigo = x.x.qp.PesCodigo,
                                                            PesChave = x.x.qp.PesChave,
                                                            QuaCodigo = x.x.qp.QuaCodigo,
                                                            PesCheckin = x.x.qp.PesCheckin,
                                                            PesNaovem = x.x.qp.PesNaovem,
                                                            QupCodigo = x.x.qp.QupCodigo,
                                                            PesNome = x.x.p.PesNome,
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
    
    public async Task<QuartoPessoas> GetQuartoPessoaChegas(int codigoQuarto, int codigoEvento)
    {
        return await _context.TbEventoQuartos
            .Join(_context.TbQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Where(eq => eq.q.QuaCodigo == codigoQuarto)
            .Select(eq => new QuartoPessoas
            {
                BloCodigo = eq.q.BloCodigo,
                QuaCodigo = eq.q.QuaCodigo,
                QuaNome = eq.q.QuaNome,
                Vagas = eq.q.QuaQtdcamas - _context.TbQuartoPessoas.Join(_context.TbPessoas, qp => qp.PesCodigo, p => p.PesCodigo, (qp, p) => new { qp, p })
                                                        .Join(_context.TbEventoPessoas, x => x.qp.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
                                                        .Where(x => x.x.qp.QuaCodigo == eq.q.QuaCodigo && x.ep.EveCodigo == codigoEvento).Count(),
                PessoasQuarto = _context.TbQuartoPessoas.Join(_context.TbPessoas, qp => qp.PesCodigo, p => p.PesCodigo, (qp, p) => new { qp, p })
                                                        .Join(_context.TbEventoPessoas, x => x.qp.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
                                                        .Where(x => x.x.qp.QuaCodigo == eq.q.QuaCodigo && x.ep.EveCodigo == codigoEvento)
                                                        .Select(x => new PessoaCheckin
                                                        {
                                                            PesCodigo = x.x.qp.PesCodigo,
                                                            PesChave = x.x.qp.PesChave,
                                                            QuaCodigo = x.x.qp.QuaCodigo,
                                                            PesCheckin = x.x.qp.PesCheckin,
                                                            PesNaovem = x.x.qp.PesNaovem,
                                                            QupCodigo = x.x.qp.QupCodigo,
                                                            PesNome = x.x.p.PesNome,
                                                        }).ToList()
            }).FirstOrDefaultAsync();
    }

    public async Task<QuartoPessoas> GetQuartoVagas(int codigoQuarto)
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

    public async Task<QuartoPessoas> GetQuartoOcupados(int codigoQuarto)
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
