using CCMZ_API.Models;

namespace CCMZ_API.Services.Blocos;

public interface IBlocosService
{
    //GET
    Task<IEnumerable<TbBloco>> GetBlocos();
    Task<TbBloco> GetBloco(int id);
    //POST
    Task PostBloco(TbBloco bloco);
    //PUT
    Task UpdateBloco(TbBloco bloco);
    //DELETE
    Task DeleteBloco(TbBloco bloco);
}
