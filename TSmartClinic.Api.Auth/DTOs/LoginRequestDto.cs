using System.ComponentModel.DataAnnotations;

namespace TSmartClinic.Api.Auth.DTOs
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Informe o email de acesso.")]
        [EmailAddress(ErrorMessage = "Informe um endereço de email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha de acesso.")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$",
        //    ErrorMessage = "Informe a senha forte com pelo menos 6 caracteres.")]
        public string Senha { get; set; }
    }
}
