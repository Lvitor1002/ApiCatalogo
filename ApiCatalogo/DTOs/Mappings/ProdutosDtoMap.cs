using ApiCatalogo.Models;
using AutoMapper;

namespace ApiCatalogo.DTOs.Mappings
{
    public class ProdutosDtoMap : Profile
    {
        public ProdutosDtoMap()
            => CreateMap<Produto, ProdutoDTO>().ReverseMap();
    }
}
