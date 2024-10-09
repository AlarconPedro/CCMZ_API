using CCMN_API.Models.Painel.Hospedagem.Promocao;
using CCMN_API.Models.Painel.Promocao;
using CCMZ_API;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CCMN_API.Services.Promocoes;

public class PromocoesService : IPromocoesService
{
    private readonly CCMNContext _context;
    private static List<ListarGanhadorCupom> dados = new List<ListarGanhadorCupom>();

    public PromocoesService(CCMNContext context)
    {
        _context = context;
    }

    //GET
    public async Task<IEnumerable<ListarParticipantes>> GetParticipantes(int codigoPromocao)
    {
        return await _context.TbPromocoesParticipantes
            .Where(p => p.ProCodigo == codigoPromocao)
            .Select(p => new ListarParticipantes
            {
                codigo = p.ParCodigo,
                nome = p.ParNome,
            }).ToListAsync();
    }

    public async Task<IEnumerable<ListarGanhadorCupom>> GetGanhador(string filtro, int skip, int take, string? codigoCupom)
    {
        if (skip < take) {
            if (codigoCupom.IsNullOrEmpty())
            {
                dados.Clear();
                dados = await _context.TbPromocoesCupons
                   .Where(p => filtro.Equals("T") ?
                                true :
                                filtro.Equals("V") ?
                                    (p.CupVendido == true) :
                                    filtro.Equals("S") ?
                                    (p.CupSorteado == true) : true)
                   .Select(x => new ListarGanhadorCupom
                   {
                       CupCodigo = x.CupCodigo,
                       CupNumero = x.CupNumero,
                       ParCidade = x.ParCodigoNavigation.ParCidade ?? "",
                       ParCodigo = x.ParCodigo ?? 0,
                       ParFone = x.ParCodigoNavigation.ParFone ?? "",
                       ParNome = x.ParCodigoNavigation.ParNome ?? "",
                       ParUf = x.ParCodigoNavigation.ParUf ?? "",
                       CupSorteado = x.CupSorteado,
                       CupVendido = x.CupVendido,
                   }).ToListAsync();

                foreach (var item in dados)
                {
                    dados[0].QtdCupons = dados.Count();
                }

                return dados.Skip(skip).Take(take);
            } else
            {
                dados.Clear();
                dados = await _context.TbPromocoesCupons
                   .Where(p => p.CupNumero.Contains(codigoCupom))
                   .Select(x => new ListarGanhadorCupom
                   {
                       CupCodigo = x.CupCodigo,
                       CupNumero = x.CupNumero,
                       ParCidade = x.ParCodigoNavigation.ParCidade ?? "",
                       ParCodigo = x.ParCodigo ?? 0,
                       ParFone = x.ParCodigoNavigation.ParFone ?? "",
                       ParNome = x.ParCodigoNavigation.ParNome ?? "",
                       ParUf = x.ParCodigoNavigation.ParUf ?? "",
                       CupSorteado = x.CupSorteado,
                       CupVendido = x.CupVendido,
                   }).ToListAsync();

                foreach (var item in dados)
                {
                    dados[0].QtdCupons = dados.Count();
                }

                return dados;
            }

        }

        return dados.Skip(skip).Take(take);
    }

    public async Task<IEnumerable<ListarPromocoes>> GetPromocoes()
    {
        return await _context.TbPromocoes
            .Select(p => new ListarPromocoes
            {
                ProCodigo = p.ProCodigo,
                ProDatafim = p.ProDatafim,
                ProDatainicio = p.ProDatainicio,
                ProNome = p.ProNome
            }).ToListAsync();
    }

    public async Task<IEnumerable<ListarSorteios>> GetSorteios()
    {
        return await _context.TbPromocoesSorteios.Select(p => new ListarSorteios {
                CupCodigo = p.CupCodigo,
                ParCodigo = p.ParCodigo,
                ProCodigo = p.ProCodigo,
                PreCodigo = p.PreCodigo,
                SorCodigo = p.SorCodigo,
                SorData = p.SorData,
            }).ToListAsync();
    }

