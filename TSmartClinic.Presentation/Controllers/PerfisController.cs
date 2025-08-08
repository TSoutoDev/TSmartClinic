using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Controllers
{
    public class PerfisController : BaseController<IPerfilService, BaseFilterViewModel, PerfilViewModel>
    {
        private readonly IPerfilService _perfilService;
        private readonly IClienteService _clienteService;
        private readonly INichoService _nichoService;
        private readonly IUsuarioLogadoService _usuarioLogadoService;


        public PerfisController(IClienteService clienteService, INichoService nichoService, IPerfilService perfilService, IUsuarioLogadoService usuarioLogadoService) : base(perfilService)
        {
            _perfilService = perfilService;
            _nichoService = nichoService;
            _usuarioLogadoService = usuarioLogadoService;
            _clienteService = clienteService;
        }

        public override async Task<IActionResult> Cadastro(PerfilViewModel model)
        {
            await CriarViewBags();
            return await base.Cadastro(model);
        }

        public override async Task<IActionResult> Cadastro(int? id)
        {
            await CriarViewBags();
            return await base.Cadastro(id);
        }

        private async Task CriarViewBags()
        {
            await CriarViewBagNicho();
            await CriarViewClientes();
            ViewBag.UsuarioMaster = _usuarioLogadoService.UsuarioMaster;
            //   await CriarViewBagRiscos();
            //   await CriarViewBagFatoresRiscos();
        }

        private async Task CriarViewBagNicho()
        {
            var resultado = await _nichoService.ListarNichos();

            ViewBag.Nichos = resultado
                .Select(x => new SelectListItem
                {
                    Text = x.NomeNicho,
                    Value = x.Id.ToString()
                });
        }


        private async Task CriarViewClientes()
        {
            var resultado = await _clienteService.ListarClientes();

            ViewBag.Clientes = resultado
                .Select(x => new SelectListItem
                {
                    Text = x.NomeCliente,
                    Value = x.Id.ToString()
                });
        }
    }
}
