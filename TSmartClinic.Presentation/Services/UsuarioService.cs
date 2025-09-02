using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.Settings;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services
{
    public class UsuarioService : BaseService<UsuarioFilterViewModel, UsuarioViewModel>, IUsuarioService
    {

        public UsuarioService(IAccessTokenService accessTokenService,IOptions<UrlApiSettings>? urlApiSettings) : base(accessTokenService, urlApiSettings, "usuarios")
        {
        }


        public async Task Bloquear(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{_baseUrlController}/{id}/bloquear");
            }
        }
        public async Task<ResponseViewModel<UsuarioViewModel>> ObterPorEmail(string email)
        {
            ResponseViewModel<UsuarioViewModel> retorno = new ResponseViewModel<UsuarioViewModel>();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{_baseUrlController}/obter-por-email/{email}");

                retorno.StatusCode = response.StatusCode.GetHashCode();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    var content = await response.Content.ReadAsStringAsync();
                    var obj = JsonSerializer.Deserialize<UsuarioViewModel>(content, options);

                    retorno.Sucesso = true;
                    retorno.Itens = new List<UsuarioViewModel> { obj };
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var erro = JsonSerializer.Deserialize<ErroViewModel>(content);

                    retorno.Sucesso = false;
                    retorno.StatusCode = erro.StatusCode;
                    retorno.Mensagem = erro.Message;
                }

                return retorno;
            }
        }
    

        public async Task<UsuarioViewModel> PreencherDados(UsuarioViewModel viewModel)
        {
            if (viewModel.Id.HasValue)
            {
                viewModel.Senha = viewModel.Senha;
                viewModel.Email = viewModel.Email;
                //viewModel.Login = viewModel.Login;

            }
            else
            {
                viewModel.Senha = GerarSenha();
               // viewModel.Login = GerarLogin();
            }

            return await Task.FromResult(viewModel);
        }

        public Task ProcessarFotoAsync(UsuarioViewModel model, string foto)
        {
            if (!string.IsNullOrEmpty(foto))
            {
                try
                {
                    // Remove o prefixo caso exista
                    var base64Data = foto;

                    if (foto.Contains(","))
                        base64Data = foto.Substring(foto.IndexOf(",") + 1);

                    // Remove espaços e caracteres inválidos comuns
                    base64Data = base64Data.Replace(" ", "").Replace("\r", "").Replace("\n", "");

                    model.Foto = Convert.FromBase64String(base64Data);
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException("Formato da foto inválido.", ex);
                }
            }
            return Task.CompletedTask;
        }

        public string GerarLogin()
        {
            return Guid.NewGuid().ToString().Substring(0, 13);
        }

        public string GerarSenha()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

            string senha = new string(Enumerable.Repeat(chars, 6)
                 .Select(s => s[random.Next(s.Length)]).ToArray());

            string senhaHash = HashSenhaSHA256(senha);

            return senhaHash;
        }

        //criptografar a senha
        public static string HashSenhaSHA256(string senha)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));
                return Convert.ToBase64String(bytes);
            }
        }

        public async Task<ResponseViewModel<PrimeiroAcessoViewModel>> DefinirSenhaPrimeiroAcesso(int usuarioId, string novaSenha)
        {
            ResponseViewModel<PrimeiroAcessoViewModel> retorno = new ResponseViewModel<PrimeiroAcessoViewModel>();

            using (var client = new HttpClient())
            {
                // HttpResponseMessage response = await client.GetAsync($"{_baseUrlController}/primeiro-acesso/{usuarioId}");
                //var url2 = ($"{_baseUrlController}/{usuarioId}/primeiro-acesso");
                var url = $"http://localhost:5136/api/usuarios/{usuarioId}/primeiro-acesso";
                var dto = new PrimeiroAcessoViewModel { NovaSenha = novaSenha}; // "novaSenha" se a API usa camelCase
                HttpResponseMessage response = await client.PatchAsync(url, JsonContent.Create(dto));
            
                retorno.StatusCode = response.StatusCode.GetHashCode();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    var content = await response.Content.ReadAsStringAsync();
                    var obj = JsonSerializer.Deserialize<PrimeiroAcessoViewModel>(content, options);

                    retorno.Sucesso = true;
                    retorno.Itens = new List<PrimeiroAcessoViewModel> { obj };
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var erro = JsonSerializer.Deserialize<ErroViewModel>(content);

                    retorno.Sucesso = false;
                    retorno.StatusCode = erro.StatusCode;
                    retorno.Mensagem = erro.Message;
                }

                return retorno;
            }
        }
    }
}
