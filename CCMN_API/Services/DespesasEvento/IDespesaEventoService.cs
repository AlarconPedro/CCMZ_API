using CCMN_API.Models.Painel.Evento;
using CCMN_API.Models.Painel.EventoDespesas;

namespace CCMN_API.Services.DespesasEvento;

public interface IDespesaEventoService
{
    Task<TbDespesaEvento> GetDespesasEvento(int codigoEvento);

    Task<EventoCusto> GetEventoCusto(int codigoEvento);

    Task<PessoasPagantesCobrantes> GetCobrantesPagantes(int codigoEvento);
}
