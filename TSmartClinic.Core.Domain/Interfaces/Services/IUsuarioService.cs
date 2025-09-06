using TSmartClinic.Core.Domain.Entities;
namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IUsuarioService : IBaseService<Usuario>
    {
        Usuario ObterPorEmail(string email);
        void Bloquear(int id);
        List<string> ObterPermissaoUsuario(int usuarioId, List<Cliente> clinicasUsuario);
        void DefinirSenha(string token, string novaSenha);//extrai o id do usuário e só então persiste. Serviço recebe TOKEN e faz toda validação,
        string GerarTokenResetSenha(string email);//  novo serviço: gerar token de reset
    }
}
