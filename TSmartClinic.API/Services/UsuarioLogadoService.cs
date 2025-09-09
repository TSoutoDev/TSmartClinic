using System.Security.Claims;
using TSmartClinic.Core.Domain.Interfaces.Services;

namespace TSmartClinic.API.Services
{
    public class UsuarioLogadoService : IUsuarioLogadoService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UsuarioLogadoService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string TipoUsuario => _contextAccessor.HttpContext?.User.FindFirst("TipoUsuario")?.Value?.ToUpper();
        public bool UsuarioMaster => TipoUsuario == "M";

        public int? UsuarioLogadoId
        {
            get
            {
                var claim = _contextAccessor.HttpContext?.User?.FindFirst("Usuario_Id");
                if (claim != null && int.TryParse(claim.Value, out int id))
                {
                    return id;
                }
                return null;
            }
        }
        public int? ClienteId
        {
            get
            {
                var claim = _contextAccessor.HttpContext?.User?.FindFirst("Cliente_Id");
                if (claim != null && int.TryParse(claim.Value, out int id))
                {
                    return id;
                }
                return null;
            }
        }
        public int? NichoClienteId
        {
            get
            {
                var claim = _contextAccessor.HttpContext?.User?.FindFirst("Cliente_NichoId");
                if (claim != null && int.TryParse(claim.Value, out int id))
                {
                    return id;
                }
                return null;
            }
        }

        public string? Email => _contextAccessor.HttpContext?.User?.FindFirst("Usuario_Email")?.Value;
    }
}
