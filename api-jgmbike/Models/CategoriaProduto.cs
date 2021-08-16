using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api_jgmbike.Models
{
    public class CategoriaProduto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
        [Required]
        [MaxLength(300)]
        public string ImagemUrl { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}
