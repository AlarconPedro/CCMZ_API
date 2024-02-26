using CCMZ_API.Models.Painel.Dashboard;
using CCMZ_API.Models.Painel.QuartoPessoa;

namespace CCMZ_API.Services.Dashboard;

public interface IDashboardService
{
    Task<int> GetNumeroPessoasAChegar();
    Task<int> GetNumeroPessoasChegas();
    Task<IEnumerable<PessoasAChegar>> GetPessoasAChegar(int codigoEvento);
    Task<IEnumerable<PessoasAChegar>> GetPessoasChegas(int codigoEvento);
    Task<QuartoPessoas> GetQuartoPessoaAChegar(int codigoQuarto);
    Task<QuartoPessoas> GetQuartoPessoaChegas(int codigoQuarto);
    Task<int> GetIdEventoAtivo();
}
