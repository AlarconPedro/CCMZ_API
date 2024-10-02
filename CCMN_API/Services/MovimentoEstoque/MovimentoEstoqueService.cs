using CCMN_API.Models;
using CCMN_API.Models.Painel.Estoque;
using CCMN_API.Services.Produtos;
using CCMZ_API;
using Microsoft.EntityFrameworkCore;

namespace CCMN_API.Services.MovimentoProdutos
{
    public class MovimentoEstoqueService : IMovimentoEstoqueServicecs
    {
        private readonly CCMNContext _context;

        public MovimentoEstoqueService(CCMNContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovimentoEstoque>> GetMovimentosEstoque()
        {
            return await _context.TbMovimentoProdutos.Select(x => new MovimentoEstoque
            {
               MovCodigo = x.MovCodigo,
               MovData = x.MovData,
               MovTipo = x.MovTipo,
               MovQuantidade = x.MovQuantidade,
               ProCodigo = x.ProCodigo,
               ProNome = x.ProCodigoNavigation.ProNome,
            }).ToListAsync();
        }
        public async Task<TbMovimentoProduto> GetMovimentoEstoque(int codigoMovimento)
        {
            return await _context.TbMovimentoProdutos.FirstOrDefaultAsync(m => m.MovCodigo == codigoMovimento);
        }
        public async Task<(int, string)> AddMovimento(TbMovimentoProduto movimentoEstoque)
        {
            var produto = await _context.TbProdutos.FirstOrDefaultAsync(p => p.ProCodigo == movimentoEstoque.ProCodigo);
            if (produto.ProQuantidade >= movimentoEstoque.MovQuantidade) {
                if (movimentoEstoque.MovTipo.Equals("S"))
                    produto.ProQuantidade -= movimentoEstoque.MovQuantidade;
                else
                    produto.ProQuantidade += movimentoEstoque.MovQuantidade;
                var ultimoMovimento = _context.TbMovimentoProdutos.FirstOrDefault();
                if (ultimoMovimento != null)
                {
                    movimentoEstoque.MovCodigo = await _context.TbMovimentoProdutos.MaxAsync(m => m.MovCodigo) + 1;
                }
                else
                {
                    movimentoEstoque.MovCodigo = 1;
                }
                _context.TbMovimentoProdutos.Add(movimentoEstoque);
                await _context.SaveChangesAsync();

                _context.TbProdutos.Update(produto);
                await _context.SaveChangesAsync();

                return (200, "Movimento de estoque Efetuado !");
            } else
            {
                return (400 ,"Quantidade insuficiente em estoque !");
            }
        }
        public async Task UpdateMovimento(TbMovimentoProduto movimentoEstoque)
        {
            _context.TbMovimentoProdutos.Update(movimentoEstoque);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteMovimento(int codigoMovimento)
        {
            _context.TbMovimentoProdutos.Where(m => m.MovCodigo == codigoMovimento).ExecuteDelete();
            await _context.SaveChangesAsync();
        }
    }
}
