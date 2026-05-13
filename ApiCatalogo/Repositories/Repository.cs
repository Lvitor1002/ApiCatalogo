using System.Linq.Expressions;
using ApiCatalogo.ContextDB;
using ApiCatalogo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CatalogoDbContext _contextDB;

        public Repository(CatalogoDbContext contextDB)
            =>_contextDB = contextDB;


        public T Create(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity), $"{entity} é nula.");

            _contextDB.Set<T>().Add(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity), $"{entity} é nula.");

            _contextDB.Set<T>().Remove(entity);
            return entity;
        }

        public T Update(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity), $"{entity} é nula.");

            _contextDB.Set<T>().Update(entity);
            return entity;
        }


        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
            => await _contextDB.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _contextDB.Set<T>().AsNoTracking().Take(10).ToListAsync();
    }
}
