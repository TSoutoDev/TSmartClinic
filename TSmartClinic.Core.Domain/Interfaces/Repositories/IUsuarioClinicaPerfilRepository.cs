namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IUsuarioClinicaPerfilRepository 
    {
        List<string> ObterPermissaoPorIds(int usuarioId, int clinicaId);
    }
}
