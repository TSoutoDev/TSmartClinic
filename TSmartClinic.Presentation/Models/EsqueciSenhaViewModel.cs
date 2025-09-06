using System.ComponentModel.DataAnnotations;

namespace TSmartClinic.Presentation.Models
{
    public class EsqueciSenhaViewModel
    {
        [Required(ErrorMessage = "Informe o e-mail.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }
    }
}
