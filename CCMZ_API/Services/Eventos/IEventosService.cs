using CCMZ_API.Models;
using CCMZ_API.Models.Painel.Bloco;
using CCMZ_API.Models.Painel.Comunidade;
using CCMZ_API.Models.Painel.Quartos;

namespace CCMZ_API.Services.Eventos;

public interface IEventosService
{
    //GET
    Task<IEnumerable<TbEvento>> GetEventos();
    Task<TbEvento> GetEvento(int id);
    Task<IEnumerable<BlocoNome>> GetPavilhoes();
    Task<IEnumerable<QuartoPavilhao>> GetQuartosPavilhao(int codigoPavilhao);
    Task<IEnumerable<QuartoPavilhao>> GetQuartosAlocados(int codigoPavilhao, int codigoEvento);
    Task<IEnumerable<ComunidadeNome>> GetComunidades();
    //POST
    Task PostEvento(TbEvento evento);
    Task PostQuartos (List<TbEventoQuarto> eventoQuarto);
    //PUT
    Task UpdateEvento(TbEvento evento);
    Task UpdateEventoQuarto(TbEventoQuarto eventoQuarto);
    //DELETE
    Task DeleteEvento(TbEvento evento);
}
