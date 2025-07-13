using AgendaApp.API.DTOs.Requests.Insert;
using FluentValidation;

namespace AgendaApp.API.DTOs.Validator
{
    public class CategoriaValidator : AbstractValidator<CategoriaInsertRequestDTO>
    {
        public CategoriaValidator()
        {
            RuleFor(s => s.Descricao)
           .NotEmpty().WithMessage("O [Nome da descrição] é obrigatória.")
           .Length(2, 100).WithMessage("O [Nome da Ação] deve ter entre 2 e 70 caracteres.");

            RuleFor(s => s.FlagSituacao)
                .NotNull().WithMessage("O Situação da categoria] é obrigatória.");

        }
    }
}
