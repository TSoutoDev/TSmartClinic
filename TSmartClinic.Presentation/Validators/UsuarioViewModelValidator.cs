using FluentValidation;
using TSmartClinic.Presentation.Models;

namespace TSmartClinic.Presentation.Validators
{
    public class UsuarioViewModelValidator : AbstractValidator<UsuarioViewModel>
    {
        public UsuarioViewModelValidator()
        {
            RuleFor(s => s.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(2, 300).WithMessage("O nome deve ter entre 2 e 300 caracteres.");

            RuleFor(s => s.Email)
              .NotEmpty().WithMessage("O email é obrigatório.")
              .Length(2, 510).WithMessage("O email deve ter entre 2 e 510 caracteres.");

            RuleForEach(x => x.UsuarioClientePerfil)
                .NotNull().WithMessage("Os perfis de cliente não podem ser nulos.");

            RuleFor(x => x.DataExpiracaoSenha)
               .NotNull().WithMessage("A data de expiração da senha é obrigatória.")
               .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("A data de expiração deve ser hoje ou no futuro.")
               .LessThanOrEqualTo(DateTime.UtcNow.AddYears(2).Date).WithMessage("A data de expiração não pode ultrapassar 2 anos.");

            RuleFor(x => x.ClienteId)
                .GreaterThan(0).WithMessage("O Cliente não pode ser nulo.");

        }
    }
}
