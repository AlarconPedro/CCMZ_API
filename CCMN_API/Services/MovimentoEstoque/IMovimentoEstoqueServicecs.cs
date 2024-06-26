using CCMN_API.Models;

namespace CCMN_API.Services.MovimentoProdutos;

public interface IMovimentoEstoqueServicecs
{
    Task<IEnumerable<TbMovimentoProduto>> GetMovimentosEstoque();
    Task<TbMovimentoProduto> GetMovimentoEstoque(int codigoMovimento);
    Task AddMovimento(TbMovimentoProduto movimentoEstoque);
    Task UpdateMovimento(TbMovimentoProduto movimentoEstoque);
    Task DeleteMovimento(int codigoMovimento);
}
