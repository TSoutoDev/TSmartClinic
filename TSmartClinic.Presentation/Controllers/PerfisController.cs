using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.ViewModels.Filters;
using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Presentation.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento dos perfis de acesso.
    /// </summary>
    public class PerfisController : BaseController<IPerfilService, BaseFilterViewModel, PerfilViewModel>
    {
        private readonly IPerfilService _perfilService;
        private readonly IClienteService _clienteService;
        private readonly INichoService _nichoService;
        private readonly IUsuarioLogadoService _usuarioLogadoService;
        private readonly IPerfilPermissaoService _perfilPermissaoService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância do controller de perfis.
        /// </summary>
        public PerfisController(
            IClienteService clienteService,
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

        /// <summary>
        /// Realiza a inclusão ou alteração de um perfil.
        /// </summary>
        /// <param name="model">Dados do perfil informados na tela.</param>
        /// <returns>Resultado da operação de cadastro.</returns>
        public override async Task<IActionResult> Cadastro(PerfilViewModel model)
        {
            await CriarViewBags(model.ClienteId, model.NichoId);

            var ids = (model.SelectedOperacaoIds ?? Enumerable.Empty<int>()).Distinct();

            model.OperacaoPerfis = ids
                .Select(id => new OperacaoPerfilViewModel { OperacaoId = id })
                .ToList();

            model.Modulos = await _perfilPermissaoService.ListarArvorePermissoesAsync();

            return await base.Cadastro(model);
        }

        /// <summary>
        /// Carrega a tela de inclusão ou edição de perfil.
        /// </summary>
        /// <param name="publicId">
        /// Identificador público do perfil. Nulo para uma nova inclusão.
        /// </param>
        /// <returns>Tela de cadastro do perfil.</returns>
        public override async Task<IActionResult> Cadastro(Guid? publicId)
        {
            var arvore = await _perfilPermissaoService.ListarArvorePermissoesAsync();

            // NOVO CADASTRO
            if (!publicId.HasValue)
            {
                await CriarViewBags();

                return View(new PerfilViewModel
                {
                    Modulos = arvore
                });
            }

            // EDIÇÃO
            var resp = await _perfilService.ObterPorPublicId(publicId.Value);

            var model = (resp?.Itens ?? new List<PerfilViewModel>()).FirstOrDefault();

            if (model == null)
            {
                await CriarViewBags();

                ModelState.AddModelError("",  resp?.Mensagem ?? "Perfil não encontrado."
                );

                return View(new PerfilViewModel
                {
                    Modulos = arvore
                });
            }

            // Agora já temos ClienteId e NichoId do perfil
            await CriarViewBags( model.ClienteId,   model.NichoId);

            model.Modulos = arvore;

            model.SelectedOperacaoIds =  await _perfilPermissaoService.ObterOperacoesDoPerfilAsync(publicId.Value);

            return View(model);
        }

        /// <summary>
        /// Prepara os dados auxiliares utilizados pela tela de cadastro de perfil.
        /// </summary>
        private async Task CriarViewBags(int? clienteId = null, int? nichoId = null)
        {
            await CriarViewBagNicho(nichoId);
            await CriarViewClientes(clienteId);

            ViewBag.UsuarioMaster = _usuarioLogadoService.UsuarioMaster;
        }

        /// <summary>
        /// Carrega os nichos disponíveis para exibição no combo de nichos.
        /// </summary>
        private async Task CriarViewBagNicho(int? nichoId = null)
        {
            var resultado = await _nichoService.ListarNichos();

            ViewBag.Nichos = resultado
                .Select(x => new SelectListItem
                {
                    Text = x.NomeNicho,
                    Value = x.Id.ToString(),
                    Selected = x.Id == nichoId
                })
                .ToList();
        }

        /// <summary>
        /// Carrega os clientes disponíveis e mantém selecionado o cliente informado.
        /// </summary>
        /// <param name="clienteId">Identificador do cliente que deverá permanecer selecionado.</param>
        private async Task CriarViewClientes(int? clienteId = null)
        {
            var resultado = await _clienteService.ListarClientes();

            ViewBag.Clientes = resultado
                .Select(x => new SelectListItem
                {
                    Text = x.NomeCliente,
                    Value = x.Id.ToString(),
                    Selected = x.Id == clienteId
                })
                .ToList();
        }
    }
}