using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.Settings;
using TSmartClinic.Presentation.ViewModels.Filters;
using static System.Net.WebRequestMethods;

namespace TSmartClinic.Presentation.Services
{
    public abstract class BaseService<TFilterViewModel, TViewModel> : IBaseService<TFilterViewModel, TViewModel>
       where TFilterViewModel : BaseFilterViewModel
       where TViewModel : BaseViewModel
    {
        protected readonly string _baseUrlController;
        private readonly int? _empresaAtivaId;
        private readonly string _accessToken;

        protected string AccessToken
        {
            get {return _accessToken;}
        }
        protected int? EmpresaAtivaId
        {
            get{return _empresaAtivaId;}
        }

        protected BaseService(IOptions<UrlApiSettings>? urlApiSettings)
        {
        }

        public BaseService(IAccessTokenService accessTokenService, IOptions<UrlApiSettings>? urlApiSettings, string nomeController)
        {
            _baseUrlController = $"{urlApiSettings?.Value.ApiGateway}/{nomeController}";

            _accessToken = accessTokenService.Obter();
        }


        public BaseService(
            IEmpresaAtivaService empresaAtivaService,
            IAccessTokenService accessTokenService,
            IOptions<UrlApiSettings>? urlApiSettings,
        string nomeController) : this(accessTokenService, urlApiSettings, nomeController)
        {
            _empresaAtivaId = empresaAtivaService.ObterId();
        }


        public virtual async Task<ResponseViewModel<TViewModel>> Atualizar(int id, TViewModel entity)
        {
            ResponseViewModel<TViewModel> retorno = new ResponseViewModel<TViewModel>();

            var property = typeof(TViewModel).GetProperty("IdEmpresa");
            if (property != null && property.CanRead) property.SetValue(entity, _empresaAtivaId);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);
                HttpResponseMessage response = await client.PatchAsJsonAsync($"{_baseUrlController}/{id}", entity);

                retorno.StatusCode = response.StatusCode.GetHashCode();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    retorno.Sucesso = true;
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var erro = string.IsNullOrWhiteSpace(content)
                        ? null
                        : JsonSerializer.Deserialize<ErroViewModel>(content);

                    if (retorno.StatusCode == 401)
                        retorno.Mensagem = "Operação não autorizada!";
                    else if (retorno.StatusCode == 403)
                        retorno.Mensagem = "Usuario não tem permissão!";
                    else
                        retorno.Mensagem = erro?.Message ?? "Erro não tratado.  Operação não realizada.";

                    retorno.Sucesso = false;
                }

                return retorno;
            }
        }

        public async Task<ResponseViewModel<TViewModel>> Excluir(int id)
        {
            ResponseViewModel<TViewModel> retorno = new ResponseViewModel<TViewModel>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);
                HttpResponseMessage response = await client.DeleteAsync($"{_baseUrlController}/{id}");

                retorno.StatusCode = response.StatusCode.GetHashCode();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    retorno.Sucesso = true;
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var erro = string.IsNullOrWhiteSpace(content)
                        ? null
                        : JsonSerializer.Deserialize<ErroViewModel>(content);

                    if (retorno.StatusCode == 401)
                        retorno.Mensagem = "Operação não autorizada!";
                    else if (retorno.StatusCode == 403)
                        retorno.Mensagem = "Usuario não tem permissão!";
                    else
                        retorno.Mensagem = erro?.Message ?? "Erro não tratado.  Operação não realizada.";

                    retorno.Sucesso = false;
                }

                return retorno;
            }
        }

        public virtual async Task<ResponseViewModel<TViewModel>> Inserir(TViewModel entity)
        {
            ResponseViewModel<TViewModel> retorno = new ResponseViewModel<TViewModel>();

            var property = typeof(TViewModel).GetProperty("IdEmpresa");
            if (property != null && property.CanRead) property.SetValue(entity, _empresaAtivaId);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);
                HttpResponseMessage response = await client.PostAsJsonAsync($"{_baseUrlController}", entity);

                retorno.StatusCode = response.StatusCode.GetHashCode();
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    retorno.Sucesso = true;
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var erro = string.IsNullOrWhiteSpace(content)
                        ? null
                        : JsonSerializer.Deserialize<ErroViewModel>(content);

                    if (retorno.StatusCode == 401)
                        retorno.Mensagem = "Operação não autorizada!";
                    else if (retorno.StatusCode == 403)
                        retorno.Mensagem = "Usuario não tem permissão!";
                    else
                        retorno.Mensagem = erro?.Message ?? "Erro não tratado.  Operação não realizada.";

                    retorno.Sucesso = false;
                }

                return retorno;
            }
        }

        public virtual async Task<ResponseViewModel<TViewModel>> Listar(TFilterViewModel filtro)
        {
            ResponseViewModel<TViewModel> retorno = new ResponseViewModel<TViewModel>();

            var property = typeof(TFilterViewModel).GetProperty("IdEmpresa");

            if (property != null && property.CanRead) property.SetValue(filtro, _empresaAtivaId);

            using (var client = new HttpClient())
            {
                //var teste = $"{_baseUrlController}/listar/{filtro}";
                //var endpoint = "http://localhost:5136/api/usuarios/listar";
               
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);
                HttpResponseMessage response = await  client.PostAsJsonAsync($"{_baseUrlController}/listar", filtro);

                retorno.StatusCode = response.StatusCode.GetHashCode();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    var content = await response.Content.ReadAsStringAsync();
                    retorno = JsonSerializer.Deserialize<ResponseViewModel<TViewModel>>(content, options);

                    retorno.Sucesso = true;
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var erro = string.IsNullOrWhiteSpace(content)
                        ? null
                        : JsonSerializer.Deserialize<ErroViewModel>(content);

                    if (retorno.StatusCode == 401)
                        retorno.Mensagem = "Operação não autorizada!";

                    else if (retorno.StatusCode == 403)
                        retorno.Mensagem = "Usuario não tem permissão!";
                    else
                        retorno.Mensagem = erro?.Message ?? "Erro não tratado.  Operação não realizada.";

                    retorno.Sucesso = false;
                }

                return retorno;
            }
        }

        public async Task<ResponseViewModel<TViewModel>> ObterPorId(int id)
        {
            ResponseViewModel<TViewModel> retorno = new ResponseViewModel<TViewModel>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);
                HttpResponseMessage response = await client.GetAsync($"{_baseUrlController}/{id}");

                retorno.StatusCode = response.StatusCode.GetHashCode();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    var content = await response.Content.ReadAsStringAsync();
                    var obj = JsonSerializer.Deserialize<TViewModel>(content, options);

                    retorno.Sucesso = true;
                    retorno.Itens = new List<TViewModel> { obj };
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var erro = string.IsNullOrWhiteSpace(content)
                        ? null
                        : JsonSerializer.Deserialize<ErroViewModel>(content);

                    if (retorno.StatusCode == 401)
                        retorno.Mensagem = "Operação não autorizada!";
                    else if (retorno.StatusCode == 403)
                        retorno.Mensagem = "Usuario não tem permissão!";
                    else
                        retorno.Mensagem = erro?.Message ?? "Erro não tratado.  Operação não realizada.";

                    retorno.Sucesso = false;
                }

                return retorno;
            }
        }


    }
}
