using CCMN_API.Models.Painel.Evento;
using CCMN_API.Models.Painel.EventoDespesas;
using CCMZ_API.Models.Painel.Alocacao;

namespace CCMN_API.Services.DespesasEvento;

public interface IDespesaEventoService
{
    //GET
    Task<TbDespesaEvento> GetDespesasEvento(int codigoEvento);
    Task<EventoCusto> GetEventoCusto(int codigoEvento);
    Task<IEnumerable<ComunidadeNome>> GetComunidadesEvento(int codigoEvento);
    Task<PessoasPagantesCobrantes> GetCobrantesPagantes(int codigoEvento);

    //POST
    Task<TbDespesaEvento> AddDespesaEvento(TbDespesaEvento despesaEvento);

    //PUT
    Task<TbDespesaEvento> UpdateDespesaEvento(TbDespesaEvento despesaEvento);
}
