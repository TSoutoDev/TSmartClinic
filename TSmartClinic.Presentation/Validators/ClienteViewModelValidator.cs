using FluentValidation;
using TSmartClinic.Presentation.Models;

namespace TSmartClinic.Presentation.Validators
{
    public class ClienteViewModelValidator : AbstractValidator<ClienteViewModel>
    {
        public ClienteViewModelValidator()
        {
            RuleFor(s => s.NomeCliente)
               .NotEmpty().WithMessage("O campo é obrigatório.")
               .Length(2, 300).WithMessage("O campo deve ter entre 2 e 300 caracteres.");

            RuleFor(s => s.RazaoSocial)
             .NotEmpty().WithMessage("O campo é obrigatório.")
             .Length(2, 300).WithMessage("O campo deve ter entre 2 e 300 caracteres.");

            RuleFor(s => s.CNPJ)
             .NotEmpty().WithMessage("O é obrigatório.")
             .MaximumLength(18).WithMessage("O campo deve ter entre no máximo 18 caracteres.");

            RuleFor(s => s.EmailContato)
                .NotEmpty().WithMessage("O campo é obrigatório.")
                .Length(2, 200).WithMessage("O campo deve ter entre 2 e 200 caracteres.");

            RuleFor(s => s.Ativo)
                .NotEmpty()
                .WithMessage("O campo é obrigatório.");

            RuleFor(s => s.NichoId)
              .NotEmpty()
              .WithMessage("O campo é obrigatório.");
        }
    }
}
