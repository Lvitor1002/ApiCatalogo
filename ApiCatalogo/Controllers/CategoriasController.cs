using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly Mapper _mapper;

        public CategoriasController(IUnitOfWork uof, Mapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            var categorias = await _uof.CategoriaRepository.GetAllAsync();
            
            if(!categorias.Any())
                return NotFound("Categorias não encontradas.");

            return Ok(_mapper.Map<IEnumerable<CategoriaDTO>>(categorias));
        }



        [HttpGet("{id:int}",Name = "ObterCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            var categoria = await _uof.CategoriaRepository.GetAsync(c=>c.CategoriaID == id);
            
            if(categoria == null)
                return NotFound($"Categoria de id '{id}' não encontrada.");

            return Ok(_mapper.Map<CategoriaDTO>(categoria));
        }



        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> Post(CategoriaDTO categoriaDTO)
        {
            if(categoriaDTO == null)
                return BadRequest("Categoria é nula.");

            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            var novaCategoria = _uof.CategoriaRepository.Create(categoria);

            await _uof.CommitAsync();

            var novaCategoriaDTO = _mapper.Map<CategoriaDTO>(novaCategoria);

            return new CreatedAtRouteResult("ObterCategoria", new { id = novaCategoriaDTO.CategoriaID }, novaCategoriaDTO);
        }



        [HttpPut("{id}:int")]
        public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO categoriaDTO)
        {

            if(id != categoriaDTO.CategoriaID)
                return BadRequest("Categoria não encontrada.");

            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            _uof.CategoriaRepository.Update(categoria);

            await _uof.CommitAsync();

            return Ok($"Categoria de id '{id}' atualizada com sucesso: {_mapper.Map<CategoriaDTO>(categoria)}");
        }



        [HttpDelete("{id}:int")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            var categoria = await _uof.CategoriaRepository.GetAsync(c => c.CategoriaID == id);

            if(categoria == null)
                return NotFound($"Categoria de id '{id}' não encontrada.");

            var categoriaDeletada = _uof.CategoriaRepository.Delete(categoria);
            
            await _uof.CommitAsync();

            return Ok($"Categoria de id '{id}' deletada com sucesso: {_mapper.Map<CategoriaDTO>(categoriaDeletada)}");
        }


        #region Área de paginação
        private ActionResult<IEnumerable<CategoriaDTO>> ObterCategoriasPaginacao(PagedList<Categoria> categorias)
        {
            var metadado = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadado));
            
            return Ok(_mapper.Map<IEnumerable<CategoriaDTO>>(categorias));
        }
        
        
        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters categoriasParameters)
        {
            var categorias = await _uof.CategoriaRepository.GetCategoriaAsync(categoriasParameters);
            return ObterCategoriasPaginacao(categorias);
        }



        [HttpGet("filter/nome/pagination")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasPorNomePaginacao([FromQuery] CategoriasFiltroNome categoriasFiltroNome)
        {
            var categorias = await _uof.CategoriaRepository.GetCategoriaFiltroNomeAsync(categoriasFiltroNome);
            return ObterCategoriasPaginacao(categorias);
        }


        #endregion Fim da área de paginação
    }
}
