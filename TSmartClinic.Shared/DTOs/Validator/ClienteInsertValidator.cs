using FluentValidation;
using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Shared.DTOs.Validator
{
    public class ClienteInsertValidator : AbstractValidator<BaseClienteRequestDTO>
    {
        public ClienteInsertValidator()
        {
            RuleFor(s => s.NomeCliente)
               .NotEmpty().WithMessage("O campo [Nome Fantasia] é obrigatório.")
               .Length(2, 300).WithMessage("O campo [Nome Fantasia] deve ter entre 2 e 300 caracteres.");

            RuleFor(s => s.RazaoSocial)
             .NotEmpty().WithMessage("O campo [Razão Social] é obrigatório.")
             .Length(2, 300).WithMessage("O campo [Razão Social] deve ter entre 2 e 300 caracteres.");

            RuleFor(s => s.Cnpj)
             .NotEmpty().WithMessage("O campo [CNPJ] é obrigatório.")
             .MaximumLength(18).WithMessage("O campo [CNPJ] deve ter entre no máximo 18 caracteres.");

            RuleFor(s => s.RazaoSocial)
                .NotEmpty().WithMessage("O campo [Email] é obrigatório.")
                .Length(2, 200).WithMessage("O campo [Email] deve ter entre 2 e 200 caracteres.");

            RuleFor(s => s.Ativo)
                .NotEmpty()
                .WithMessage("O campo [Situação] é obrigatória.");

            RuleFor(s => s.NichoId)
              .NotEmpty()
              .WithMessage("O campo [Nicho] é obrigatório.");
        }
    }
}
