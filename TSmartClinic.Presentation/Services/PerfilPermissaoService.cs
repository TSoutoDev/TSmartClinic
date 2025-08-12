using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;
using NuGet.Packaging.Signing;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Service;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Models.PermissoesAcesso;
using TSmartClinic.Presentation.Services;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.Settings;
using TSmartClinic.Presentation.ViewModels.Filters;
// Importa os tipos aninhados (ModuloViewModel, FuncionalidadeViewModel, OperacaoViewModel)
using static TSmartClinic.Presentation.Models.PermissoesAcesso.PermissoesViewModel;

public class PerfilPermissaoService : BaseService<BaseFilterViewModel, PermissoesViewModel>, IPerfilPermissaoService
{
    private readonly string? _baseUrlController;

    // construtor preservado
    public PerfilPermissaoService(IAccessTokenService accessTokenService, IOptions<UrlApiSettings>? urlApiSettings) : base(accessTokenService, urlApiSettings, "permissoesAcesso")
    {
        _baseUrlController = $"{urlApiSettings!.Value.ApiGateway}/permissoesAcesso";
    }
    //listar Nichos
    //public async Task<List<PermissoesViewModel>> ListarArvoreModuloPermissoesAsync2()
    //{
    //    using (var client = new HttpClient())
    //    {
    //        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);

    //        //var result = await client.GetAsync($"{_baseUrlController}/permissoes-acesso");

    //        //if (result.IsSuccessStatusCode)
    //        //{
    //        //    var options = new JsonSerializerOptions
    //        //    {
    //        //        PropertyNameCaseInsensitive = true
    //        //    };

    //        //    var content = await result.Content.ReadAsStringAsync();
    //        //    var lista = JsonSerializer.Deserialize<List<PermissoesViewModel>>(content, options);
    //        //    return lista;
    //        //}
    //        //else
    //        //{
    //        //    return new List<PermissoesViewModel>();
    //        //}
    //    }
    //}

    public async Task<List<PermissoesViewModel>> ListarArvoreModuloPermissoesAsync()
    {
        using (var client = new HttpClient())
        {
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);

            var result = await client.GetAsync($"{_baseUrlController}/permissoes-acesso");

            if (result.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var content = await result.Content.ReadAsStringAsync();
                var lista = JsonSerializer.Deserialize<List<PermissoesViewModel>>(content, options);
                return lista;
            }
            else
            {
                return new List<PermissoesViewModel>();
            }
        }
    }

    public async Task<List<ModuloViewModel>> ListarArvorePermissoesAsync()
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);

        var teste = ($"{_baseUrlController}/permissoes-acesso");
        var teste2 = ($"http://localhost:5136/api/permissoesacesso/permissoes-acesso");

        //var resp = await client.GetAsync($"{_baseUrlController}/permissoes-acesso");
        var resp = await client.GetAsync($"{teste2}");

        if (resp.StatusCode == HttpStatusCode.NoContent) return new();

        resp.EnsureSuccessStatusCode();
        await using var stream = await resp.Content.ReadAsStreamAsync();
        var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var lista = await JsonSerializer.DeserializeAsync<List<ModuloViewModel>>(stream, opts);

        return lista ?? new();
    }

    public async Task<List<int>> ObterOperacoesDoPerfilAsync(int perfilId)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);

        // Ex.: GET /permissoesAcesso/perfis/{perfilId}/operacoes  -> [1,5,8,...]
        var teste2 = ($"http://localhost:5136/api/perfis/permissoes-acesso");
        var resp = await client.GetAsync($"{_baseUrlController}/perfis/{perfilId}/operacoes");
        if (resp.StatusCode == HttpStatusCode.NoContent) return new();

        resp.EnsureSuccessStatusCode();
        var json = await resp.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<int>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
    }

    public async Task SalvarOperacoesDoPerfilAsync(int perfilId, IEnumerable<int> operacaoIds)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);

        var payload = JsonSerializer.Serialize(operacaoIds);
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

        // Ex.: PUT /permissoesAcesso/perfis/{perfilId}/operacoes  body: [1,2,3]
        var resp = await client.PutAsync($"{_baseUrlController}/perfis/{perfilId}/operacoes", content);
        resp.EnsureSuccessStatusCode();
    }

}
