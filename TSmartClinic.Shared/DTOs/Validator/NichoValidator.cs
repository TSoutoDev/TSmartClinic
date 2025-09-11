using FluentValidation;
using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Shared.DTOs.Validator
{
    public class NichoValidator : AbstractValidator<BaseNichoRequestDTO>
    {
        public NichoValidator()
        {

            RuleFor(s => s.NomeNicho)
               .NotEmpty().WithMessage("O campo [Nome Nicho] é obrigatório.")
               .Length(2, 200).WithMessage("O campo [Nome Nicho] deve ter entre 2 e 200 caracteres.");

            RuleFor(s => s.Ativo)
              .NotEmpty()
              .WithMessage("O campo [Situação] é obrigatória.");
        }
    }
}
