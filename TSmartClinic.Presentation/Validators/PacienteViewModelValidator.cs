using FluentValidation;
using TSmartClinic.Presentation.Models;

namespace TSmartClinic.Presentation.Validators
{
    public class PacienteViewModelValidator : AbstractValidator<PacienteViewModel>
    {
        public PacienteViewModelValidator()
        {
            RuleFor(s => s.NomePaciente).NotEmpty().WithMessage("O campo é obrigatório.").Length(2, 300).WithMessage("O campo deve ter entre 2 e 300 caracteres.");
            RuleFor(s => s.DataNascimento).NotEmpty().WithMessage("O campo é obrigatório.");
            RuleFor(s => s.CPF).NotEmpty().WithMessage("O campo é obrigatório.").MaximumLength(14).WithMessage("O campo deve ter no máximo 14 caracteres.");
            RuleFor(s => s.Telefone).MaximumLength(20).WithMessage("O campo deve ter no máximo 20 caracteres.");
            RuleFor(s => s.Email).NotEmpty().WithMessage("O campo é obrigatório.").EmailAddress().WithMessage("Informe um e-mail válido.")
                .MaximumLength(100).WithMessage("O campo deve ter no máximo 100 caracteres.");
            RuleFor(s => s.Observacoes).MaximumLength(8000).WithMessage("O campo deve ter no máximo 8000 caracteres.");
            RuleFor(s => s.Ativo).NotNull().WithMessage("O campo é obrigatório.");
            RuleFor(s => s.ConvenioId).NotEmpty().WithMessage("O campo é obrigatório.");
        }
    }
}