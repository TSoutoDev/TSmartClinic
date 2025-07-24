namespace TSmartClinic.Presentation.Services.Interfaces
{
    public interface IAccessTokenService
    {
        void Salvar(string accessToken);
        string Obter();
        void Excluir();
    }
}
