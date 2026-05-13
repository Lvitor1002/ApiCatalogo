namespace ApiCatalogo.Pagination
{
    public class ProdutosFiltroPreco : QueryStringParameters
    {
        public decimal? Preco { get; set; }
        public decimal? PrecoCriterio { get; set; }
    }
}
