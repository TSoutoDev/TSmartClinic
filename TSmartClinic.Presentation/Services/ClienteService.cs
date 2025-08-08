using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.Settings;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services
{
    public class ClienteService : BaseService<BaseFilterViewModel, ClienteViewModel>, IClienteService
    {
        private readonly string? _baseUrlController;
        public ClienteService(IAccessTokenService accessTokenService, IOptions<UrlApiSettings>? urlApiSettings) : base(accessTokenService, urlApiSettings, "clientes")
        {
            _baseUrlController = $"{urlApiSettings.Value.ApiGateway}/clientes";
        }

        //listar Clientes
        public async Task<List<ClienteViewModel>> ListarClientes()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);

                var result = await client.GetAsync($"{_baseUrlController}/dropdown-clientes");

                if (result.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var content = await result.Content.ReadAsStringAsync();
                    var lista = JsonSerializer.Deserialize<List<ClienteViewModel>>(content, options);
                    return lista;
                }
                else
                {
                    return new List<ClienteViewModel>();
                }
            }
        }
    }

}
