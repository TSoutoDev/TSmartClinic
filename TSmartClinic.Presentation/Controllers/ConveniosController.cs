using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Controllers
{
    public class ConveniosController
        : BaseController<IConvenioService, BaseFilterViewModel, ConvenioViewModel>
    {
        private readonly IConvenioService _convenioService;
        private readonly IClienteService _clienteService;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public ConveniosController(
            IConvenioService convenioService,
            IClienteService clienteService,
            IUsuarioLogadoService usuarioLogadoService)
            : base(convenioService)
        {
            _convenioService = convenioService;
            _clienteService = clienteService;
            _usuarioLogadoService = usuarioLogadoService;
        }

        [HttpGet]
        public override async Task<IActionResult> Consulta()
        {
            return await base.Consulta();
        }

        [HttpPost]
        public override async Task<IActionResult> BuscaPadrao(BaseFilterViewModel filtro)
        {
            return await base.BuscaPadrao(filtro);
        }

        [HttpPost]
        public override async Task<IActionResult> BuscaAvancada(BaseFilterViewModel filtro)
        {
            return await base.BuscaAvancada(filtro);
        }

        [HttpGet]
        public override async Task<IActionResult> Cadastro(Guid? publicId)
        {
            var result = await base.Cadastro(publicId) as ViewResult;

            if (result?.Model is ConvenioViewModel model)
            {
                if (!_usuarioLogadoService.UsuarioMaster)
                {
                    model.ClienteId = _usuarioLogadoService.ClienteId ?? 0;
                }

                await CriarViewClientes(model.ClienteId);
            }
            else
            {
                await CriarViewClientes();
            }

            ViewBag.UsuarioMaster = _usuarioLogadoService.UsuarioMaster;

            return result;
        }

        [HttpPost]
        public override async Task<IActionResult> Cadastro(ConvenioViewModel model)
        {
            if (!_usuarioLogadoService.UsuarioMaster)
            {
                model.ClienteId = _usuarioLogadoService.ClienteId ?? 0;
            }

            if (!ModelState.IsValid)
            {
                await CriarViewClientes(model.ClienteId);
                ViewBag.UsuarioMaster = _usuarioLogadoService.UsuarioMaster;

                return View(model);
            }

            return await base.Cadastro(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Excluir(ConvenioViewModel model)
        {
            return await base.Excluir(model);
        }

        private async Task CriarViewBags()
        {
            ViewBag.UsuarioMaster = _usuarioLogadoService.UsuarioMaster;

            if (_usuarioLogadoService.UsuarioMaster)
            {
                await CriarViewClientes();
            }
        }

        private async Task CriarViewClientes(int? clienteSelecionado = null)
        {
            var resultado = await _clienteService.ListarClientes();

            var lista = resultado
                .Select(x => new SelectListItem
                {
                    Text = x.NomeCliente,
                    Value = x.Id.ToString(),
                    Selected = clienteSelecionado.HasValue && x.Id == clienteSelecionado.Value
                })
                .ToList();

            if (_usuarioLogadoService.UsuarioMaster)
            {
                lista.Insert(0, new SelectListItem
                {
                    Text = "- Selecione o Cliente -",
                    Value = ""
                });
            }

            ViewBag.Clientes = lista;
        }
    }
}