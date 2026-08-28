using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Controllers.Clientes_
{
    public class ClientesController : BaseController<IClienteService, BaseFilterViewModel, ClienteViewModel>
    {
        private readonly INichoService _nichoService;

        public ClientesController(INichoService nichoService, IClienteService service) : base(service)
        {
            _nichoService = nichoService;
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
            await CriarViewBags();

            var result = await base.Cadastro(publicId) as ViewResult;

            if (result?.Model is ClienteViewModel model)
            {
                // Aqui depois podemos tratar dados específicos
                // do Cliente, como endereço, se necessário.
            }

            return result;
        }

        [HttpPost]
        public override async Task<IActionResult> Cadastro(ClienteViewModel model)
        {
            await CriarViewBags();

            return await base.Cadastro(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Excluir(ClienteViewModel model)
        {
            return await base.Excluir(model);
        }

        private async Task CriarViewBags()
        {
            await CriarViewNichos();
        }

        private async Task CriarViewNichos()
        {
            var resultado = await _nichoService.ListarNichos();

            var lista = resultado
                .Select(x => new SelectListItem
                {
                    Text = x.NomeNicho,
                    Value = x.Id.ToString()
                })
                .ToList();

            lista.Insert(
                0,
                new SelectListItem
                {
                    Text = "- Selecione o Nicho -",
                    Value = ""
                });

            ViewBag.Nichos = lista;
        }
    }
}