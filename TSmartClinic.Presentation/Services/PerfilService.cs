using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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
        public PerfilService(IAccessTokenService accessTokenService, IOptions<UrlApiSettings>? urlApiSettings) : base(accessTokenService, urlApiSettings, "perfis")
        {
        }


        //listar Nichos
        public  async Task<List<NichoViewModel>> ListarNichos()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);
                //var novaUrl = _baseUrlController.Replace("perfis", "");
                //var result = await client.GetAsync($"{novaUrl}");
                    var result = await client.GetAsync("http://localhost:5049/api/nichos/listar");


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
