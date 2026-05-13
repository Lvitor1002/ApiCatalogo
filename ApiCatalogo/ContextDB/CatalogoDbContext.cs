using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.ContextDB
{
    public class CatalogoDbContext : DbContext
    {
        public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Produto>? Produtos { get; set; }
        public DbSet<Models.Categoria>? Categorias { get; set; }
    }
}