    public async Task<TbPromocoesParticipante> GetDadosParticipantes(string cpfParticipantes)
    {
        return await _context.TbPromocoesParticipantes.Where(pp => pp.ParCpf == cpfParticipantes).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TbPromocoesCupon>> GetCuponsParticipante(int codigoParticipante)
    {
        return await _context.TbPromocoesCupons.Where(p => p.ParCodigo == codigoParticipante).ToListAsync();
    }

    public async Task<(int, ListarGanhadorCupom)> SortearCupom(string cupom)
    {
        var cupons = await _context.TbPromocoesCupons.Where(p => p.CupNumero == cupom).FirstOrDefaultAsync();
        if (cupons != null)
        {
            if (cupons.CupSorteado ?? false)
            {
                return (400, null);
            } else
            {
                if (cupons.ParCodigo > 0 || cupons.ParCodigo != null)
                {
                    cupons.CupSorteado = true;
                    _context.TbPromocoesCupons.Update(cupons);
                    await _context.SaveChangesAsync();
                    return (200, await _context.TbParticipantesCupons
                           .Where(pc => pc.CupCodigo.Equals(cupons.CupCodigo))
                           .Select(xx => new ListarGanhadorCupom
                           {
                               ParCidade = xx.ParCodigoNavigation.ParCidade,
                               ParCodigo = xx.ParCodigoNavigation.ParCodigo,
                               CupCodigo = xx.CupCodigo,
                               CupNumero = xx.CupCodigoNavigation.CupNumero,
                               CupSorteado = xx.CupCodigoNavigation.CupSorteado,
                               ParFone = xx.ParCodigoNavigation.ParFone,
                               ParNome = xx.ParCodigoNavigation.ParNome,
                               ParUf = xx.ParCodigoNavigation.ParUf,
                               CupVendido = xx.CupCodigoNavigation.CupVendido,
                           }).FirstOrDefaultAsync());
                    
                }
                else
                {
                    return (401, null);
                }
            }
        } else
        {
            return (404, null);
        }
    }

    //POST
    public async Task<TbPromocoesParticipante> AddParticipantes(TbPromocoesParticipante participantes)
    {
        var pessoaCadastrada = await _context.TbPromocoesParticipantes.Where(p => p.ParCpf == participantes.ParCpf).FirstOrDefaultAsync();
        if (pessoaCadastrada != null)
        {
            return pessoaCadastrada;
        } else
        {
            var result = await _context.TbPromocoesParticipantes.FirstOrDefaultAsync();
            if (result != null)
            {
                participantes.ParCodigo = await _context.TbPromocoesParticipantes.MaxAsync(p => p.ParCodigo) + 1;
            }
            else
            {
                participantes.ParCodigo = 1;
            }
            _context.TbPromocoesParticipantes.Add(participantes);
            await _context.SaveChangesAsync();
            return participantes;
        }
       
    }

    public async Task<(bool, string)> AddCupons(TbPromocoesCupon cupons)
    {
        var cupom = await _context.TbPromocoesCupons.Where(pc => pc.CupNumero == cupons.CupNumero).FirstOrDefaultAsync();
        if (cupom != null) {
            if (cupom.CupVendido ?? false){
                return (false, "Cupom já Lançado !");
            } 
            cupom.ParCodigo = cupons.ParCodigo;
            cupom.CupVendido = true;
            _context.TbPromocoesCupons.Update(cupom);
            await _context.SaveChangesAsync();
            return (true, "Cupom Cadastrado com Sucesso !");
        } else
        {
            return (false, "Nenhum Cupom Encontrado !");
        }
    }
        
    public async Task AddSorteios(TbPromocoesSorteio sorteios)
    {
        var result = await _context.TbPromocoesSorteios.FirstOrDefaultAsync();
        if (result != null)
        {
            sorteios.SorCodigo = await _context.TbPromocoesSorteios.MaxAsync(p => p.SorCodigo) + 1;
        } else
        {
            sorteios.SorCodigo = 1;
        }
        _context.TbPromocoesSorteios.Add(sorteios);
        await _context.SaveChangesAsync();
    }

    public async Task AddPromocoes(TbPromoco promocoes)
    {
        var result = await _context.TbPromocoes.FirstOrDefaultAsync();
        if (result != null)
        {
            promocoes.ProCodigo = await _context.TbPromocoes.MaxAsync(p => p.ProCodigo) + 1;
        } else
        {
            promocoes.ProCodigo = 1;
        }
        _context.TbPromocoes.Add(promocoes);
        await _context.SaveChangesAsync();
    }

    //PUT
    public async Task UpdateParticipantes(TbPromocoesParticipante participantes)
    {
        _context.TbPromocoesParticipantes.Update(participantes);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCupons(TbPromocoesCupon cupons)
    {
        _context.TbPromocoesCupons.Update(cupons);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateSorteios(TbPromocoesSorteio sorteios)
    {
        _context.TbPromocoesSorteios.Update(sorteios);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePromocoes(TbPromoco promocoes)
    {
        _context.TbPromocoes.Update(promocoes);
        await _context.SaveChangesAsync();
    }

    //DELETE
    public async Task DeleteParticipantes(int codigoParticipante)
    {
        var participantes = await _context.TbPromocoesParticipantes.FindAsync(codigoParticipante);
        _context.TbPromocoesParticipantes.Remove(participantes);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCupons(int codigoCupom)
    {
        var cupons = await _context.TbPromocoesCupons.FindAsync(codigoCupom);
        _context.TbPromocoesCupons.Remove(cupons);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSorteios(int codigoSorteio)
    {
        var sorteios = await _context.TbPromocoesSorteios.FindAsync(codigoSorteio);
        _context.TbPromocoesSorteios.Remove(sorteios);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePromocoes(int codigoPromocao)
    {
        var promocoes = await _context.TbPromocoes.FindAsync(codigoPromocao);
        _context.TbPromocoes.Remove(promocoes);
        await _context.SaveChangesAsync();
    }
}
