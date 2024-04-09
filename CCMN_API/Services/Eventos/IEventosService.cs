using CCMZ_API.Models;
using CCMZ_API.Models.Painel.Alocacao;
using CCMZ_API.Models.Painel.Pessoas;
using CCMZ_API.Models.Painel.Quartos;

namespace CCMZ_API.Services.Eventos;

public interface IEventosService
{
    //GET
    Task<IEnumerable<TbEvento>> GetEventos(int mes);
    Task<IEnumerable<EventosNome>> GetEventosAtivos();
    Task<IEnumerable<EventosNome>> GetEventoNome();
    Task<TbEvento> GetEvento(int id);
    Task<IEnumerable<BlocoNome>> GetPavilhoes();
/*    Task<IEnumerable<QuartosNome>> GetQuartos(int codigoEvento, int codigoBloco);*/    
    Task<IEnumerable<QuartoPavilhao>> GetQuartosPavilhao(int codigoPavilhao, int codigoEvento);
    Task<IEnumerable<QuartoPavilhao>> GetQuartosAlocados(int codigoPavilhao, int codigoEvento);
    Task<IEnumerable<Hospedes>> GetPessoaEvento(int codigoComunidade);
    Task<IEnumerable<PessoaQuarto>> GetPessoasAlocadas(int codigoComunidade, int codigoEvento);
    Task<IEnumerable<PessoaQuarto>> GetPessoasQuarto(int codigoQuarto, int codigoEvento);
    Task<IEnumerable<ComunidadeNome>> GetComunidades();
    Task<IEnumerable<Hospedes>> GetHospedes(int codigoEvento);
    //POST
    Task PostEvento(TbEvento evento);
    Task PostQuartos (List<TbEventoQuarto> eventoQuarto, int codigo);   
    Task PostPessoas(List<TbEventoPessoa> eventoPessoa, int codigo);
    //PUT
    Task UpdateEvento(TbEvento evento);
    Task UpdateEventoQuarto(TbEventoQuarto eventoQuarto);
    Task UpdateEventoPessoa(TbEventoPessoa eventoPessoa);
    //DELETE
    Task DeleteEvento(TbEvento evento);
    Task RemoveQuartoEvento(int codigoQuarto);
}
