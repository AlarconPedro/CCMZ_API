﻿namespace CCMZ_API.Services.Pessoas;

using CCMN_API;
using CCMN_API.Models;
using CCMN_API.Models.Painel.Hospedagem.Alocacao;
using CCMN_API.Models.Painel.Hospedagem.Comunidade;
using CCMN_API.Models.Painel.Hospedagem.Pessoas;
using CCMZ_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class PessoasService : IPessoasService
{
    private readonly CCMNContext _context;

    public PessoasService(CCMNContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pessoas>> GetPessoas(int codigoComunidade, string cidade)
    {
        return await _context.TbPessoas.Where(p => (codigoComunidade > 0 
            ? (p.ComCodigo == codigoComunidade) 
            : true)
            && cidade.Equals("Todos") 
                ? true 
                : (p.ComCodigoNavigation.ComCidade.Contains(cidade)))
        .Select(x => new Pessoas
        {
            PesCodigo = x.PesCodigo,
            PesNome = x.PesNome,
            PesGenero = x.PesGenero,
            ComCodigo = x.ComCodigo,
            Comunidade = _context.TbComunidades.Where(c => c.ComCodigo == x.ComCodigo).Select(c => c.ComNome).FirstOrDefault(),
            PesCatequista = x.PesCatequista,
            PesResponsavel = x.PesResponsavel,
            PesSalmista = x.PesSalmista,
/*            PesObservacao = x.PesObservacao*/        
        }).ToListAsync();
    }

    public async Task<IEnumerable<string>> GetCidades()
    {
        //return await _context.TbPessoas.Select(x => x.ComCodigo).Distinct().Join(_context.TbComunidades, p => p, c => c.ComCodigo, (p, c) => c.ComCidade).Distinct().ToListAsync();
        return await _context.TbComunidades.Select(c =>  c.ComCidade).Distinct().ToListAsync();
    }

    public async Task<IEnumerable<ComunidadesNome>> GetComunidadesNomes(string cidade)
    {
        return await _context.TbComunidades.Where(c => cidade.Equals("Todos") 
        ? true 
        : c.ComCidade.Contains(cidade)).Select(x => new ComunidadesNome
        {
            ComCodigo = x.ComCodigo,
            ComNome = x.ComNome,
            ComCidade = x.ComCidade,
            ComUf = x.ComUf
        }).ToListAsync();
    }

    public async Task<IEnumerable<Pessoas>> GetPessoasBusca(int codigoComunidade, string busca)
    {
        return await _context.TbPessoas.Where(p => p.ComCodigo == codigoComunidade && p.PesNome.Contains(busca)).Select(x => new Pessoas
        {
            PesCodigo = x.PesCodigo,
            PesNome = x.PesNome,
            PesGenero = x.PesGenero,
            ComCodigo = x.ComCodigo,
            Comunidade = _context.TbComunidades.Where(c => c.ComCodigo == x.ComCodigo).Select(c => c.ComNome).FirstOrDefault(),
            PesCatequista = x.PesCatequista,
            PesResponsavel = x.PesResponsavel,
            PesSalmista = x.PesSalmista,
        }).ToListAsync();
    }

    public async Task<TbPessoa> GetPessoaId(int idPessoa)
    {
        return await _context.TbPessoas.FirstOrDefaultAsync(p => p.PesCodigo == idPessoa);
    }

    public async Task<bool> GetPessoaPodeExcluir(int idPessoa)
    {
        var pessoa = await _context.TbPessoas.FirstOrDefaultAsync(p => p.PesCodigo == idPessoa);
        if (pessoa == null)
        {
            return false;
        }
        var casal = await _context.TbCasais.FirstOrDefaultAsync(c => c.CasEsposo == idPessoa || c.CasEsposa == idPessoa);
        if (casal != null)
        {
            return false;
        }
        var quarto = await _context.TbQuartoPessoas.FirstOrDefaultAsync(qp => qp.PesCodigo == idPessoa);
        if (quarto != null)
        {
            return false;
        }
        var evento = await _context.TbEventoPessoas.FirstOrDefaultAsync(ep => ep.PesCodigo == idPessoa);
        if (evento != null)
        {
            return false;
        }
        return true;
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
