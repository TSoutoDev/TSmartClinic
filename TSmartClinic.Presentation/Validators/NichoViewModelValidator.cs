using FluentValidation;
using TSmartClinic.Presentation.Models;

namespace TSmartClinic.Presentation.Validators
{
    public class NichoViewModelValidator : AbstractValidator<NichoViewModel>
    {
        public NichoViewModelValidator()
        {

            RuleFor(s => s.NomeNicho)
               .NotEmpty().WithMessage("O Nome é obrigatório.")
               .Length(2, 200).WithMessage("O Nome deve ter entre 2 e 200 caracteres.");

            RuleFor(s => s.Ativo)
              .NotEmpty()
              .WithMessage("A Situação é obrigatória.");
        }
    }
}
