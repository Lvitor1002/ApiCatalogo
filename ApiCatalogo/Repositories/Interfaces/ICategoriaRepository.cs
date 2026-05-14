using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repositories.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        //Métodos específicos para Categoria, além dos métodos CRUD já existentes na interface genérica IRepository<T>
        Task<PagedList<Categoria>> GetCategoriaAsync(CategoriasParameters categoriasParameters);
        Task<PagedList<Categoria>> GetCategoriaFiltroNomeAsync(CategoriasFiltroNome categoriasFiltroNome);
    }
}
