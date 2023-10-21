using CCMZ_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CCMZ_API.Services.Quartos;

using CCMZ_API.Models.Painel.Quartos;

public class QuartosService : IQuartosService
{

    private readonly CCMZContext _context;

    public QuartosService(CCMZContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Quartos>> GetQuartos()
    {
        return await _context.TbQuartos.Select(x => new Quartos
        {
            QuaCodigo = x.QuaCodigo,
            QuaNome = x.QuaNome,
            QuaQtdcamas = x.QuaQtdcamas,
            Bloco = _context.TbBlocos.FirstOrDefault(b => b.BloCodigo == x.BloCodigo).BloNome
        }).ToListAsync();
    }

    public async Task<TbQuarto> GetQuartoById(int id)
    {
        return await _context.TbQuartos.FirstOrDefaultAsync(q => q.QuaCodigo == id);
    }

    public async Task PostQuarto(TbQuarto tbQuarto)
    {
        _context.TbQuartos.Add(tbQuarto);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateQuarto(TbQuarto tbQuarto)
    {
        _context.Entry(tbQuarto).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteQuarto(TbQuarto tbQuarto)
    {
        _context.TbQuartos.Remove(tbQuarto);
        await _context.SaveChangesAsync();
    }
}
