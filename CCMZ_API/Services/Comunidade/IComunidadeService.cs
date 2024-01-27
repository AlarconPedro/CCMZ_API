using CCMZ_API.Models;

namespace CCMZ_API.Services.Comunidade;

using CCMZ_API.Models.Painel.Comunidade;

public interface IComunidadeService
{
    //GET
    Task<IEnumerable<Comunidade>> GetComunidades();
    Task<IEnumerable<ComunidadeNome>> GetComunidadesNomes();
    Task<TbComunidade> GetComunidade(int id);
    //POST
    Task PostComunidade(TbComunidade comunidade);
    //PUT
    Task UpdateComunidade(TbComunidade comunidade);
    //DELETE
    Task DeleteComunidade(TbComunidade comunidade);
}
    