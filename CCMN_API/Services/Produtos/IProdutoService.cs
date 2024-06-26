using CCMN_API.Models;

namespace CCMN_API.Services.Produtos;

public interface IProdutoService
{
    Task<IEnumerable<TbProduto>> GetProdutos();
    Task<TbProduto> GetProduto(int codigoProduto);
    Task AddProduto(TbProduto produto);
    Task UpdateProduto(TbProduto produto);
    Task DeleteProduto(int codigoProduto);
}
