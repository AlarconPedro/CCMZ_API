using CCMZ_API.Models;

namespace CCMZ_API.Services.Pessoas;

public interface IPessoasService
{
    //GET
    Task<IEnumerable<TbPessoa>> GetPessoas(); 
    //POST
    //PUT
    //DELETE
}
