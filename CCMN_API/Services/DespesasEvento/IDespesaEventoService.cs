using CCMN_API.Models.Painel.Acerto;
using CCMN_API.Models.Painel.Evento;
using CCMN_API.Models.Painel.EventoDespesas;
using CCMZ_API.Models.Painel.Alocacao;

namespace CCMN_API.Services.DespesasEvento;

public interface IDespesaEventoService
{
    //GET
    Task<IEnumerable<TbDespesaEvento>> GetDespesasEvento(int codigoEvento);
    Task<EventoCusto> GetEventoCusto(int codigoEvento);
   /* Task<IEnumerable<ComunidadeNome>> GetComunidadesEvento(int codigoEvento);*/
    Task<IEnumerable<ComunidadeEventoDados>> GetComunidadesDados(int codigoEvento);
    Task<PessoasPagantesCobrantes> GetCobrantesPagantes(int codigoEvento);
    Task<decimal> GetValorCozinha(int codigoEvento);
    Task<decimal> GetValorHostiaria(int codigoEvento);

    //POST
    Task AddDespesaEvento(TbDespesaEvento despesaEvento);
    Task AddDespesaCozinha(int codigoEvento, decimal valor);
    Task AddDespesaHostiaria(int codigoEvento, decimal valor);

    //PUT
    Task UpdateDespesaEvento(TbDespesaEvento despesaEvento);
    Task UpdateDespesaCozinha(int codigoEvento, decimal valor);
    Task UpdateDespesaHostiaria(int codigoEvento, decimal valor);
}
