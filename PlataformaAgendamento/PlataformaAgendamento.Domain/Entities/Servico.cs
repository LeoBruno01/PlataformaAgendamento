using System.ComponentModel.DataAnnotations;

namespace PlataformaAgendamento.Domain.Entities
{
    public class Servico
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do serviço é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 2000.00, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Required]
        [Range(5, 480, ErrorMessage = "A duração deve ser de pelo menos 5 minutos.")]
        public int DuracaoEmMinutos { get; set; }
    }
}
