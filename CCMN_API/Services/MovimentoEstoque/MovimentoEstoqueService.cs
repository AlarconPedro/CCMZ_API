using CCMN_API.Models;
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

        public async Task<IEnumerable<TbMovimentoProduto>> GetMovimentosEstoque()
        {
            return await _context.TbMovimentoProdutos.ToListAsync();
        }
        public async Task<TbMovimentoProduto> GetMovimentoEstoque(int codigoMovimento)
        {
            return await _context.TbMovimentoProdutos.FirstOrDefaultAsync(m => m.MovCodigo == codigoMovimento);
        }
        public async Task AddMovimento(TbMovimentoProduto movimentoEstoque)
        {
            _context.TbMovimentoProdutos.Add(movimentoEstoque);
            await _context.SaveChangesAsync();
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
