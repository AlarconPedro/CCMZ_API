namespace CCMZ_API.Services.Pessoas;
using CCMZ_API.Models;
using CCMZ_API.Models.Painel.Pessoas;

public interface IPessoasService
{
    //GET
    Task<IEnumerable<Pessoas>> GetPessoas(int skip, int take);
    Task<PessoaDetalhes> GetPessoaDetalhe(int idPessoa);
    //POST
    Task PostPessoas(TbPessoa tbPessoa);
    //PUT
    Task PutPessoas(TbPessoa tbPessoa);
    //DELETE
    Task DeletePessoas(TbPessoa tbPessoa);
}
