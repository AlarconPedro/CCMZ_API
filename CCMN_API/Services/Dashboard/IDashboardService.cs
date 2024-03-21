using CCMZ_API.Models.Painel.Dashboard;
using CCMZ_API.Models.Painel.QuartoPessoa;

namespace CCMZ_API.Services.Dashboard;

using CCMZ_API.Models.Painel.Quartos;

public interface IDashboardService
{
    Task<int> GetNumeroPessoasAChegar();
    Task<int> GetNumeroPessoasChegas();
    Task<int> GetNumeroCamasLivres();
    Task<int> GetNumeroCamasOcupadas();
    Task<IEnumerable<PessoasAChegar>> GetPessoasAChegar(int codigoEvento);
    Task<IEnumerable<PessoasAChegar>> GetPessoasChegas(int codigoEvento);
    Task<IEnumerable<PessoasAChegar>> GetQuartosLivres(int codigoEvento);
    Task<IEnumerable<PessoasAChegar>> GetQuartosOcupados(int codigoEvento);
    Task<QuartoPessoas> GetQuartoPessoaAChegar(int codigoQuarto);
    Task<QuartoPessoas> GetQuartoPessoaChegas(int codigoQuarto);
    Task<QuartoPessoas> GetQuartoVagas(int codigoQuarto);
    Task<QuartoPessoas> GetQuartoOcupados(int codigoQuarto);
    Task<int> GetIdEventoAtivo();
}
