using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.Settings;

namespace TSmartClinic.Presentation.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ICriptografiaProvider _criptografiaProvider;

        protected readonly string _baseUrlController;
        private readonly string _accessToken;
        private readonly string _apiGateway;

        protected string AccessToken
        {
            get{return _accessToken;}
        }

        public AutenticacaoService(IAccessTokenService accessTokenService, IOptions<UrlApiSettings>? urlApiSettings, IUsuarioService usuarioService, ICriptografiaProvider criptografiaProvider)
        {
            _usuarioService = usuarioService;
            _criptografiaProvider = criptografiaProvider;
            _apiGateway = urlApiSettings?.Value.ApiGateway;
            _baseUrlController = $"{urlApiSettings?.Value.ApiGateway}/auth";

            _accessToken = accessTokenService.Obter();
        }

        public async Task<ResponseViewModel<AccountViewModel>> Logar(AccountViewModel accountViewModel)
        {
            ResponseViewModel<AccountViewModel> retorno = new ResponseViewModel<AccountViewModel>();

            using (var client = new HttpClient())
            {

                //string url = $"{_baseUrlController}/login";
                //var response2 = await client.PostAsJsonAsync("http://localhost:5296/api/auth/login", accountViewModel);

                //var responseBody = await response2.Content.ReadAsStringAsync(); // para debug
                //Console.WriteLine(responseBody); // ou coloque um breakpoint aqui


                HttpResponseMessage response = await client.PostAsJsonAsync($"{_baseUrlController}/login", accountViewModel);
                retorno.StatusCode = response.StatusCode.GetHashCode();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var obj = JsonSerializer.Deserialize<AccountViewModel>(content, options);

                        retorno.Itens = new List<AccountViewModel> { obj };
                        retorno.Sucesso = true;
                    }
                    catch
                    {
                        retorno.Sucesso = false;
                        retorno.Mensagem = "Erro ao gravar o registro. Favor validar as informações.";
                    }
                }
                else
                {
                    // Mensagem padronizada para qualquer erro
                    retorno.Sucesso = false;
                    retorno.Mensagem = "Erro ao gravar o registro. Favor validar as informações.";
                }

                return retorno;
            }
        }

        public async Task<ResponseViewModel<AccountViewModel>> Logout()
        {
            ResponseViewModel<AccountViewModel> retorno = new ResponseViewModel<AccountViewModel>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);

                try
                {
                    // 1 - Limpa o cache de permissões na API principal
                    HttpResponseMessage responseCache = await client.PostAsync($"{_apiGateway}/permissoes-acesso/limpar-cache", null);

                    if (!responseCache.IsSuccessStatusCode)
                    {
                        retorno.StatusCode = responseCache.StatusCode.GetHashCode();
                        retorno.Sucesso = false;
                        retorno.Mensagem = "Não foi possível limpar o cache de permissões.";

                        return retorno;
                    }

                    // 2 - Faz o logout na API de autenticação
                    HttpResponseMessage response = await client.PostAsync($"{_baseUrlController}/logout", null);

                    retorno.StatusCode = response.StatusCode.GetHashCode();

                    if (response.IsSuccessStatusCode)
                    {
                        retorno.Sucesso = true;
                        retorno.Mensagem = "Logout realizado com sucesso.";
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        ErroViewModel erro = string.IsNullOrWhiteSpace(content) ? null : JsonSerializer.Deserialize<ErroViewModel>(content);

                        retorno.Sucesso = false;

                        if (erro == null || erro.StatusCode == 0)
                        {
                            retorno.Mensagem = "Não foi possível realizar o logout.";
                        }
                        else
                        {
                            retorno.StatusCode = erro.StatusCode;
                            retorno.Mensagem = erro.Message;
                        }
                    }

                    return retorno;
                }
                catch (Exception ex)
                {
                    retorno.Sucesso = false;
                    retorno.StatusCode = 500;
                    retorno.Mensagem = $"Erro ao realizar logout: {ex.Message}";

                    return retorno;
                }
            }
        }

        public async Task<ResponseViewModel<AccountViewModel>> SelecionarUnidade(string tokenSelecaoUnidade, int unidadeId, bool definirComoPadrao)
        {
            var retorno = new ResponseViewModel<AccountViewModel>();

            using (var client = new HttpClient())
            {
                try
                {
                    var request = new
                    {
                        TokenSelecaoUnidade = tokenSelecaoUnidade,
                        UnidadeId = unidadeId,
                        DefinirComoPadrao = definirComoPadrao
                    };

                    var response = await client.PostAsJsonAsync($"{_baseUrlController}/selecionar-unidade", request);
                    retorno.StatusCode = response.StatusCode.GetHashCode();

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var obj = JsonSerializer.Deserialize<AccountViewModel>(content, options);

                        retorno.Itens = obj != null ? new List<AccountViewModel> { obj } : new List<AccountViewModel>();

                        retorno.Sucesso = obj != null;

                        return retorno;
                    }

                    var conteudoErro = await response.Content.ReadAsStringAsync();

                    retorno.Sucesso = false;
                    retorno.Mensagem = string.IsNullOrWhiteSpace(conteudoErro) ? "Não foi possível selecionar a unidade." : conteudoErro;

                    return retorno;
                }
                catch (Exception ex)
                {
                    retorno.Sucesso = false;
                    retorno.StatusCode = 500;
                    retorno.Mensagem = $"Erro ao selecionar unidade: {ex.Message}";
                    return retorno;
                }
            }
        }
    }
}
