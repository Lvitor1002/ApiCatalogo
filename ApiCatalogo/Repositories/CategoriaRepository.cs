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
        public Task<Categoria> GetCategoriaAsync(CategoriasParameters categoriasParameters)
        {
            throw new NotImplementedException();
        }

        public Task<Categoria> GetCategoriaFiltroNomeAsync(CategoriasFiltroNome categoriasFiltroNome)
        {
            throw new NotImplementedException();
        }
    }
}
