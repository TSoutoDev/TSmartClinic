using FluentValidation;
using TSmartClinic.Shared.DTOs.Requests.Insert;

namespace TSmartClinic.Shared.DTOs.Requests
{
    public class TarefaValidator : AbstractValidator<TarefaInsertRequestDTO>
    {
        public TarefaValidator()
        {
            RuleFor(s => s.Nome)
                .NotEmpty().WithMessage("O [Nome] é obrigatória.")
                .Length(2, 250).WithMessage("O [Nome] deve ter entre 2 e 70 caracteres.");

            RuleFor(s => s.FlagSituacao)
                .NotNull().WithMessage("A Situação é obrigatória.");

            RuleFor(s => s.Data)
                .NotNull().WithMessage("A [data] é obrigatória.");
            RuleFor(s => s.Hora)
             .NotNull().WithMessage("A [Hora] é obrigatória.");

            RuleFor(s => s.Prioridade)
             .NotNull().WithMessage("A [Prioridade] é obrigatória.");

            RuleFor(s => s.CategoriaId)
            .NotNull().WithMessage("A [Categoria] é obrigatória.");

        }

    }
}
