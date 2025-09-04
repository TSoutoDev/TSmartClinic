using FluentValidation;
using TSmartClinic.Shared.DTOs.Requests.Update;

namespace TSmartClinic.Shared.DTOs.Validator.Update
{
    public class UsuarioUpdateValidator : AbstractValidator<UsuarioUpdateRequestDTO>
    {
        public UsuarioUpdateValidator()
        {
            RuleFor(s => s.Nome)
             .NotEmpty().WithMessage("O campo [nome] é obrigatório.")
             .Length(2, 300).WithMessage("O [nome] deve ter entre 2 e 300 caracteres.");

            RuleFor(s => s.Senha)
              .NotEmpty().WithMessage("O campo [senha] é obrigatório.")
              .Length(2, 1020).WithMessage("O [senha] deve ter entre 2 e 1020 caracteres.");

            RuleFor(s => s.Email)
              .NotEmpty().WithMessage("O campo [email] é obrigatório.")
              .Length(2, 510).WithMessage("O [email] deve ter entre 2 e 510 caracteres.");

            RuleFor(x => x.DataExpiracaoSenha)
               .NotNull().WithMessage("A data de expiração da senha é obrigatória.")
               .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("A data de expiração deve ser hoje ou no futuro.")
               .LessThanOrEqualTo(DateTime.UtcNow.AddYears(2).Date).WithMessage("A data de expiração não pode ultrapassar 2 anos.");

            RuleFor(x => x.TipoUsuario)
                .NotNull().WithMessage("O tipo de usuário é obrigatório.")
                .Must(c => c == 'C' || c == 'M')
                .WithMessage("O tipo de usuário deve ser 'C', 'M'.");

            // FlagBloqueado: não precisa de validação específica, é bool e já tem default
            RuleFor(x => x.FlagBloqueado)
                .NotNull().WithMessage("O status de bloqueio precisa ser definido.");

            // Ativo: booleano obrigatório
            RuleFor(x => x.Ativo)
                .NotNull().WithMessage("O status de ativo precisa ser definido.");

            // ClienteId: deve ser maior que zero
            RuleFor(x => x.ClienteId)
                .GreaterThan(0).WithMessage("O ClienteId deve ser maior que zero.");

            // LoginInclusao: pode ser nulo, mas se informado, não pode estar vazio
            RuleFor(x => x.LoginAlteracao)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.LoginAlteracao))
                .WithMessage("O login de inclusão não pode estar vazio quando informado.");

            // DataInclusao: pode ser nula, mas se informado, não pode ser no futuro
            RuleFor(x => x.DataAlteracao)
                .Must(data => data == null || data <= DateTime.UtcNow)
                .WithMessage("A data de inclusão não pode ser futura.");

            // UsuarioClientePerfil: lista opcional, mas se existir, não pode conter nulos
            RuleForEach(x => x.UsuarioClientePerfil)
                .NotNull().WithMessage("Os perfis de cliente não podem ser nulos.");
        }
    }
}
