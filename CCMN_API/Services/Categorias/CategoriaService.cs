using CCMN_API.Models;
using CCMZ_API;
using Microsoft.EntityFrameworkCore;

namespace CCMN_API.Services.Categorias;

public class CategoriaService : ICategoriaService
{
    private readonly CCMNContext _context;

    public CategoriaService(CCMNContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TbCategoria>> GetCategorias()
    {
        return await _context.TbCategorias.ToListAsync();
    }

    public async Task<TbCategoria> GetCategoria(int catCodigo)
    {
        return await _context.TbCategorias.FindAsync(catCodigo);
    }

    public async Task AddCategoria(TbCategoria categoria)
    {
        _context.TbCategorias.Add(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategoria(TbCategoria categoria)
    {
        _context.TbCategorias.Update(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoria(int catCodigo)
    {
        _context.TbCategorias.Where(c => c.CatCodigo == catCodigo).ExecuteDelete();
        await _context.SaveChangesAsync();
    }
}
