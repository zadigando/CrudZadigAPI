using System.ComponentModel.DataAnnotations;

namespace CrudZadigAPI.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do Produto é obrigatorio!")]
        [MaxLength(255, ErrorMessage = "O nome do Produto tem que ter no maximo 255 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição do Produto é obrigatorio!")]
        [MaxLength(1000, ErrorMessage = "A descrição do Produto tem que ter no máximo 1000 caracteres")]
        public string? Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço do Produto é obrigatorio!")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço do Produto deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A quantidade do Produto é obrigatória!")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser zero ou maior.")]
        public int Quantidade { get; set; }
    }
}