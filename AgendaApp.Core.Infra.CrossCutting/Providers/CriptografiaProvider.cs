using AgendaApp.Core.Domain.Interfaces.Providers;
using AgendaApp.Core.Infra.CrossCutting.Criptografia;

namespace AgendaApp.Core.Infra.CrossCutting.Providers
{
    public class CriptografiaProvider : ICriptografiaProvider
    {
        private const string CHAVE_CRIPTOGRAFIA = "c2lkZWluZm9fcGVvcGxlbmV0X3NhdWRlX29jdXBhY2lvbmFs";
        private readonly DefaultCrypto _objCrypt;

        public CriptografiaProvider()
        {
            _objCrypt = new DefaultCrypto();
        }

        public string Criptografar(string textoParaCriptografar)
        {
            return _objCrypt.Encrypt(textoParaCriptografar, CHAVE_CRIPTOGRAFIA);
        }

        public string Decriptografar(string textoCriptografado)
        {
            return _objCrypt.Decrypt(textoCriptografado, CHAVE_CRIPTOGRAFIA);
        }
    }
}
