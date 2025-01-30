using System.ComponentModel.DataAnnotations;

namespace CrudZadigAPI.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do Produto � obrigatorio!")]
        [MaxLength(255, ErrorMessage = "O nome do Produto tem que ter no maximo 255 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descri��o do Produto � obrigatorio!")]
        [MaxLength(1000, ErrorMessage = "A descri��o do Produto tem que ter no m�ximo 1000 caracteres")]
        public string? Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O pre�o do Produto � obrigatorio!")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O pre�o do Produto deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A quantidade do Produto � obrigat�ria!")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser zero ou maior.")]
        public int Quantidade { get; set; }
    }
}