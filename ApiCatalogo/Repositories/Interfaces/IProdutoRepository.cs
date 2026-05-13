using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        //Métodos específicos para Produto, além dos métodos CRUD já existentes na interface genérica IRepository<T>

    }
}
