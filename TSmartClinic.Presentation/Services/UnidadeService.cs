using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.Settings;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services
{
    public class UnidadeService : BaseService<BaseFilterViewModel, UnidadeViewModel>, IUnidadeService
    {
        private readonly string? _baseUrlController;

        public UnidadeService(IAccessTokenService accessTokenService, IOptions<UrlApiSettings>? urlApiSettings) : base(accessTokenService, urlApiSettings, "unidades")
        {
            _baseUrlController = $"{urlApiSettings.Value.ApiGateway}/unidades";
        }

        public async Task<List<UnidadeViewModel>> ListarPorCliente(int clienteId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);

                var result = await client.GetAsync($"{_baseUrlController}/dropdown-unidades/{clienteId}");

                if (result.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var content = await result.Content.ReadAsStringAsync();
                    var lista = JsonSerializer.Deserialize<List<UnidadeViewModel>>(content, options);

                    return lista ?? new List<UnidadeViewModel>();
                }

                return new List<UnidadeViewModel>();
            }
        }
    }
}