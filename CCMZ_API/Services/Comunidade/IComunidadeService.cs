using CCMZ_API.Models;

namespace CCMZ_API.Services.Comunidade;

public interface IComunidadeService
{
    //GET
    Task<IEnumerable<TbComunidade>> GetComunidades();
    Task<TbComunidade> GetComunidade(int id);
    //POST
    Task PostComunidade(TbComunidade comunidade);
    //PUT
    Task UpdateComunidade(TbComunidade comunidade);
    //DELETE
    Task DeleteComunidade(TbComunidade comunidade);
}
    