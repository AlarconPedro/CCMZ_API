using CCMN_API;
using CCMN_API.Models;
using CCMN_API.Models.Painel.Hospedagem.Checkin;
using CCMN_API.Models.Painel.Hospedagem.QuartoPessoa;

namespace CCMZ_API.Services.QuartoPessoa;

public interface ICheckinService
{
    Task<IEnumerable<QuartosCheckinEvento>> GetQuartoCheckin( int codigoEvento);
    Task<IEnumerable<QuartoPessoas>> GetQuartoPessoasBusca(int codigoEvento, string busca);

    Task UpdateQuartoPessoa(TbQuartoPessoa quartoPessoa);
}
