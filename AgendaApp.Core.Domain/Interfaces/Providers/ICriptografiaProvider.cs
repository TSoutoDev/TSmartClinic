namespace AgendaApp.Core.Domain.Interfaces.Providers
{
    public interface ICriptografiaProvider
    {
        string Criptografar(string textoParaCriptografar);
        string Decriptografar(string textoCriptografado);
    }
}
