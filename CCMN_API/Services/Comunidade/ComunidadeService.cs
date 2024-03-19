using CCMZ_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CCMZ_API.Services.Comunidade;

using CCMZ_API.Models.Painel.Comunidade;

public class ComunidadeService : IComunidadeService
{
    private readonly CCMNContext _context;

    public ComunidadeService(CCMNContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<string>> GetCidadesComunidades()
    {
        return await _context.TbComunidades.Select(x => x.ComCidade).Distinct().ToListAsync();
    }

    public async Task<IEnumerable<Comunidade>> GetComunidades(string cidade)
    {
        return await _context.TbComunidades
            .Where(c => cidade != "Todos" ?  c.ComCidade.ToUpper() == cidade.ToUpper() : true)
            .Select(x => new Comunidade
        {
            ComCodigo = x.ComCodigo,
            ComCidade = x.ComCidade,
            ComUf = x.ComUf,
            ComNome = x.ComNome,
            QtdPessoas = _context.TbPessoas.Where(p => p.ComCodigo == x.ComCodigo).Count()
        }).ToListAsync();
    }

    public async Task<IEnumerable<ComunidadeNome>> GetComunidadesNomes()
    {
        return await _context.TbComunidades.Select(x => new ComunidadeNome
        {
            ComCodigo = x.ComCodigo,
            ComNome = x.ComNome,
            ComCidade = x.ComCidade,
            ComUf = x.ComUf
        }).ToListAsync();
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
