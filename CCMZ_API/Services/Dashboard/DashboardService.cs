using CCMZ_API.Models.Painel.Dashboard;
using CCMZ_API.Models.Painel.QuartoPessoa;
using Microsoft.EntityFrameworkCore;

namespace CCMZ_API.Services.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly CCMZContext _context;

    public DashboardService(CCMZContext context)
    {
        _context = context;
    }

    public async Task<int> GetNumeroPessoasAChegar()
    {
        return await _context.TbQuartoPessoas.Where(x => x.PesCheckin == false).CountAsync();
    }

    public async Task<IEnumerable<PessoasAChegar>> GetPessoasAChegar(int codigoEvento)
    {
        return await _context.TbQuartoPessoas
            .Where(x => x.PesCheckin == false)
            .Select(x => new PessoasAChegar
            {
             PesCodigo = x.PesCodigo,
             PesNome = x.PesCodigoNavigation.PesNome,
             PesGenero = x.PesCodigoNavigation.PesGenero,
             ComNome = x.PesCodigoNavigation.ComCodigoNavigation.ComNome,
             QuaCodigo = x.QuaCodigo
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
    public async Task<int> GetIdEventoAtivo()
    {
        return await _context.TbEventos.Where(x => x.EveDatafim >= DateTime.Now).Select(x => x.EveCodigo).FirstOrDefaultAsync();
    }
}
