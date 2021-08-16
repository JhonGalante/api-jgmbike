using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_jgmbike.Models
{
    public class Servico
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
        [MaxLength(300)]
        public string ImagemUrl { get; set; }
    }
}
