using FluentValidation;
using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Shared.DTOs.Validator.Insert
{
    public class PerfilInsertValidator : AbstractValidator<BasePerfilRequestDTO>
    {
        public PerfilInsertValidator()
        {
            RuleFor(s => s.ValidadeDias)
            .InclusiveBetween(0, 9999)
            .WithMessage("O campo ValidadeDias deve ter no máximo 4 dígitos (0–9999).");

            RuleFor(s => s.ErrosSenha)
                .InclusiveBetween(0, 9999)
                .WithMessage("O campo ErrosSenha deve ter no máximo 4 dígitos (0–9999).");

            RuleFor(s => s.NomePerfil)
                .NotEmpty()
                .MaximumLength(150)
                .WithMessage("O campo NomePerfil deve ter até 150 caracteres.");

            RuleFor(s => s.ClienteId)
                .NotEmpty()
                .WithMessage("O campo ClienteId é obrigatório.");

            RuleFor(s => s.NichoId)
                .NotEmpty()
                .WithMessage("O campo Nicho obrigatório.");
        }
    }
}
