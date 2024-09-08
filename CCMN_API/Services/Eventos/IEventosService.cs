using CCMN_API;
using CCMN_API.Models;
using CCMN_API.Models.Painel.Hospedagem.Alocacao;
using CCMN_API.Models.Painel.Hospedagem.Evento;
using CCMN_API.Models.Painel.Hospedagem.Pessoas;
using CCMN_API.Models.Painel.Hospedagem.Quartos;
using CCMZ_API.Models;

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
    Task<IEnumerable<PessoaEvento>> GetPessoaEvento(int codigoComunidade, int codigoEvento);
    Task<IEnumerable<PessoaQuarto>> GetPessoasAlocadas(int codigoComunidade, int codigoEvento);
    Task<IEnumerable<PessoaQuarto>> GetPessoasQuarto(int codigoQuarto, int codigoEvento);
    Task<IEnumerable<ComunidadeNome>> GetComunidades();
    Task<IEnumerable<Hospedes>> GetHospedes(int codigoEvento);
    //POST
    Task PostEvento(TbEvento evento);
    Task PostQuartos (List<TbEventoQuarto> eventoQuarto, int codigo);   
    Task PostPessoas(List<TbEventoPessoa> eventoPessoa);
    //PUT
    Task UpdateEvento(TbEvento evento);
    Task UpdateEventoQuarto(TbEventoQuarto eventoQuarto);
    Task UpdateEventoPessoa(TbEventoPessoa eventoPessoa);
    //DELETE
    Task DeleteEventoPessoas(List<TbEventoPessoa> evento);
    Task RemoverPessoasEvento(List<int> codigoPessoas, int codigoEvento);
    Task DeleteEvento(TbEvento evento);
    Task RemoveQuartoEvento(int codigoQuarto);
}
