using ApiCatalogo.ContextDB;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repositories.Interfaces;

namespace ApiCatalogo.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(CatalogoDbContext contextDB) : base(contextDB)
        {
        }


        //Métodos específicos(CONCRETOS) para Categoria
        public async Task<PagedList<Categoria>> GetCategoriaAsync(CategoriasParameters categoriasParameters)
        {
            var categorias = await GetAllAsync();

            var categoriasOrdenadas = categorias.OrderBy(c=>c.CategoriaID).AsQueryable();

            return PagedList<Categoria>.ToPageList(categoriasOrdenadas, categoriasParameters.PageNumber, categoriasParameters.PageSize); ;
        }

        public async Task<PagedList<Categoria>> GetCategoriaFiltroNomeAsync(CategoriasFiltroNome categoriasFiltroNome)
        {
            var categorias = await GetAllAsync();

            if(!string.IsNullOrEmpty(categoriasFiltroNome.Nome))
                categorias = categorias.Where(c=>c.Nome.ToLower().Contains(categoriasFiltroNome.Nome.ToLower()));
            
            return PagedList<Categoria>.ToPageList(categorias.AsQueryable(), categoriasFiltroNome.PageNumber, categoriasFiltroNome.PageSize);
        }
    }
}
