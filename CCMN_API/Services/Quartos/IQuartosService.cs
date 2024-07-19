using CCMZ_API.Models;

namespace CCMZ_API.Services.Quartos;

using CCMN_API;
using CCMN_API.Models;
using CCMZ_API.Models.Painel.Quartos;
public interface IQuartosService
{
    //GET
    Task <IEnumerable<Quartos>> GetQuartos(int codigoBloco);
    Task <IEnumerable<Quartos>> GetQuartosBusca(int codigoBloco, string busca);
    Task<TbQuarto> GetQuartoById(int id);
    //POST
    Task PostQuarto(TbQuarto tbQuarto);
    //PUT
    Task UpdateQuarto(TbQuarto tbQuarto);
    //DELETE
    Task DeleteQuarto(TbQuarto tbQuarto);
}
