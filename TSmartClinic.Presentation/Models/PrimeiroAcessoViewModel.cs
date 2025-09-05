using System.ComponentModel.DataAnnotations;

namespace TSmartClinic.Presentation.Models
{
    public class PrimeiroAcessoViewModel : BaseViewModel
    {
        public int IdUsuario { get; set; }

        [Required]
        public string Token { get; set; } = default!;   // 👈 novo

        // Pode manter se usar em outro fluxo; para o link com JWT não precisa:
         public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string NovaSenha { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        [Compare("NovaSenha", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmarSenha { get; set; } = default!;
    }
}
