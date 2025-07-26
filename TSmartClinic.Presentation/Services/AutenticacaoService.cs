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


        protected string AccessToken
        {
            get{return _accessToken;}
        }

        public AutenticacaoService(IAccessTokenService accessTokenService, IOptions<UrlApiSettings>? urlApiSettings, IUsuarioService usuarioService, ICriptografiaProvider criptografiaProvider)
        {
            _usuarioService = usuarioService;
            _criptografiaProvider = criptografiaProvider;

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
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    var content = await response.Content.ReadAsStringAsync();
                    var obj = JsonSerializer.Deserialize<AccountViewModel>(content, options);

                    retorno.Itens = new List<AccountViewModel>() { obj };
                    retorno.Sucesso = true;
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ErroViewModel erro = string.IsNullOrWhiteSpace(content)
                        ? null
                        : JsonSerializer.Deserialize<ErroViewModel>(content);

                    retorno.Sucesso = false;
                    if (erro == null || erro?.StatusCode == 0)
                    {
                        retorno.StatusCode = retorno.StatusCode;
                        retorno.Mensagem = "Erro ao gravar o registro.  Favor validar as informações.";
                    }
                    else
                    {
                        retorno.StatusCode = erro.StatusCode;
                        retorno.Mensagem = erro.Message;
                    }
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
                HttpResponseMessage response = await client.PostAsync($"{_baseUrlController}/logout", null);

                retorno.StatusCode = response.StatusCode.GetHashCode();
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    retorno.Sucesso = true;
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ErroViewModel erro = string.IsNullOrWhiteSpace(content)
                        ? null
                        : JsonSerializer.Deserialize<ErroViewModel>(content);

                    retorno.Sucesso = false;
                    if (erro == null || erro?.StatusCode == 0)
                    {
                        retorno.StatusCode = retorno.StatusCode;
                        retorno.Mensagem = "Erro ao gravar o registro.  Favor validar as informações.";
                    }
                    else
                    {
                        retorno.StatusCode = erro.StatusCode;
                        retorno.Mensagem = erro.Message;
                    }
                }

                return retorno;
            }
        }


    }
}
