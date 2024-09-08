using CCMN_API.Models;
using CCMN_API.Models.Painel.Estoque;

namespace CCMN_API.Services.MovimentoProdutos;

public interface IMovimentoEstoqueServicecs
{
    Task<IEnumerable<MovimentoEstoque>> GetMovimentosEstoque();
    Task<TbMovimentoProduto> GetMovimentoEstoque(int codigoMovimento);
    Task AddMovimento(TbMovimentoProduto movimentoEstoque);
    Task UpdateMovimento(TbMovimentoProduto movimentoEstoque);
    Task DeleteMovimento(int codigoMovimento);
}
