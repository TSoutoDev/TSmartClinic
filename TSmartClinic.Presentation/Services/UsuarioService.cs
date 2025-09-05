using Microsoft.Extensions.Options;
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
        public async Task<ResponseViewModel<PrimeiroAcessoViewModel>> DefinirSenhaTokenAsync(string token, string novaSenha)
        {
            var retorno = new ResponseViewModel<PrimeiroAcessoViewModel>();

            using var client = new HttpClient();
            var url = $"{_baseUrlController}/primeiro-acesso"; // ex.: http://localhost:5136/api/usuarios/primeiro-acesso
            var payload = new { token, novaSenha };

            var response = await client.PostAsync(url, JsonContent.Create(payload));

            retorno.StatusCode = (int)response.StatusCode;
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                retorno.Sucesso = true;
                retorno.Mensagem = "Senha definida com sucesso.";
                return retorno;
            }

            try
            {
                var erro = JsonSerializer.Deserialize<ErroViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                retorno.Sucesso = false;
                retorno.Mensagem = erro?.Message ?? "Falha ao definir a senha.";
            }
            catch
            {
                retorno.Sucesso = false;
                retorno.Mensagem = "Falha ao definir a senha.";
            }

            return retorno;
        }



        public async Task<UsuarioViewModel> PreencherDados(UsuarioViewModel viewModel)
        {
            if (viewModel.Id.HasValue)
            {
                viewModel.Senha = viewModel.Senha;
                viewModel.Email = viewModel.Email;
                //viewModel.Login = viewModel.Login;

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

        public Task<ResponseViewModel<UsuarioViewModel>> ObterPorEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
