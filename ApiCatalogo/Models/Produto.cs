using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoID{ get; set; }



        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(80)]
        public string? Nome{ get; set; }



        [Required]
        [StringLength(300)]
        public string? Descricao{ get; set; }



        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public double Preco { get; set; }



        public double Estoque{ get; set; }
        public DateTime DataCadastro{ get; set; }
        public int CategoriaID{ get; set; }



        [JsonIgnore] // Para ignorar a propriedade Categoria durante a serialização JSON
        public Categoria? Categoria{ get; set; }
    }
}
