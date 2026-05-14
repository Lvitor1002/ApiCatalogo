using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        //Métodos específicos para Produto, além dos métodos CRUD já existentes na interface genérica IRepository<T>
        Task<IEnumerable<Produto>> GetProdutoPorCategoriaAsync(int id);

        Task<PagedList<Produto>> GetProdutosAsync(ProdutosParameters produtosParameters);

        Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroPreco);
    }
}
