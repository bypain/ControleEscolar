using System.ComponentModel.DataAnnotations;

namespace ControleEscolar.Models
{
    public class Aluno
    {
        public Guid Id { get; set; }
        public string? Matricula { get; set; }
        public string? Nome { get; set; }
        public string? Endereco { get; set; }

        [Required(ErrorMessage = "A data de nascimento � obrigat�ria.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Aluno), nameof(ValidateDataNascimento))]
        public DateTime DataNascimento { get; set; }

        public decimal ValorMensalidade { get; set; }

        // Valida��o personalizada
        public static ValidationResult? ValidateDataNascimento(DateTime dataNascimento, ValidationContext context)
        {
            if (dataNascimento > DateTime.Today)
            {
                return new ValidationResult("A data de nascimento n�o pode ser maior que o dia atual.");
            }
            return ValidationResult.Success;
        }
    }
}
