namespace CCMZ_API.Services.Pessoas;

using CCMN_API;
using CCMN_API.Models;
using CCMN_API.Models.Painel.Hospedagem.Alocacao;
using CCMN_API.Models.Painel.Hospedagem.Pessoas;
using CCMZ_API.Models;

public interface IPessoasService
{
    //GET
    Task<IEnumerable<Pessoas>> GetPessoas(int codigoComunidade, string cidade);
    Task<IEnumerable<string>> GetCidades();
    Task<IEnumerable<ComunidadeNome>> GetComunidadesNomes(string cidade);
    Task<IEnumerable<Pessoas>> GetPessoasBusca(int codigoComunidade, string busca);
    Task<TbPessoa> GetPessoaId(int idPessoa);
    Task<bool> GetPessoaPodeExcluir(int idPessoa);
    Task<PessoaDetalhes> GetPessoaDetalhe(int idPessoa);
    //POST
    Task PostPessoas(TbPessoa tbPessoa);
    //PUT
    Task PutPessoas(TbPessoa tbPessoa);
    //DELETE
    Task DeletePessoas(TbPessoa tbPessoa);
}
