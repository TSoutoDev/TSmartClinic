using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.Settings;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services
{
    public class PacienteService : BaseService<BaseFilterViewModel, PacienteViewModel>, IPacienteService
    {
       
        private readonly string? _baseUrlController;
        public PacienteService(IAccessTokenService accessTokenService, IOptions<UrlApiSettings>? urlApiSettings) : base(accessTokenService, urlApiSettings, "pacientes")
        {
            _baseUrlController = $"{urlApiSettings.Value.ApiGateway}/pacientes";
        }

        public async Task<List<PacienteViewModel>> ListarPacientes()
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
                    var lista = JsonSerializer.Deserialize<List<PacienteViewModel>>(content, options);
                   
                    return lista;
                }
                else
                {
                    return new List<PacienteViewModel>();
                }
            }
        }
    }
}
