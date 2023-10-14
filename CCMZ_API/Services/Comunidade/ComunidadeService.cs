using CCMZ_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CCMZ_API.Services.Comunidade;

public class ComunidadeService : IComunidadeService
{
    private readonly CCMZContext _context;

    public ComunidadeService(CCMZContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TbComunidade>> GetComunidades()
    {
        return await _context.TbComunidades.ToListAsync();
    }

    public async Task<TbComunidade> GetComunidade(int id)
    {
        return await _context.TbComunidades.FirstOrDefaultAsync(c => c.ComCodigo == id);
    }

    public async Task PostComunidade(TbComunidade comunidade)
    {
        if (comunidade.ComCodigo == 0)
        {
            var lastComunidade = await _context.TbComunidades.FirstOrDefaultAsync();
            if (lastComunidade != null)
            {
                comunidade.ComCodigo = await _context.TbComunidades.MaxAsync(c => c.ComCodigo) + 1;
            } else
            {
                comunidade.ComCodigo = 1;
            }
            _context.TbComunidades.Add(comunidade);
            await _context.SaveChangesAsync();
        }else 
        { 
            await UpdateComunidade(comunidade);
        }
    }

    public async Task UpdateComunidade(TbComunidade comunidade)
    {
        _context.Entry(comunidade).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteComunidade(TbComunidade comunidade)
    {
        _context.TbComunidades.Remove(comunidade);
        await _context.SaveChangesAsync();
    }
}
