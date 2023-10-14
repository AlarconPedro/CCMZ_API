namespace CCMZ_API.Services.Pessoas;

using CCMZ_API.Models;
using CCMZ_API.Models.Painel.Pessoas;
using Microsoft.EntityFrameworkCore;

public class PessoasService : IPessoasService
{
    private readonly CCMZContext _context;

    public PessoasService(CCMZContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pessoas>> GetPessoas()
    {
        return await _context.TbPessoas.Select(x => new Pessoas
        {
            PesCodigo = x.PesCodigo,
            PesNome = x.PesNome,
            PesGenero = x.PesGenero,
            Comunidade = _context.TbComunidades.Where(c => c.ComCodigo == x.ComCodigo).Select(c => c.ComNome).FirstOrDefault()
        }).ToListAsync();
    }

    public async Task<TbPessoa> GetPessoaId(int idPessoa)
    {
        return await _context.TbPessoas.FirstOrDefaultAsync(p => p.PesCodigo == idPessoa);
    }

    public async Task<PessoaDetalhes> GetPessoaDetalhe(int idPessoa)
    {
        return await _context.TbPessoas.Where(p => p.PesCodigo == idPessoa).Select(x => new PessoaDetalhes
        {
            PesCodigo = x.PesCodigo,
            PesNome = x.PesNome,
            PesGenero = x.PesGenero,
            Comunidade = _context.TbComunidades.Where(c => c.ComCodigo == x.ComCodigo).Select(c => c.ComNome).FirstOrDefault(),
            Evento = _context.TbEventoPessoas
                .Where(ep => ep.PesCodigo == x.PesCodigo)
                .Join(_context.TbEventos, ep => ep.EveCodigo, e => e.EveCodigo, (ep, e) => e.EveNome).FirstOrDefault(),
            Casal = _context.TbCasais
                .Where(c => c.CasEsposo == x.PesCodigo || c.CasEsposa == x.PesCodigo)
                .Join(_context.TbPessoas, c => c.CasEsposo, p => p.PesCodigo, (c, p) => p.PesNome)
                .FirstOrDefault(),
            Quarto = _context.TbQuartoPessoas
                .Where(qp => qp.PesCodigo == x.PesCodigo)
                .Join(_context.TbQuartos, qp => qp.QuaCodigo, q => q.QuaCodigo, (qp, q) => q.QuaNome).FirstOrDefault()
        }).FirstOrDefaultAsync();
    }

    public async Task PostPessoas(TbPessoa tbPessoa)
    {
        if (tbPessoa.PesCodigo == 0)
        {
            var lastPessoa = await _context.TbPessoas.FirstOrDefaultAsync();
            if (lastPessoa != null)
            {
                tbPessoa.PesCodigo = await _context.TbPessoas.MaxAsync(p => p.PesCodigo) + 1;
            } else
            {
                  tbPessoa.PesCodigo = 1;
            }
            _context.TbPessoas.Add(tbPessoa);
            await _context.SaveChangesAsync();
        } else
        {
            await PutPessoas(tbPessoa);
        }
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
