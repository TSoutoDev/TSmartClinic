using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Core.Infra.CrossCutting.Criptografia;

namespace TSmartClinic.Core.Infra.CrossCutting.Providers
{
    public class CriptografiaProvider : ICriptografiaProvider
    {
        private const string CHAVE_CRIPTOGRAFIA = "85C7989FTSmartClinicApplication5B8D2AE68DFC";
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
