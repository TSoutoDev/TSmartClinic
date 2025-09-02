using System.ComponentModel.DataAnnotations;

namespace TSmartClinic.Shared.DTOs.Requests.Base
{
    public class BaseUsuarioPrimeiroAcessoRequestoDTO
    {
        [Required]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string NovaSenha { get; set; } = default!;
    }
}
