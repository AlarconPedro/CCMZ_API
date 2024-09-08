namespace CCMZ_API.Services.Dashboard;

using CCMN_API.Models.Painel.Hospedagem.Dashboard;
using CCMN_API.Models.Painel.Hospedagem.QuartoPessoa;

public interface IDashboardService
{
    Task<int> GetNumeroPessoasAChegar(int codigoEvento);
    Task<int> GetNumeroPessoasChegas(int codigoEvento);
    Task<int> GetNumeroPessoasNaoVem(int codigoEvento);
    Task<int> GetNumeroPessoasCobrantes(int codigoEvento);
    Task<int> GetNumeroPessoasPagantes(int codigoEvento);
    Task<int> GetNumeroCamasLivres();
    Task<int> GetNumeroCamasOcupadas(int codigoEvento);
    Task<IEnumerable<PessoasAChegar>> GetPessoasAChegar(int codigoEvento);
    Task<IEnumerable<PessoasAChegar>> GetPessoasChegas(int codigoEvento);
    Task<IEnumerable<PessoasAChegar>> GetPessoasNaoVem(int codigoEvento);
    Task<IEnumerable<PessoasAChegar>> GetQuartosLivres(int codigoEvento);
    Task<IEnumerable<PessoasAChegar>> GetQuartosOcupados(int codigoEvento);
    Task<QuartoPessoas> GetQuartoPessoaAChegar(int codigoQuarto, int codigoEvento);
    Task<QuartoPessoas> GetQuartoPessoaChegas(int codigoQuarto, int codigoEvento);
    //Task<QuartoPessoas> GetQuartoPessoaNaoVem(int codigoQuarto, int codigoEvento);
    Task<QuartoPessoas> GetQuartoVagas(int codigoQuarto);
    Task<QuartoPessoas> GetQuartoOcupados(int codigoQuarto);
    Task<int> GetIdEventoAtivo();
}
