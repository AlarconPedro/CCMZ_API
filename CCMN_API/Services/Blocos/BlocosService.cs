using CCMZ_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CCMZ_API.Services.Blocos;

using CCMN_API;
using CCMZ_API.Models.Painel.Bloco;

public class BlocosService : IBlocosService
{
    private readonly CCMNContext _context;

    public BlocosService(CCMNContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Bloco>> GetBlocos()
    {
        return await _context.TbBlocos.Select(x => new Bloco
        {
            BloCodigo = x.BloCodigo,
            BloNome = x.BloNome,
            QtdQuartos = _context.TbQuartos.Where(q => q.BloCodigo == x.BloCodigo).Count(),
            QtdLivres = _context.TbQuartos.Where(q => q.BloCodigo == x.BloCodigo && q.QuaQtdcamas != 0).Count(),
            QtdOcupados = _context.TbQuartos.Where(q => q.BloCodigo == x.BloCodigo && q.QuaQtdcamas == 0).Count()
        }).ToListAsync();
    }

    public async Task<IEnumerable<BlocoNome>> GetBlocosNomes()
    {
        return await _context.TbBlocos.Select(x => new BlocoNome
        {
            BloCodigo = x.BloCodigo,
            BloNome = x.BloNome
        }).ToListAsync();
    }

    public async Task<TbBloco> GetBloco(int id)
    {
        return await _context.TbBlocos.FirstOrDefaultAsync(bloco => bloco.BloCodigo == id);
    }

    public async Task PostBloco(TbBloco bloco)
    {
        if (bloco.BloCodigo == 0)
        {
            var blocoLast = await _context.TbBlocos.FirstOrDefaultAsync();
            if (blocoLast != null)
            {
                bloco.BloCodigo = await _context.TbBlocos.MaxAsync(b => b.BloCodigo) + 1;
            } else {
                bloco.BloCodigo = 1;
            }
        } else
        {
            await UpdateBloco(bloco);
        }
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
