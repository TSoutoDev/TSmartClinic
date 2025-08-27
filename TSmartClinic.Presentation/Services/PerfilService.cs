using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.Settings;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services
{
    public class PerfilService : BaseService<BaseFilterViewModel, PerfilViewModel>, IPerfilService
    {
        private readonly string? _baseUrlController;
        public PerfilService(IAccessTokenService accessTokenService, IOptions<UrlApiSettings>? urlApiSettings) : base(accessTokenService, urlApiSettings, "perfis")
        {
            _baseUrlController = $"{urlApiSettings.Value.ApiGateway}/perfis";
        }

        //listar Clientes
        public async Task<List<PerfilViewModel>> ListarPerfilPorCliente(int clienteId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);

                var result = await client.GetAsync($"{_baseUrlController}/dropdown-perfil/{clienteId}");

                if (result.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var content = await result.Content.ReadAsStringAsync();
                    var lista = JsonSerializer.Deserialize<List<PerfilViewModel>>(content, options);
                    return lista;
                }
                else
                {
                    return new List<PerfilViewModel>();
                }
            }
        }
    }
}
