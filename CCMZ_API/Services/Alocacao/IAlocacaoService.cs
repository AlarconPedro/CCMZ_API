using CCMZ_API.Models.Painel.Alocacao;
using CCMZ_API.Models.Painel.Pessoas;

namespace CCMZ_API.Services.Alocacao;

public interface IAlocacaoService
{
    //GET
    Task<IEnumerable<EventosNome>> GetEventos();
    Task<IEnumerable<ComunidadeNome>> GetComunidades(int codigoEvento);
    Task<IEnumerable<PessoasNome>> GetPessoasComunidade(int codigoEvento, int codigoComunidde);
    Task<IEnumerable<BlocoNome>> GetBlocos(int codigoEvento);
    Task<IEnumerable<QuartosNome>> GetQuartos(int codigoEvento, int codigoBloco);
    Task<IEnumerable<PessoasAlocadas>> GetPessoasQuarto(int codigoQuarto);
    //INSERT
/*    Task AddPessoaQuarto(int codigoQuarto, int codigoPessoa);*/    
    Task AddPessoaQuarto(TbQuartoPessoa quartoPessoa);
    //UPDATE
    Task AtualizarPessoaQuarto(TbQuartoPessoa quartoPessoa);
    //DELETE
    Task RemoverPessoaQuarto(TbQuartoPessoa quartoPessoa);
}
