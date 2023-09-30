using CCMZ_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CCMZ_API.Services.Pessoas;

public class PessoasService : IPessoasService
{
    private readonly CcmzContext _context;

    public PessoasService(CcmzContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TbPessoa>> GetPessoas()
    {
        return await _context.TbPessoas.ToListAsync();
    }

    public async Task PostPessoas(TbPessoa tbPessoa)
    {
        _context.TbPessoas.Add(tbPessoa);
        await _context.SaveChangesAsync();
    }

    public async Task PutPessoas(TbPessoa tbPessoa)
    {
        _context.Entry(tbPessoa).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeletePessoas(TbPessoa tbPessoa)
    {
        _context.TbPessoas.Remove(tbPessoa);
        await _context.SaveChangesAsync();
    }

}
