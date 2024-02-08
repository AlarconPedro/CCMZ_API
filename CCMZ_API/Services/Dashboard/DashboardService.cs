using CCMZ_API.Models.Painel.Dashboard;
using CCMZ_API.Models.Painel.QuartoPessoa;
using Microsoft.EntityFrameworkCore;

namespace CCMZ_API.Services.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly CCMZContext _context;

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
             ComNome = x.PesCodigoNavigation.ComCodigoNavigation.ComNome
            }).ToListAsync();
    }

    public async Task<QuartoPessoas> GetQuartoPessoaAChegar(int codigoQuarto)
    {
       /* return await _context.TbQuartoPessoas
            .Where(x => x.QuaCodigo == codigoQuarto && x.PesCheckin == false)
            .Select(x => new QuartoPessoas
            {
                PesCodigo = x.PesCodigo,
                PesNome = x.PesCodigoNavigation.PesNome,
                ComNome = x.PesCodigoNavigation.ComCodigoNavigation.ComNome
            }).FirstOrDefaultAsync();*/
    }
}
