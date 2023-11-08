using CCMZ_API.Models.Painel.Alocacao;

namespace CCMZ_API.Services.Alocacao;

public interface IAlocacaoService
{
    Task<IEnumerable<EventosNome>> GetEventos();
    Task<IEnumerable<ComunidadeNome>> GetComunidades(int codigoEvento);
    Task<IEnumerable<BlocoNome>> GetBlocos(int codigoEvento);
    Task<IEnumerable<QuartosNome>> GetQuartos(int codigoEvento);
}
