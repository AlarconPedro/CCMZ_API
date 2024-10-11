using CCMN_API.Models.Painel.Hospedagem.Promocao;
using CCMN_API.Models.Painel.Promocao;

namespace CCMN_API.Services.Promocoes;

public interface IPromocoesService
{
    //GET
    Task<IEnumerable<ListarPromocoes>> GetPromocoes(string filtro);
    Task<IEnumerable<ListarGanhadorCupom>> GetGanhador(string filtro, int skip, int take, string? codigoCupom);
    Task<IEnumerable<ListarParticipantes>> GetParticipantes(int codigoPromocao);
    Task<IEnumerable<ListarSorteios>> GetSorteios();
    Task<IEnumerable<ListarPremios>> GetPremios(int codigoPromocao);
    Task<IEnumerable<TbPromocoesCupon>> GetCuponsParticipante(int codigoParticipante);
    Task<TbPromocoesParticipante> GetDadosParticipantes(string cpfParticipantes);
    Task<(int, ListarGanhadorCupom)> SortearCupom(string cupom, int codigoSorteio);

    //POST
    Task<TbPromocoesParticipante> AddParticipantes(TbPromocoesParticipante participantes);
    Task<(bool, string)> AddCupons(TbPromocoesCupon cupons);
    Task AddPremios(TbPromocoesPremio premios);
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
