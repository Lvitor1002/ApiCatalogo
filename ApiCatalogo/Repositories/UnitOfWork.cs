using ApiCatalogo.ContextDB;
using ApiCatalogo.Repositories.Interfaces;

namespace ApiCatalogo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        public IProdutoRepository ProdutoRepository {get => _produtoRepository ?? new ProdutoRepository(ContextDb);}
        public ICategoriaRepository CategoriaRepository {get => _categoriaRepository ?? new CategoriaRepository(ContextDb);}
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        public CatalogoDbContext ContextDb;



        public UnitOfWork(CatalogoDbContext contextDb)
            => ContextDb = contextDb;

        public async Task CommitAsync()
            => await ContextDb.SaveChangesAsync();

        public void Dispose()
            => ContextDb.Dispose();
    }
}
