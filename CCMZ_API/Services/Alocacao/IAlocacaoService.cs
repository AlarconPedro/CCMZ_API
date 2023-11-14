using CCMZ_API.Models.Painel.Alocacao;
using CCMZ_API.Models.Painel.Pessoas;

namespace CCMZ_API.Services.Alocacao;

public interface IAlocacaoService
{
    Task<IEnumerable<EventosNome>> GetEventos();
    Task<IEnumerable<ComunidadeNome>> GetComunidades(int codigoEvento);
    Task<IEnumerable<PessoasNome>> GetPessoasComunidade(int codigoEvento, int codigoComunidde);
    Task<IEnumerable<BlocoNome>> GetBlocos(int codigoEvento);
    Task<IEnumerable<QuartosNome>> GetQuartos(int codigoEvento);
}
