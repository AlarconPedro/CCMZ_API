using CCMN_API.Models.Painel.Evento;
using CCMN_API.Models.Painel.EventoDespesas;
using CCMZ_API.Models.Painel.Alocacao;

namespace CCMN_API.Services.DespesasEvento;

public interface IDespesaEventoService
{
    Task<TbDespesaEvento> GetDespesasEvento(int codigoEvento);
    Task<EventoCusto> GetEventoCusto(int codigoEvento);
    Task<IEnumerable<ComunidadeNome>> GetComunidadesEvento(int codigoEvento);
    Task<PessoasPagantesCobrantes> GetCobrantesPagantes(int codigoEvento);
}
