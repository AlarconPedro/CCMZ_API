using CCMN_API.Models;
using CCMN_API.Models.Painel.Hospedagem.EventoDespesas;

namespace CCMN_API.Services.DespesasComunidade;

public interface IDespesaComunidadeService
{
    Task<TbDespesaComunidadeEvento> GetDespesasComunidade(int codigoEvento, int codigoComunidade);

    Task<PessoasPagantesCobrantes> GetCobrantesPagantes(int codigoEvento, int codigoComunidade);

    Task AddDespesaComunidade(TbDespesaComunidadeEvento despesaComunidade);
}
