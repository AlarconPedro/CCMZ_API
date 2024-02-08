using CCMZ_API.Models.Painel.Dashboard;
using CCMZ_API.Models.Painel.QuartoPessoa;

namespace CCMZ_API.Services.Dashboard;

public interface IDashboardService
{
    Task<int> GetNumeroPessoasAChegar();
    Task<IEnumerable<PessoasAChegar>> GetPessoasAChegar(int codigoEvento);
    Task<QuartoPessoas> GetQuartoPessoaAChegar(int codigoQuarto);
}
