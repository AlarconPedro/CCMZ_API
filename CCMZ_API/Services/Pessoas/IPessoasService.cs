using CCMZ_API.Models;

namespace CCMZ_API.Services.Pessoas;

public interface IPessoasService
{
    //GET
    Task<IEnumerable<TbPessoa>> GetPessoas();
    //POST
    Task PostPessoas(TbPessoa tbPessoa);
    //PUT
    Task PutPessoas(TbPessoa tbPessoa);
    //DELETE
    Task DeletePessoas(TbPessoa tbPessoa);
}
