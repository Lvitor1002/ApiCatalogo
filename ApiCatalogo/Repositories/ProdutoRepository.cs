using ApiCatalogo.ContextDB;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repositories.Interfaces;

namespace ApiCatalogo.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(CatalogoDbContext context) : base(context)
        {
        }

        //Métodos específicos(CONCRETOS) para Produto
        public async Task<IEnumerable<Produto>> GetProdutoPorCategoriaAsync(int id)
        {
            var produtos = await GetAllAsync();

            return produtos.Where(p => p.CategoriaID == id);
        }

        public async Task<PagedList<Produto>> GetProdutosAsync(ProdutosParameters produtosParameters)
        {
            var produtos = await GetAllAsync();

            var produtosOrdenados = produtos.OrderBy(p=>p.ProdutoID).AsQueryable();

            return PagedList<Produto>.ToPageList(produtosOrdenados, produtosParameters.PageNumber, produtosParameters.PageSize);
        }

        public async Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroPreco)
        {
            var produtos = await GetAllAsync();

            if(!produtosFiltroPreco.Preco.HasValue && string.IsNullOrEmpty(produtosFiltroPreco.PrecoCriterio))
                return PagedList<Produto>.ToPageList(produtos, produtosFiltroPreco.PageNumber, produtosFiltroPreco.PageSize);
            
            if(produtosFiltroPreco.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
                produtos = produtos.Where(p=>(decimal)p.Preco > produtosFiltroPreco.Preco.Value).OrderBy(p=>p.Preco);
            
            else if (produtosFiltroPreco.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
                produtos = produtos.Where(p => (decimal)p.Preco < produtosFiltroPreco.Preco.Value).OrderBy(p => p.Preco);

            else if (produtosFiltroPreco.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
                produtos = produtos.Where(p => (decimal)p.Preco == produtosFiltroPreco.Preco.Value).OrderBy(p => p.Preco);

            return PagedList<Produto>.ToPageList(produtos.AsQueryable(), produtosFiltroPreco.PageNumber, produtosFiltroPreco.PageSize);
        }
    }
}
