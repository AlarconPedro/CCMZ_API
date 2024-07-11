using CCMN_API.Models;
using CCMN_API.Models.Painel.EventoDespesas;
using CCMZ_API;
using Microsoft.EntityFrameworkCore;

namespace CCMN_API.Services.DespesasComunidade;

public class DespesaComunidadeService : IDespesaComunidadeService
{
    private readonly CCMNContext _context;

    public DespesaComunidadeService(CCMNContext context)
    {
        _context = context;
    }

    public async Task<PessoasPagantesCobrantes> GetCobrantesPagantes(int codigoEvento, int codigoComunidade)
    {
        return await _context.TbEventoPessoas
            .Where(x => x.EveCodigo == codigoEvento && x.PesCodigoNavigation.ComCodigoNavigation.ComCodigo == codigoComunidade)
            .Select(x => new PessoasPagantesCobrantes
            {
                Cobrantes = _context.TbEventoPessoas.Where(x => x.EveCodigo == codigoEvento && x.EvpCobrante == true && x.PesCodigoNavigation.ComCodigoNavigation.ComCodigo == codigoComunidade && x.PesCodigoNavigation.TbQuartoPessoas.Any(qp => qp.PesNaovem) == false).Count(),
                Pagantes = _context.TbEventoPessoas.Where(x => x.EveCodigo == codigoEvento && x.EvpPagante == true && x.PesCodigoNavigation.ComCodigoNavigation.ComCodigo == codigoComunidade && x.PesCodigoNavigation.TbQuartoPessoas.Any(qp => qp.PesNaovem) == false).Count(),
            }).FirstOrDefaultAsync();
    }

    public async Task<TbDespesaComunidadeEvento> GetDespesasComunidade(int codigoEvento, int codigoComunidade)
    {
        return await _context.TbDespesaComunidadeEventos
            .Where(x => x.EveCodigo == codigoEvento && x.ComCodigo == codigoComunidade)
            .Select(x => new TbDespesaComunidadeEvento
            {
                DceNome = x.DceNome,
                DceCodigo = x.DceCodigo,
                DceQuantiadde = x.DceQuantiadde,
                DceValor = x.DceValor,
            }).FirstOrDefaultAsync();
    }

    public async Task AddDespesaComunidade(TbDespesaComunidadeEvento despesaComunidade)
    {
        var despesas = await _context.TbDespesaComunidadeEventos.FirstOrDefaultAsync();
        if (despesas != null)
        {
            despesas.DceCodigo = await _context.TbDespesaComunidadeEventos.MaxAsync(x => x.DceCodigo) + 1;
        } else
        {
            despesas.DceCodigo = 1;
        }
        _context.TbDespesaComunidadeEventos.Add(despesaComunidade);
        await _context.SaveChangesAsync();
    }
}
