using TSmartClinic.Presentation.Services.Interfaces;

namespace TSmartClinic.Presentation.Services
{
    public class UsuarioLogadoService : IUsuarioLogadoService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UsuarioLogadoService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        //public int? IdUsuarioLogado =>
        //    int.TryParse(_contextAccessor.HttpContext?.User.FindFirst("IdUsuario")?.Value, out var id) ? id : (int?)null;
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

        public string TipoUsuario =>
            _contextAccessor.HttpContext?.User?.FindFirst("Usuario_Tipo")?.Value?.ToUpper();

        public bool UsuarioMaster => TipoUsuario == "M";

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
    }
}
