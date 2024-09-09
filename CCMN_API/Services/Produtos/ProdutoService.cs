using CCMN_API.Models;
using CCMZ_API;
using Microsoft.EntityFrameworkCore;

namespace CCMN_API.Services.Produtos;

public class ProdutoService : IProdutoService
{
    private readonly CCMNContext _context;

    public ProdutoService(CCMNContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TbProduto>> GetProdutos()
    {
        return await _context.TbProdutos.ToListAsync();
    }

    public async Task<TbProduto> GetProduto(int codigoProduto)
    {
        return await _context.TbProdutos.FindAsync(codigoProduto);
    }

    public async Task AddProduto(TbProduto produto)
    {
        var produtoExistente = await _context.TbProdutos.FirstOrDefaultAsync();
        if (produtoExistente != null)
        {
            produto.ProCodigo = await _context.TbProdutos.MaxAsync(p => p.ProCodigo) + 1;
        } else
        {
            produto.ProCodigo = 1;
        }
        _context.TbProdutos.Add(produto);
        await _context.SaveChangesAsync();
        await RegistraMovimento("E", produto);
    }

    public async Task UpdateProduto(TbProduto produto)
    {
        _context.TbProdutos.Update(produto);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteProduto(int codigoProduto)
    {
        var retorno = _context.TbProdutos.Find(codigoProduto);
        if (retorno != null) {
            _context.TbProdutos.Remove(retorno);
            await _context.SaveChangesAsync();
            //await RegistraMovimento("S", retorno);
        }        
    }

    private async Task RegistraMovimento(string tipoMovimento, TbProduto? produto)
    {
        var existe = await _context.TbMovimentoProdutos.FirstOrDefaultAsync();
        if (existe == null)
        {
            _context.TbMovimentoProdutos.Add(new TbMovimentoProduto
            {
                MovCodigo = 1,
                MovTipo = tipoMovimento,
                MovData = DateTime.Now,
                ProCodigo = produto.ProCodigo,
                MovQuantidade = produto.ProQuantidade,
                ProCodigoNavigation = produto
            });
        } else {
            _context.TbMovimentoProdutos.Add(new TbMovimentoProduto
            {
                MovCodigo = await _context.TbMovimentoProdutos.MaxAsync(m => m.MovCodigo) + 1,
                MovTipo = tipoMovimento,
                MovData = DateTime.Now,
                ProCodigo = produto.ProCodigo,
                MovQuantidade = produto.ProQuantidade,
                ProCodigoNavigation = produto
            });
        }
        
        await _context.SaveChangesAsync();
    }
}
