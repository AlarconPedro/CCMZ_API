using CCMN_API.Models.Painel.Hospedagem.Promocao;
using CCMN_API.Models.Painel.Promocao;

namespace CCMN_API.Services.Promocoes;

public interface IPromocoesService
{
    //GET
    Task<IEnumerable<ListarPromocoes>> GetPromocoes();
    Task<IEnumerable<ListarGanhadorCupom>> GetGanhador(string filtro, string? codigoCupom);
    Task<IEnumerable<ListarParticipantes>> GetParticipantes(int codigoPromocao);
    Task<IEnumerable<ListarSorteios>> GetSorteios();
    Task<IEnumerable<TbPromocoesCupon>> GetCuponsParticipante(int codigoParticipante);
    Task<TbPromocoesParticipante> GetDadosParticipantes(string cpfParticipantes);

    //POST
    Task<TbPromocoesParticipante> AddParticipantes(TbPromocoesParticipante participantes);
    Task<(bool, string)> AddCupons(TbPromocoesCupon cupons);
    Task AddSorteios(TbPromocoesSorteio sorteios);
    Task AddPromocoes(TbPromoco promocoes);

    //PUT
    Task UpdateParticipantes(TbPromocoesParticipante participantes);
    Task UpdateCupons(TbPromocoesCupon cupons);
    Task UpdateSorteios(TbPromocoesSorteio sorteios);
    Task UpdatePromocoes(TbPromoco promocoes);

    //DELETE
    Task DeleteParticipantes(int codigoParticipante);
    Task DeleteCupons(int codigoCupom);
    Task DeleteSorteios(int codigoSorteio);
    Task DeletePromocoes(int codigoPromocao);
}
