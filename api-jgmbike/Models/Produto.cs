using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_jgmbike.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
        [Required]
        [MaxLength(300)]
        public string Descricao { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8,2)")]
        public double Preco { get; set; }
        [Required]
        public int Estoque { get; set; }
        [Required]
        [MaxLength(300)]
        public string ImagemUrl { get; set; }
        public CategoriaProduto Categoria { get; set; }
        public int CategoriaId { get; set; }
    }
}
