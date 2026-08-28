using FluentValidation;
using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Shared.DTOs.Validator
{
    public class ConvenioInsertValidator : AbstractValidator<BaseConvenioRequestDTO>
    {
        public ConvenioInsertValidator()
        {
            RuleFor(x => x.NomeConvenio).NotEmpty()
                .WithMessage("O campo [Nome do Convênio] é obrigatório.")
                .Length(2, 300)
                .WithMessage("O campo [Nome do Convênio] deve ter entre 2 e 300 caracteres.");

            RuleFor(x => x.CNPJ).NotEmpty()
                .WithMessage("O campo [CNPJ] é obrigatório.")
                .MaximumLength(18)
                .WithMessage("O campo [CNPJ] deve ter no máximo 18 caracteres.");

            RuleFor(x => x.Telefone)
                .MaximumLength(20)
                .WithMessage("O campo [Telefone] deve ter no máximo 20 caracteres.");

            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("O campo [Email] é obrigatório.")
                .EmailAddress()
                .WithMessage("Informe um email válido.")
                .MaximumLength(200)
                .WithMessage("O campo [Email] deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Ativo)
                .NotNull()
                .WithMessage("O campo [Situação] é obrigatório.");

            RuleFor(x => x.ClienteId)
                .GreaterThan(0)
                .WithMessage("O campo [Clínica] é obrigatório.");
        }
    }
}