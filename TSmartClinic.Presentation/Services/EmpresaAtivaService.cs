using Newtonsoft.Json;
using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.ViewModels;

namespace TSmartClinic.Presentation.Services
{

    public class EmpresaAtivaService : IEmpresaAtivaService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICriptografiaProvider _criptografiaProvider;
        private const string chave_empresa_ativa = "tsmartclinic-empresa-ativa";

        public EmpresaAtivaService(IHttpContextAccessor httpContextAccessor, ICriptografiaProvider criptografiaProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _criptografiaProvider = criptografiaProvider;
        }

        public void Excluir()
        {
            var context = GetHttpContext();

            if (context == null) return;

            context?.Session.Remove(chave_empresa_ativa);

            context?.Response.Cookies.Delete(chave_empresa_ativa);
        }

        public EmpresaAtivaViewModel Obter()
        {
            var context = GetHttpContext();
            if (context == null) return new EmpresaAtivaViewModel();

            var empresaAtivaCrypt = context.Session.GetString(chave_empresa_ativa)
                ?? context.Request.Cookies[chave_empresa_ativa];

            if (string.IsNullOrEmpty(empresaAtivaCrypt))
            {
                return new EmpresaAtivaViewModel();
            }

            var empresaAtivaDecrypted = _criptografiaProvider.Decriptografar(empresaAtivaCrypt);

            return JsonConvert.DeserializeObject<EmpresaAtivaViewModel>(empresaAtivaDecrypted);
        }

        public int ObterId()
        {
            var empresaAtiva = Obter();

            return empresaAtiva?.Id ?? 0;
        }

        public void Salvar(EmpresaAtivaViewModel empresaAtiva)
        {
            var context = GetHttpContext();

            if (context == null) return;

            var empresaAtivaCrypt = _criptografiaProvider.Criptografar(JsonConvert.SerializeObject(empresaAtiva));
            context.Session.SetString(chave_empresa_ativa, empresaAtivaCrypt);

            var cookieOptions = CreateCookieOptions();
            context.Response.Cookies.Append(chave_empresa_ativa, empresaAtivaCrypt, cookieOptions);
        }


        private CookieOptions CreateCookieOptions()
        {
            return new CookieOptions
            {
                HttpOnly = false,
                Expires = DateTime.UtcNow.AddHours(36),
                IsEssential = true,
                Secure = true
            };
        }

        private HttpContext GetHttpContext()
        {
            return _httpContextAccessor.HttpContext;
        }
    }
}
