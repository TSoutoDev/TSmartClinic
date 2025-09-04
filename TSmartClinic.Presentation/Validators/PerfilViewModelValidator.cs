using FluentValidation;
using TSmartClinic.Presentation.Models;

namespace TSmartClinic.Presentation.Validators
{
    public class PerfilViewModelValidator : AbstractValidator<PerfilViewModel>
    {
        public PerfilViewModelValidator()
        {
            RuleFor(s => s.ValidadeDias)
                .NotNull().WithMessage("A validade é obrigatória.")
                .InclusiveBetween(0, 9999)
                .WithMessage("O campo ValidadeDias deve ter no máximo 4 dígitos (0–9999).");

            RuleFor(s => s.ErrosSenha)
                .NotNull().WithMessage("A quantidade de erros é obrigatória.")
                .InclusiveBetween(0, 9999)
                .WithMessage("O campo ErrosSenha deve ter no máximo 4 dígitos (0–9999).");

            RuleFor(s => s.NomePerfil)
                .NotEmpty().WithMessage("O Nome é obrigatório.")
                .MaximumLength(150).WithMessage("O Nome Perfil deve ter até 150 caracteres.");

            // Situação → se for bool?, valida seleção
            RuleFor(s => s.Ativo)
                .NotNull().WithMessage("A Situação é obrigatória.");

            RuleFor(s => s.ClienteId)
               .NotEmpty()
               .WithMessage("O Cliente é obrigatório.");

            // Operações selecionadas (checkboxes)
            RuleFor(x => x.SelectedOperacaoIds)
                .NotNull().WithMessage("Selecione ao menos uma permissão.")
                .Must(ids => ids.Any())
                .WithMessage("Selecione ao menos uma permissão.");
        }
    }
}
