using CCMN_API.Models.Painel.Promocao;

namespace CCMN_API.Services.Promocoes;

public interface IPromocoesService
{
    Task<IEnumerable<ListarPromocoes>> GetPromocoes();
    Task<IEnumerable<ListarParticipantes>> GetParticipantes(int codigoPromocao);
}
