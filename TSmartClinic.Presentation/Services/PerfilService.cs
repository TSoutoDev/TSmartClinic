using Microsoft.Extensions.Options;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.Settings;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services
{
    public class PerfilService : BaseService<BaseFilterViewModel, PerfilViewModel>, IPerfilService
    {
        public PerfilService(IAccessTokenService accessTokenService, IOptions<UrlApiSettings>? urlApiSettings) : base(accessTokenService,urlApiSettings, "perfis")
        {
        }
    }
}
