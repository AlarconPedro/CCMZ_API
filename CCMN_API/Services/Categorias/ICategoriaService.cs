using CCMN_API.Models;

namespace CCMN_API.Services.Categorias;

public interface ICategoriaService
{
    Task<IEnumerable<TbCategoria>> GetCategorias();
    Task<TbCategoria> GetCategoria(int catCodigo);
    Task AddCategoria(TbCategoria categoria);
    Task UpdateCategoria(TbCategoria categoria); 
    Task DeleteCategoria(int catCodigo);
}
