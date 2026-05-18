using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs
{
    public class CategoriaDTO
    {

        public int CategoriaID { get; set; }

        [Required]
        public string? Nome{ get; set; }

    }
}
