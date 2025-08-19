using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.ViewModels.Filters;
using System.Linq;
using TSmartClinic.Shared.DTOs.Requests.Base; // precisa pra FirstOrDefault
using AutoMapper;

namespace TSmartClinic.Presentation.Controllers
{
    public class PerfisController : BaseController<IPerfilService, BaseFilterViewModel, PerfilViewModel>
    {
        private readonly IPerfilService _perfilService;
        private readonly IClienteService _clienteService;
        private readonly INichoService _nichoService;
        private readonly IUsuarioLogadoService _usuarioLogadoService;
        private readonly IPerfilPermissaoService _perfilPermissaoService;
        private readonly IMapper _mapper;

        public PerfisController(IClienteService clienteService,
                                INichoService nichoService,
                                IPerfilService perfilService,
                                IUsuarioLogadoService usuarioLogadoService,
                                IPerfilPermissaoService perfilPermissaoService,
                                IMapper mapper) : base(perfilService)
        {
            _perfilService = perfilService;
            _nichoService = nichoService;
            _usuarioLogadoService = usuarioLogadoService;
            _clienteService = clienteService;
            _perfilPermissaoService = perfilPermissaoService;
            _mapper = mapper;
        }

        public override async Task<IActionResult> Cadastro(PerfilViewModel model)
        {
            await CriarViewBags();

            // Monte OperacaoPerfis a partir dos IDs marcados (sempre antes de usar o model)
            var ids = (model.SelectedOperacaoIds ?? Enumerable.Empty<int>()).Distinct();
            model.OperacaoPerfis = ids
                .Select(id => new OperacaoPerfilViewModel
                {
                    OperacaoId = id,
                    // Se for edição você tem Id; se for criação pode deixar 0/null
                    PerfilId = model.Id ?? 0
                })
                .ToList();

            // Carrega árvore
            model.Modulos = await _perfilPermissaoService.ListarArvorePermissoesAsync();

            // 🔹 Salva as operações do perfil (apenas se já tem Id)
            if (model.Id.HasValue)
            {
                var operacoes = model.SelectedOperacaoIds ?? Enumerable.Empty<int>();
                _perfilService.Atualizar(model.Id.Value, model);
               // await _perfilPermissaoService.SalvarOperacoesDoPerfilAsync(model.Id.Value, operacoes);

            }

            return await base.Cadastro(model);
        }

        public override async Task<IActionResult> Cadastro(int? id)
        {
            await CriarViewBags();

            var arvore = await _perfilPermissaoService.ListarArvorePermissoesAsync();

            if (!id.HasValue)
            {
                return View(new PerfilViewModel { Modulos = arvore });
            }

            //ResponseViewModel<List<PerfilViewModel>>
            var resp = await _perfilService.ObterPorId(id.Value);

            // "Objeto" ITENS do payload
            var model = (resp?.Itens ?? new List<PerfilViewModel>()).FirstOrDefault();

            if (model == null)
            {
                ModelState.AddModelError("", resp?.Mensagem ?? "Perfil não encontrado.");
                return View(new PerfilViewModel { Modulos = arvore });
            }

            model.Modulos = arvore;
            model.SelectedOperacaoIds = await _perfilPermissaoService.ObterOperacoesDoPerfilAsync(id.Value);

            return View(model);
        }


        private async Task CriarViewBags()
        {
            await CriarViewBagNicho();
            await CriarViewClientes();
            ViewBag.UsuarioMaster = _usuarioLogadoService.UsuarioMaster;
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
