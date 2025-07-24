using TSmartClinic.Presentation.ViewModels;

namespace TSmartClinic.Presentation.Services.Interfaces
{
    public interface IEmpresaAtivaService
    {

        void Salvar(EmpresaAtivaViewModel empresaAtiva);
        EmpresaAtivaViewModel Obter();
        int ObterId();
        void Excluir();
    }
}
