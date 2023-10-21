using CCMZ_API.Models;

namespace CCMZ_API.Services.Quartos;

using CCMZ_API.Models.Painel.Quartos;
public interface IQuartosService
{
    //GET
    Task <IEnumerable<Quartos>> GetQuartos();
    Task<TbQuarto> GetQuartoById(int id);
    //POST
    Task PostQuarto(TbQuarto tbQuarto);
    //PUT
    Task UpdateQuarto(TbQuarto tbQuarto);
    //DELETE
    Task DeleteQuarto(TbQuarto tbQuarto);
}
