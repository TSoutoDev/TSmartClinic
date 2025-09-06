using System.ComponentModel.DataAnnotations;

namespace TSmartClinic.Presentation.Models
{
    public class ResetSenhaViewModel : BaseViewModel
    {
        public string Token { get; set; }

        [Required(ErrorMessage = "Informe a nova senha.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string NovaSenha { get; set; }

        [Compare(nameof(NovaSenha), ErrorMessage = "As senhas não conferem.")]
        public string ConfirmarSenha { get; set; }
    }
}
