using ApiCatalogo.Models;
using AutoMapper;

namespace ApiCatalogo.DTOs.Mappings
{
    public class CategoriasDtoMap: Profile
    {
        public CategoriasDtoMap()
            =>CreateMap<Categoria, CategoriaDTO>().ReverseMap();
    }
}
