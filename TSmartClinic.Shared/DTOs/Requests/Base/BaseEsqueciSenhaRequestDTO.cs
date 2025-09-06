using System.ComponentModel.DataAnnotations;

namespace TSmartClinic.Shared.DTOs.Requests.Base
{
    public class BaseEsqueciSenhaRequestDTO
    {
        [Required(ErrorMessage = "Informe o e-mail.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }
    }
}
