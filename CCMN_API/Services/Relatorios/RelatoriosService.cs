using CCMN_API.Models.Painel.Relatorio;
using CCMZ_API;
using Microsoft.EntityFrameworkCore;

namespace CCMN_API.Services.Relatorios;

public class RelatoriosService : IRelatoriosService
{
    private readonly CCMNContext _context;

    public RelatoriosService(CCMNContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RelatorioProdutosAcabando>> GetProdutosAcabando()
    {
        return await _context.TbProdutos.Where(p => p.ProQuantidade <= p.ProQuantidadeMin)
                .Select(x => new RelatorioProdutosAcabando
                {
                    ProQuantidade = x.ProQuantidade,
                    ProCodigo = x.ProCodigo,
                    ProDescricao = x.ProDescricao,
                    ProImagem = x.ProImagem,
                    ProMedida = x.ProMedida,
                    ProNome = x.ProNome,
                    ProUniMedida = x.ProUniMedida,
                    ProValor = x.ProValor,
                }).ToListAsync();
    }
}
