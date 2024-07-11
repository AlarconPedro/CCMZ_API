using CCMZ_API.Models;

namespace CCMZ_API.Services.Blocos;

using CCMN_API.Models;
using CCMZ_API.Models.Painel.Bloco;

public interface IBlocosService
{
    //GET
    Task<IEnumerable<Bloco>> GetBlocos();
    Task<IEnumerable<BlocoNome>> GetBlocosNomes();
    Task<TbBloco> GetBloco(int id);
    //POST
    Task PostBloco(TbBloco bloco);
    //PUT
    Task UpdateBloco(TbBloco bloco);
    //DELETE
    Task DeleteBloco(TbBloco bloco);
}
