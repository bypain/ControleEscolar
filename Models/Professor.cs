using System.ComponentModel.DataAnnotations;

namespace ControleEscolar.Models
{
    public class Professor
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Endereco { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Professor), nameof(ValidateDataNascimento))]
        public DateTime DataNascimento { get; set; }

        
        public static ValidationResult? ValidateDataNascimento(DateTime dataNascimento, ValidationContext context)
        {
            if (dataNascimento > DateTime.Today)
            {
                return new ValidationResult("A data de nascimento não pode ser maior que o dia atual.");
            }
            return ValidationResult.Success;
        }
    }
}
