using CCMN_API;
using CCMZ_API.Models.Painel.QuartoPessoa;

namespace CCMZ_API.Services.QuartoPessoa;

public interface ICheckinService
{
    Task<IEnumerable<QuartoPessoas>> GetQuartoPessoas(int codigoBloco, int codigoEvento);
    Task<IEnumerable<QuartoPessoas>> GetQuartoPessoasBusca(int codigoEvento, string busca);

    Task UpdateQuartoPessoa(TbQuartoPessoa quartoPessoa);
}
