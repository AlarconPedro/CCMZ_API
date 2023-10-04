using CCMZ_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CCMZ_API.Services.Blocos;

public class BlocosService : IBlocosService
{
    private readonly CCMZContext _context;

    public BlocosService(CCMZContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<TbBloco>> GetBlocos()
    {
        return await _context.TbBlocos.ToListAsync();
    }
    public async Task<TbBloco> GetBloco(int id)
    {
        return await _context.TbBlocos.FirstOrDefaultAsync(bloco => bloco.BloCodigo == id);
    }

    public async Task PostBloco(TbBloco bloco)
    {
        _context.TbBlocos.Add(bloco);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBloco(TbBloco bloco)
    {
        _context.TbBlocos.Update(bloco);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteBloco(TbBloco bloco)
    {
        _context.TbBlocos.Remove(bloco);
        await _context.SaveChangesAsync();
    }

}
