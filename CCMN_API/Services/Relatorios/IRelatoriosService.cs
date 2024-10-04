using CCMN_API.Models.Painel.Relatorio;

namespace CCMN_API.Services.Relatorios;

public interface IRelatoriosService
{
    Task<IEnumerable<RelatorioProdutosAcabando>> GetProdutosAcabando();
}
