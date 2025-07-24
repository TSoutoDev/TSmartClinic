using Microsoft.Extensions.Options;
using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Core.Infra.CrossCutting.Criptografia;

namespace TSmartClinic.Core.Infra.CrossCutting.Providers
{
    public class CriptografiaProvider : ICriptografiaProvider
    {
        //private const string CHAVE_CRIPTOGRAFIA = "85C7989FTSmartClinicApplication5B8D2AE68DFC";
        private readonly string _chave;
        private readonly DefaultCrypto _defaultCrypto;

        public CriptografiaProvider(IOptions<CryptoSettings> options)
        {
            _chave = options.Value.Chave
                    ?? throw new ArgumentNullException(nameof(options.Value.Chave));
            _defaultCrypto = new DefaultCrypto();
        }

        public string Criptografar(string textoParaCriptografar)
        {
            return _defaultCrypto.Encrypt(textoParaCriptografar, _chave);
        }

        public string Decriptografar(string textoCriptografado)
        {
            return _defaultCrypto.Decrypt(textoCriptografado, _chave);
        }
    }
}
