using CCMZ_API.Models;

namespace CCMZ_API.Services.Comunidade;

using CCMN_API.Models;
using CCMZ_API.Models.Painel.Comunidade;

public interface IComunidadeService
{
    //GET
    Task<IEnumerable<string>> GetCidadesComunidades();
    Task<IEnumerable<Comunidade>> GetComunidades(string cidade);
    Task<IEnumerable<ComunidadeNome>> GetComunidadesNomes();
    Task<TbComunidade> GetComunidade(int id);
    //POST
    Task PostComunidade(TbComunidade comunidade);
    //PUT
    Task UpdateComunidade(TbComunidade comunidade);
    //DELETE
    Task DeleteComunidade(TbComunidade comunidade);
}
    