using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs
{
    public class ProdutoDTO
    {
        public int ProdutoID{ get; set; }

        [StringLength(80)]
        public string? Nome{ get; set; }


        [Required]
        [StringLength(300)]
        public string? Descricao{ get; set; }


        public int CategoriaID{ get; set; }
    }
}
