using System.Text.Json;
using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        // Mapeamento automático, usando AutoMapper

        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
        {
            var produtos = await _uof.ProdutoRepository.GetAllAsync();

            if(!produtos.Any())
                return NotFound("Produtos não encontrado.");

            return Ok(_mapper.Map<IEnumerable<ProdutoDTO>>(produtos));
        }


        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<ProdutoDTO>> Get(int id)
        {
            var produto = await _uof.ProdutoRepository.GetAsync(p=>p.ProdutoID == id);

            if(produto == null)
                return NotFound($"Produto de id '{id}' não encontrado.");

            return Ok(_mapper.Map<ProdutoDTO>(produto));
        }



        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> Post(ProdutoDTO produtoDTO)
        {
            if (produtoDTO == null)
                return BadRequest("Produto é nulo.");

            var produto = _mapper.Map<Produto>(produtoDTO);

            var novoProduto = _uof.ProdutoRepository.Create(produto);

            await _uof.CommitAsync();

            var novoProdutoDTO = _mapper.Map<ProdutoDTO>(novoProduto);

            return new CreatedAtRouteResult("ObterProduto", new { id = novoProdutoDTO.ProdutoID }, novoProdutoDTO);
        }




        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Put(int id, ProdutoDTO produtoDTO)
        {
            if(id != produtoDTO.ProdutoID)
                return BadRequest("Id do produto não confere ao id da rota.");

            if(produtoDTO == null)
                return BadRequest("Produto é nulo.");

            var produto = _mapper.Map<Produto>(produtoDTO);

            var produtoAtualizado = _uof.ProdutoRepository.Update(produto);

            await _uof.CommitAsync();

            return Ok($"Produto de id '{id}' atualizado com sucesso: {_mapper.Map<ProdutoDTO>(produtoAtualizado)}");
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
            var produto = await _uof.ProdutoRepository.GetAsync(p => p.ProdutoID == id);

            if(produto == null)
                return NotFound($"Produto de id '{id}' não encontrado.");

            var produtoDeleteado = _uof.ProdutoRepository.Delete(produto);

            await _uof.CommitAsync();

            return Ok($"Produto de id '{id}' deletado com sucesso: {_mapper.Map<ProdutoDTO>(produtoDeleteado)}");
        }


        [HttpGet("produtos/{id}")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPorCategoria(int id)
        {
            var produtos = await _uof.ProdutoRepository.GetProdutoPorCategoriaAsync(id);

            if (!produtos.Any())
                return NotFound($"Produtos da categoria de id '{id}' não encontrado.");

            return Ok(_mapper.Map<IEnumerable<ProdutoDTO>>(produtos));
        }



        #region Área de paginação
        public ActionResult<IEnumerable<ProdutoDTO>> ObterProdutosPaginacao(PagedList<Produto> produtos)
        {
            var metadado = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadado));

            return Ok(_mapper.Map<IEnumerable<ProdutoDTO>>(produtos));
        }
        
        
        
        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtoParameters)
            => ObterProdutosPaginacao(await _uof.ProdutoRepository.GetProdutosAsync(produtoParameters));




        [HttpGet("filter/price/pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosFiltroPreco([FromQuery] ProdutosFiltroPreco produtosFiltroPreco)
            => ObterProdutosPaginacao(await _uof.ProdutoRepository.GetProdutosFiltroPrecoAsync(produtosFiltroPreco));


        #endregion Fim da área de paginação
    }
}


