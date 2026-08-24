using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.Settings;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services
{
    public class ConvenioService : BaseService<BaseFilterViewModel, ConvenioViewModel>, IConvenioService
    {
        private readonly string? _baseUrlController;

        public ConvenioService(IAccessTokenService accessTokenService, IOptions<UrlApiSettings>? urlApiSettings) : base(accessTokenService, urlApiSettings, "convenios")
        {
            _baseUrlController = $"{urlApiSettings.Value.ApiGateway}/convenios";
        }

        public async Task<List<ConvenioViewModel>> ListarConvenios()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =  new AuthenticationHeaderValue("Bearer", this.AccessToken);

                var result = await client.GetAsync($"{_baseUrlController}/listar");

                if (result.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var content = await result.Content.ReadAsStringAsync();

                    var lista = JsonSerializer.Deserialize<List<ConvenioViewModel>>(content, options);

                    return lista ?? new List<ConvenioViewModel>();
                }

                return new List<ConvenioViewModel>();
            }
        }
    }
}