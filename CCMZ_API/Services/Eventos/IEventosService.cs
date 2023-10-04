using CCMZ_API.Models;

namespace CCMZ_API.Services.Eventos;

public interface IEventosService
{
    //GET
    Task<IEnumerable<TbEvento>> GetEventos();
    Task<TbEvento> GetEvento(int id);
    //POST
    Task PostEvento(TbEvento evento);
    //PUT
    Task UpdateEvento(TbEvento evento);
    //DELETE
    Task DeleteEvento(TbEvento evento);
}
