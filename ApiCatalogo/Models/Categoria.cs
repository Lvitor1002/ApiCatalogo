using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int CategoriaID{ get; set; }



        [Required]
        [StringLength(80)]
        public string? Nome{ get; set; }



        [JsonIgnore] // Para ignorar a propriedade Produtos durante a serialização JSON
        public ICollection<Produto> ListaProdutos{ get; set; }



        public Categoria()
            => ListaProdutos = new List<Produto>();

    }
}
