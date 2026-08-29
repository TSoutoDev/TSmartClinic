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

                var result = await client.GetAsync($"{_baseUrlController}/listar");

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

        public async Task<PacienteViewModel?> ObterPorId(int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =  new AuthenticationHeaderValue("Bearer", this.AccessToken);

                var result = await client.GetAsync($"{_baseUrlController}/{id}");

                if (!result.IsSuccessStatusCode)
                    return null;

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var content = await result.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<PacienteViewModel>(content, options);
            }
        }


        public async Task<List<PacienteBuscaHeaderViewModel>> BuscarPacientesHeader(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
                return new List<PacienteBuscaHeaderViewModel>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =  new AuthenticationHeaderValue("Bearer", this.AccessToken);

                var url =
                    $"{_baseUrlController}/buscar-header?termo={Uri.EscapeDataString(termo)}";

                var result = await client.GetAsync(url);

                if (!result.IsSuccessStatusCode)
                    return new List<PacienteBuscaHeaderViewModel>();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var content = await result.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<PacienteBuscaHeaderViewModel>>(content, options) ?? new List<PacienteBuscaHeaderViewModel>();
            }
        }

    }
}
