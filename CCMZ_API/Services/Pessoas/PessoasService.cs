using CCMZ_API.Models;

namespace CCMZ_API.Services.Pessoas;

public class PessoasService : IPessoasService
{
    private readonly CcmzContext _context;

    public PessoasService(CcmzContext context)
    {
        _context = context;
    }
    public Task DeletePessoas(TbPessoa tbPessoa)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TbPessoa>> GetPessoas()
    {
        throw new NotImplementedException();
    }

    public Task PostPessoas(TbPessoa tbPessoa)
    {
        throw new NotImplementedException();
    }

    public Task PutPessoas(TbPessoa tbPessoa)
    {
        throw new NotImplementedException();
    }
}
