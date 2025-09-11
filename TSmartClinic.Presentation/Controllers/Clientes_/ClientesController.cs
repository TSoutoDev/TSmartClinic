using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Controllers.Clientes_
{
    public class ClientesController : BaseController<IClienteService, BaseFilterViewModel,ClienteViewModel>
    {
        private readonly INichoService _nichoService;
        public ClientesController(INichoService nichoService, IClienteService service) : base(service)
        {
            _nichoService = nichoService;
        }

        public override async Task<IActionResult> Cadastro(ClienteViewModel model)
        {
            await CriarViewBags();

            return await base.Cadastro(model);
        }

        // GET: Cadastro
        public override async Task<IActionResult> Cadastro(int? id)
        {
            await CriarViewBags();

            var result = await base.Cadastro(id) as ViewResult;

            if (result?.Model is UsuarioViewModel model)
            {

                await CriarViewBags();
            }

            return result;
        }

        // GET: Busca Avançada
        public override async Task<IActionResult> BuscaAvancada(BaseFilterViewModel filtro)
        {
            return await base.BuscaAvancada(filtro);
        }

        // GET: Consulta
        public override async Task<IActionResult> Consulta()
        {
            return await base.Consulta();
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
                    Text = $"{x.NomeNicho}", 
                    Value = x.Id.ToString()
                })
                .ToList();

            // Insere opção inicial (placeholder) sem definir Selected
            lista.Insert(0, new SelectListItem { Text = "- Selecione o Nicho -", Value = "" });
            ViewBag.Nichos = lista;
        }
    }
}
