using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.Settings;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services
{
    public class NichoService : BaseService<BaseFilterViewModel, NichoViewModel>, INichoService
    {
        private readonly string? _baseUrlController;
        public NichoService(IAccessTokenService accessTokenService, IOptions<UrlApiSettings>? urlApiSettings) : base(accessTokenService, urlApiSettings, "nichos")
        {
            _baseUrlController = $"{urlApiSettings.Value.ApiGateway}/nichos";
        }

        //listar Nichos
        public async Task<List<NichoViewModel>> ListarNichos()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);

                var result = await client.GetAsync($"{_baseUrlController}/obter");
       
                if (result.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var content = await result.Content.ReadAsStringAsync();
                    var lista = JsonSerializer.Deserialize<List<NichoViewModel>>(content, options);
                    return lista;
                }
                else
                {
                    return new List<NichoViewModel>();
                }
            }
        }
    }
}
