using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
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
    private readonly ILogger<PerfilPermissaoService> _logger;

    public PerfilPermissaoService(ILogger<PerfilPermissaoService> logger, IAccessTokenService accessTokenService, IOptions<UrlApiSettings>? urlApiSettings) : base(accessTokenService, urlApiSettings, "permissoesAcesso")
    {
        _baseUrlController = $"{urlApiSettings!.Value.ApiGateway}/permissoesAcesso";
        _logger = logger;
    }
    
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

        var url = $"http://localhost:5136/api/permissoesacesso/permissoes-acesso/{perfilId}";
        var resp = await client.GetAsync(url);
        if (resp.StatusCode == HttpStatusCode.NoContent) return new();

        resp.EnsureSuccessStatusCode();
        var json = await resp.Content.ReadAsStringAsync();

        _logger.LogInformation($"JSON recebido: {json}");

        var modulos = JsonSerializer.Deserialize<List<ModuloViewModel>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new List<ModuloViewModel>();

        // Extrair todos os IDs de operações em todos os módulos e funcionalidades
        var operacaoIds = modulos
            .SelectMany(m => m.Funcionalidades ?? new List<FuncionalidadeViewModel>())
            .SelectMany(f => f.Operacoes ?? new List<OperacaoViewModel>())
            .Select(o => o.Id)
            .Distinct()
            .ToList();

        return operacaoIds;
    }


    public async Task SalvarOperacoesDoPerfilAsync(int perfilId, IEnumerable<int> operacaoIds)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);

        var payload = JsonSerializer.Serialize(operacaoIds);
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

        // Ex.: PUT /permissoesAcesso/perfis/{perfilId}/operacoes  body: [1,2,3]
        // var resp = await client.PutAsync($"{_baseUrlController}/perfis/{perfilId}/operacoes", content);
         var resp = await client.PutAsync($"http://localhost:5136/api/permissoesacesso/permissoes-acesso/{perfilId}/operacoes", content);
        resp.EnsureSuccessStatusCode();
    }

}
